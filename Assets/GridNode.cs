using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode
{
    public bool Visible = true;
    public Vector3 Location;
    public List<GridNode> Neighbors = new List<GridNode>();
    public GridNode Parent = null;
    public ushort GScore = 0;

    // constructor
    public GridNode(GridNode _parent, Vector3 loc)
    {
        this.Parent = _parent;
        this.Location = loc;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}