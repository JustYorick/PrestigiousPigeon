using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class PlayerMove : MonoBehaviour
{
    public GridLayout gridLayout;
    private Grid grid;
    public GameObject player;

    private void Awake()
    {
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private static Vector3 GetMouseWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    private void OnMouseDown()
    {
        int width = 14; //temp
        int height = 14; //temp
        Vector3 pos = GetMouseWorldPos();
       
        PlayerPathfinding playerPathfinding = new PlayerPathfinding(width, height, /**/new List<PathNode>());
        //Debug.Log(""+(int)Math.Floor(player.transform.position.x) + (int)Math.Floor(player.transform.position.z) + (int)Math.Floor(pos.x) + (int)Math.Floor(pos.z));
        List<PathNode> path = playerPathfinding.FindPath((int)Math.Floor(player.transform.position.x), (int)Math.Floor(player.transform.position.z), (int)Math.Floor(pos.x), (int)Math.Floor(pos.z));
        if (path != null)
        {
            Debug.Log("PATH LENGTH:"+path.Count);
            StartCoroutine(MoveSquares(path));
        }
    }

    IEnumerator MoveSquares(List<PathNode> path)
    {
        foreach (PathNode pathNode in path)
        {
            Debug.Log(pathNode.x + "," + pathNode.y);
            player.transform.position = SnapCoordinateToGrid(new Vector3(pathNode.x, 0, pathNode.y));
            yield return new WaitForSeconds(.2f);
        }
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
