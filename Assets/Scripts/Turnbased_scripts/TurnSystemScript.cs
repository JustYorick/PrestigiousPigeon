using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystemScript : MonoBehaviour
{
    public List<TurnClass> playersGroup;

    private CameraController cameraController;

    public bool isSwitched = false;
    // Start is called before the first frame update
    void Start()
    {
        cameraController = GameObject.Find("CameraController").GetComponent<CameraController>();

        ResetTurns();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTurns();
    }

    void ResetTurns()
    {
        // Zet de beurt naar de eerste 
        for (int i = 0; i < playersGroup.Count; i++)
        {
            if (i == 0)
            {
                playersGroup[i].isTurn = true;
                playersGroup[i].wasTurnPrev = false;
            }
            else
            {
                playersGroup[i].isTurn = false;
                playersGroup[i].wasTurnPrev = false;
            }
        }
    }

    void UpdateTurns()
    {
        for (int i = 0; i < playersGroup.Count; i++)
        {
            if (!playersGroup[i].wasTurnPrev)
            {
                playersGroup[i].isTurn = true;

                if (isSwitched)
                {
                    StartCoroutine(moveObject(cameraController.transform.position, playersGroup[i].playerGameObject.transform.position));
                }
                isSwitched = false;
                
                break;
            }
            if (i == playersGroup.Count - 1 &&
                     playersGroup[i].wasTurnPrev)
            {
                ResetTurns();
            }
        }
    }
    
    public IEnumerator moveObject(Vector3 origin,Vector3 destination) {
        float totalMovementTime = 0.4f; //the amount of time you want the movement to take
        float currentMovementTime = 0f;//The amount of time that has passed
        while (Vector3.Distance(cameraController.transform.position, destination) > 0.2f) {

            currentMovementTime += Time.deltaTime;
            cameraController.transform.position = Vector3.Lerp(origin, destination, currentMovementTime / totalMovementTime);
            yield return null;
        }
    }
    
}

[System.Serializable]
public class TurnClass
{
    public GameObject playerGameObject;
    public bool isTurn = false;
    public bool wasTurnPrev = false;
}

