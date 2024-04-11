using Godot;
using System;
using System.Collections.Generic;

public class PoxelMap
{
    private List<Poxel> poxels;
    public int LayerCount { get; private set; }

    public PoxelMap(int layerCount)
    {
        this.LayerCount = layerCount;
        poxels = new List<Poxel>();
    }

    public void AddPoxel()
    {
        
    }

    public void RemovePoxel()
    {

    }

    public void ChangeColor(Color color, int layer)
    {

    }
}
