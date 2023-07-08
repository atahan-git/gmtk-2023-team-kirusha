using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTesting : MonoBehaviour
{
    private Pathfinding pathfinding;


    void Start()
    {
        pathfinding = new Pathfinding(10,10);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
           
            pathfinding.GetGrid().GetXY(MousePosition(), out int x, out int y);

            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if(path != null)
            {
                for(int i = 0; i < path.Count - 1; i++) 
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, MousePosition());
    }

    Vector3 MousePosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        return mouseWorldPosition;  
    }

}
