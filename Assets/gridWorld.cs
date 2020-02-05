using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridWorld : MonoBehaviour
{
    public int size;
    public List<Vector2> walls;
    public Vector3 start;
    public Vector3 goal;
    public int gridUnit = 1;

    // Start is called before the first frame update
    void Start()
    {
        size = 8;
        walls = new List<Vector2>();
        start = new Vector3(0, 0, 0);
        goal = new Vector3(7, 0, 7);

        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = Vector3.zero;
        int planeSize = gridUnit * this.size;
        plane.transform.localScale = new Vector3(planeSize, 1, planeSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}