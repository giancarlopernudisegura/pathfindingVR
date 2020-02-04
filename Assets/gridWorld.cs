using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridWorld : MonoBehaviour
{
    public int size;
    public List<Vector2> walls;
    public Vector2 start;
    public Vector2 goal;

    public gridWorld(int size, List<Vector2> walls, Vector2 start, Vector2 goal)
    {
        this.size = size;
        this.walls = walls;
        this.start = start;
        this.goal = goal;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}