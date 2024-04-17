using Godot;
using System;

public struct Poxel
{
    Vector3 point;
    /// <summary>
    /// color per each layer
    /// </summary>
    Color[] colors;

    public Poxel(Vector3 point, int layerCount, Color initColor)
    {
        
        this.point = point;
        colors = new Color[layerCount];
        colors.Initialize();
    }
}
