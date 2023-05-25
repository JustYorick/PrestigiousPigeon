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
        private Vector3 targetLocation;
        public IEnumerator movingCoroutine;
        private static GameObject _gameOver;
        private Vector3 targetLoc;
        public abstract void NextAction();
        public abstract void Move();
        public abstract void Attack();
        public abstract int SightRange { get; }
        public abstract int MoveRange { get; }
        
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
                    DefaultTile obstacleTile = WorldController.ObstacleLayer
                        .FirstOrDefault(t => t.GameObject == gameObject);
                    WorldController.Instance.BaseLayer.FirstOrDefault(t => t.XPos == obstacleTile.XPos && t.YPos == obstacleTile.YPos)
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
            DefaultTile currentTile = WorldController.ObstacleLayer
                .FirstOrDefault(o => o.GameObject == this.gameObject);
            List<DefaultTile> targetLocations = Attacks[0].GetTargetLocations(currentTile.XPos, currentTile.YPos);
            DefaultTile enemyPos = WorldController.getPlayerTile();
            if (targetLocations.FirstOrDefault(t => t.XPos == enemyPos.XPos && t.YPos == enemyPos.YPos) == null)
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
                
                if (path != null)
                {
                    List<DefaultTile> actualPath = new List<DefaultTile>();

                    Debug.Log("moverange = " + movementRange + " pc " + path.Count);
                    if(movementRange >= path.Count){
                        if (path.Count > 0){
                            movementRange = path.Count-1;
                        } else {
                            movementRange = 0;
                        }
                    }
                    actualPath.AddRange(path.GetRange(0, movementRange+1));

                    actualPath.First().Walkable = true;
                    actualPath.Last().Walkable = false;


                    movingCoroutine = EntityMoveSquares(actualPath);
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
            }
        }
        
        // Enumerator function for moving an entity along a path of tiles
        public IEnumerator EntityMoveSquares(List<DefaultTile> path)
        {
            GridLayout gr = WorldController.Instance.gridLayout;
            // Loop over each tile in the path (skipping the first one, since that's the entity's starting tile)
            for (int i = 1; i < path.Count; i++)
            {
                // Get the next tile in the path
                DefaultTile pathNode = path[i];
                
                // Calculate the direction to the target position and set the entity's rotation accordingly
                Vector3 targetPos = new Vector3(pathNode.GameObject.transform.position.x, transform.position.y, pathNode.GameObject.transform.position.z);
                Vector3 dir = (targetPos - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(dir,Vector3.up);
                targetLoc = PlayerMovement.SnapCoordinateToGrid(targetPos, gr);
                float time = 0; 
                
                // Loop until the entity has moved halfway to the target location
                while (time < 0.5f)
                {
                    // Adds the position and rotation
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, time / 0.5f);
                    transform.position = Vector3.MoveTowards(transform.position, targetLoc, Time.deltaTime * 5);
                    time += Time.deltaTime;
                    yield return null;
                }
                transform.rotation = targetRotation;
            }

            finishedMoving = true;
        }
        

        public virtual void Update()
        {

            if (finishedMoving)
            {
                finishedMoving = false;
                this.Attack();
                StateController.ChangeState(GameState.EndTurn);
            }
        }
        
        public IEnumerator EnemyRotateToAttack()
        {
            Vector3 attackerPos = transform.position;
            Vector3 targetPos = WorldController.getPlayerTile().GameObject.transform.position;
            GridLayout gr = WorldController.Instance.gridLayout;
            // Calculate the direction to the target position and set the entity's rotation accordingly
            Vector3 targetPosition = new Vector3(targetPos.x, attackerPos.y, targetPos.z);
            Vector3 dir = (targetPosition - attackerPos).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up);
            targetLocation = PlayerMovement.SnapCoordinateToGrid(targetPos, gr);
            float time = 0;

            // Loop until the entity has moved halfway to the target location
            while (time < 0.5f)
            {
                // Adds the position and rotation
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, time / 0.5f);
                time += Time.deltaTime;
                yield return null;
            }
            transform.rotation = targetRotation;
        }
    }
}