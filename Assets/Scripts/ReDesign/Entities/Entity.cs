using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReDesign.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public UnitHealth _entityHealth { get; set; }
        [SerializeField] private GameObject _healthBar;
        public List<AttacksAndSpells> Attacks { get; set; }
        public bool finishedMoving = false;
        public bool attacking = false;
        public IEnumerator movingCoroutine;
        private static GameObject _gameOver;
        public abstract void NextAction();
        public abstract void Move();
        public abstract void Attack();

        public virtual void ReceiveDamage(int dmg)
        {
            _entityHealth.ChangeHealth(-dmg);
            _healthBar.transform.localScale = (new Vector3(
                _entityHealth.HealthPercentage(_entityHealth.Health),
                (float)0.1584, (float)0.09899999));
            
            if (_entityHealth.Health <= 0)
            {
                if (this.gameObject.name.Contains("Player"))
                {
                    TurnController.gameOver = true;
                    PlayerAnimator._animator.SetBool("PlayerDead", true);
                }
                else
                {
                    //Add animation so it isnt instant
                    DefaultTile obstacleTile = WorldController.ObstacleLayer.Where(t => t.GameObject == gameObject)
                        .FirstOrDefault();
                    WorldController.Instance.BaseLayer
                        .Where(t => t.XPos == obstacleTile.XPos && t.YPos == obstacleTile.YPos).FirstOrDefault()
                        .Walkable = true;
                    WorldController.ObstacleLayer.RemoveAt(WorldController.ObstacleLayer.IndexOf(obstacleTile));
                    obstacleTile.GameObject = null;
                    obstacleTile = null;
                    Destroy(this.gameObject);
                }

                TurnController.Instance.gameOverEvent.Invoke();
            }
        }

        public void MoveToPlayer(int movementRange)
        {
            DefaultTile currentTile = WorldController.ObstacleLayer.Where(o => o.GameObject == this.gameObject)
                .FirstOrDefault();
            List<DefaultTile> targetLocations = Attacks[0].GetTargetLocations(currentTile.XPos, currentTile.YPos);
            DefaultTile enemyPos = WorldController.getPlayerTile();
            if (targetLocations.Where(t => t.XPos == enemyPos.XPos && t.YPos == enemyPos.YPos).FirstOrDefault() == null)
            {
                int widthAndHeight = (int)Mathf.Sqrt(WorldController.Instance.BaseLayer.Count);
                PlayerPathfinding pf =
                    new PlayerPathfinding(widthAndHeight, widthAndHeight, WorldController.Instance.BaseLayer);

                List<DefaultTile> path = null;

                foreach (DefaultTile dt in pf.GetNeighbourList(enemyPos))
                {
                    List<DefaultTile> newPath = pf.FindPath(currentTile.XPos, currentTile.YPos, dt.XPos, dt.YPos);
                    if (newPath != null && (path == null || newPath.Count < path.Count))
                    {
                        path = newPath;
                    }
                }


                Debug.Log("playerpos = " + enemyPos.XPos);
                if (path != null)
                {
                    List<DefaultTile> actualPath = new List<DefaultTile>();
                    actualPath.AddRange(path.GetRange(0, movementRange + 1));
                    actualPath.First().Walkable = true;
                    actualPath.Last().Walkable = false;


                    movingCoroutine = EntityMoveSquares(actualPath, currentTile.GameObject.transform.position.y);
                    StartCoroutine(movingCoroutine);
                    currentTile.XPos = actualPath.Last().XPos;
                    currentTile.YPos = actualPath.Last().YPos;
                }
                else
                {
                    finishedMoving = true;
                }
            }
            else
            {
                finishedMoving = true;
                attacking = true;
            }
        }

        public IEnumerator EntityMoveSquares(List<DefaultTile> path, float height)
        {
            foreach (DefaultTile pathNode in path)
            {
                transform.position = new Vector3(pathNode.GameObject.transform.position.x, height,
                    pathNode.GameObject.transform.position.z);
                yield return new WaitForSeconds(.2f);
            }

            finishedMoving = true;
        }

        public virtual void Update()
        {
            if (finishedMoving == true)
            {
                attacking = true;
                //finishedMoving = false;
            }

            if (attacking == true)
            {
                this.Attack();
            }

            if (finishedMoving && !attacking)
            {
                finishedMoving = false;
                StateController.ChangeState(GameState.EndTurn);
            }
        }
    }
}