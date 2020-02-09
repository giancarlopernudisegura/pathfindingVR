using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWorld : MonoBehaviour
{
    public static float Size = 8;
    public List<Vector3> Walls = new List<Vector3>();
    public GridNode StartNode;
    public Vector3 StartLoc;
    public Vector3 Goal;
    public static float GridUnit = 1;
    public GameObject Plane;
    public GameObject WallObj;
    public GameObject GoalObj;
    public GameObject Node;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlane();
        SpawnWalls();
        SpawnGoal();
        SpawnGrid();
        DisplayNodes();
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
            WallObj.transform.localScale = new Vector3(GridUnit, 1, GridUnit);
            Instantiate(WallObj, worldVector, Quaternion.identity);
        }
    }

    private void SpawnGoal()
    {
        Instantiate(GoalObj, ConvertToWorld(Goal), Quaternion.identity);
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

        StartNode = nodeDictionary[StartLoc];
    }

    private void DisplayNodes()
    {
        foreach (GridNode n in StartNode.Neighbors)
        {
            Vector3 v = ConvertToWorld(n.Location);
            v.y -= .5f - (Node.transform.localScale.y / 2);
            Instantiate(Node, v, Quaternion.identity);
        }
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