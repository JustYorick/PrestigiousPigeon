using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using EnvironmentScripts.EnvironmentEffect;

namespace ReDesign
{
    public class SnowKingAwake : MonoBehaviour
    {
        public bool AllPillarsDestroyed = false;
        private int pillars = 0;
        [SerializeField] GameObject SnowBoss;
        [SerializeField] GameObject Skeleton;
        [SerializeField] GameObject Layer;
        [SerializeField] ParticleSystem SpawnParticles;
        [SerializeField] private TMPro.TMP_Text objectiveText;

        public void pillardestroyed(Vector3 pos)
        {
            pillars += 1;

            if (!this.gameObject.scene.isLoaded) return;
            // spawn Skeleton
            if (WorldController.Instance.checkNode(pos))
            {
                // check if tile occupied
                GameObject h = Instantiate(Skeleton, new Vector3(pos.x, pos.y - .27f, pos.z),
                    Quaternion.Euler(0, 0, 0));
                SpawnParticles.transform.position = h.transform.position;
                SpawnParticles.Play();
                if (Layer.activeInHierarchy)
                {
                    h.transform.parent = Layer.transform;
                }

                WorldController.Instance.addObstacle(h);
            }

            if (pillars == 2)
            {
                // freeze river
                WorldController.Instance.GetComponent<EnvironmentEffect>()
                    .ChangeWaterTilesToIce(WorldController.Instance.BaseLayer);
            }

            if (pillars == 3)
            {
                //Change music?
                GetComponent<AddSnowToObjects>().StartSnowing();
            }

            // spawn Snow King
            if (pillars == 4)
            {
                // Transition to snowy area with cutscene
                GetComponent<AddSnowToObjects>().AddSnow();
                GameObject g = Instantiate(SnowBoss, transform.position, Quaternion.Euler(0, 0, 0));
                if (Layer.activeInHierarchy)
                {
                    g.transform.parent = Layer.transform;
                }

                WorldController.Instance.addObstacle(g);

                pillars = 0;
                AllPillarsDestroyed = true;
                CameraController.Instance.TurnOnAnimator(CameraController.Instance.transform.position);
                if (objectiveText != null)
                {
                    objectiveText.text = "Kill all enemies!";
                }

                CollapseableUI gameUi = GameObject.Find("GameUI").GetComponent<CollapseableUI>();
                gameUi.ShowObjectiveUI();
            }
        }
    }
}