using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWorld : MonoBehaviour
{
    public static float Size = 8;
    public List<Vector3> Walls = new List<Vector3>();
    public GridNode CurrentNode;
    public Vector3 StartLoc;
    public Vector3 Goal;
    public static float GridUnit = 4;
    public GameObject Plane;
    public GameObject WallObj;
    public GameObject GoalObj;
    public GameObject Node;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlane();
        SpawnWalls();
        SpawnGoal();
        SpawnGrid();
        DisplayNodes();
        CameraToPlayer(StartLoc);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPlane()
    {
        float planeSize = GridUnit * Size;
        Plane.transform.localScale = new Vector3(planeSize, 1, planeSize);
        Instantiate(Plane, Vector3.zero, Quaternion.identity);
    }

    private void SpawnWalls()
    {
        foreach (Vector3 wallVtr in Walls)
        {
            Vector3 worldVector = ConvertToWorld(wallVtr);
            float height = GridUnit * 3;
            WallObj.transform.localScale = new Vector3(GridUnit, height, GridUnit);
            worldVector.y = (1 + height) / 2;
            Instantiate(WallObj, worldVector, Quaternion.identity);
        }
    }

    private void SpawnGoal()
    {
        Vector3 v = ConvertToWorld(Goal);
        float height = GridUnit * 2;
        float width = GridUnit * .6f;
        v.y = 1 + (height / 2);
        GoalObj.transform.localScale = new Vector3(width, height, width);
        Instantiate(GoalObj, v, Quaternion.identity);
    }

    private void SpawnGrid()
    {
        Dictionary<Vector3, GridNode> nodeDictionary = new Dictionary<Vector3, GridNode>();

        // create grid node
        for (byte i = 0; i < Size; i++)
        {
            for (byte j = 0; j < Size; j++)
            {
                Vector3 v = new Vector3(i, 1, j);
                if (!IsObstacle(v))
                {
                    GridNode g = new GridNode(null, v);
                    nodeDictionary[v] = g;
                }
            }
        }

        // add neighbors
        foreach (Vector3 v in nodeDictionary.Keys)
        {
            // add up
            if (nodeDictionary.ContainsKey(v + Vector3.forward))
            {
                nodeDictionary[v].Neighbors.Add(nodeDictionary[v + Vector3.forward]);
            }
            // add down
            if (nodeDictionary.ContainsKey(v + Vector3.back))
            {
                nodeDictionary[v].Neighbors.Add(nodeDictionary[v + Vector3.back]);
            }
            // add right
            if (nodeDictionary.ContainsKey(v + Vector3.right))
            {
                nodeDictionary[v].Neighbors.Add(nodeDictionary[v + Vector3.right]);
            }
            // add left
            if (nodeDictionary.ContainsKey(v + Vector3.left))
            {
                nodeDictionary[v].Neighbors.Add(nodeDictionary[v + Vector3.left]);
            }
        }
        CurrentNode = nodeDictionary[StartLoc];
    }

    private void DisplayNodes()
    {
        foreach (GridNode n in CurrentNode.Neighbors)
        {
            Vector3 v = ConvertToWorld(n.Location);
            float width = .8f * GridUnit;
            float height = .2f * GridUnit;
            Node.transform.localScale = new Vector3(width, height, width);
            v.y -= (1 - Node.transform.localScale.y / 2);
            Instantiate(Node, v, Quaternion.identity);
        }
    }

    public void CameraToPlayer(Vector3 GridLocation)
    {
        Vector3 WorldLocation = ConvertToWorld(GridLocation);
        //Player.transform.position.x = WorldLocation.x;
        //Player.transform.position.z = WorldLocation.z;
    }

    public static Vector3 ConvertToWorld(Vector3 input)
    {
        Vector3 output = Vector3.zero;
        output.x = (input.x * GridWorld.GridUnit) - (GridWorld.GridUnit * GridWorld.Size / 2) + (GridWorld.GridUnit / 2);
        output.z = (input.z * GridWorld.GridUnit) - (GridWorld.GridUnit * GridWorld.Size / 2) + (GridWorld.GridUnit / 2);
        output.y = 1;
        return output;
    }

    public bool IsObstacle(Vector3 v)
    {
        foreach (Vector3 wallV in Walls)
        {
            if (wallV.x == v.x && wallV.z == v.z)
            {
                return true;
            }
        }
        return false;
    }
}
