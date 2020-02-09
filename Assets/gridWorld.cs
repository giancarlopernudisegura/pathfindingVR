using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridWorld : MonoBehaviour
{
    public float size;
    public List<Vector3> walls;
    public Vector3 start;
    public Vector3 goal;
    public float gridUnit = 1;
    public GameObject plane;
    public GameObject wallObj;
    public GameObject goalObj;

    // Start is called before the first frame update
    void Start()
    {
        size = 8;
        walls = new List<Vector3>();
        for (int i = 0; i < size; i += 2)
        {
            walls.Add(new Vector3(i, 1, i));
        }
        start = new Vector3(0, 0, 0);
        goal = new Vector3(3, 0, 7);

        float planeSize = gridUnit * this.size;
        plane.transform.localScale = new Vector3(planeSize, 1, planeSize);
        Instantiate(plane, Vector3.zero, Quaternion.identity);

        foreach (Vector3 wallVtr in walls)
        {
            Vector3 worldVector = ConvertToWorld(wallVtr);
            wallObj.transform.localScale = new Vector3(gridUnit, 1, gridUnit);
            Instantiate(wallObj, worldVector, Quaternion.identity);
        }

        Instantiate(goalObj, ConvertToWorld(goal), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 ConvertToWorld(Vector3 input)
    {
        Vector3 output = Vector3.zero;
        output.x = (input.x * gridUnit) - (gridUnit * this.size / 2) + (gridUnit / 2);
        output.z = (input.z * gridUnit) - (gridUnit * this.size / 2) + (gridUnit / 2);
        output.y = 1;
        return output;
    }
}