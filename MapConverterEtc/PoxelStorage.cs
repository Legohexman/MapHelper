using Godot;
using System;
using System.Collections.Generic;

public struct PoxelStorage
{
    //make a default gradient per layer situation

    private List<Vector3> points;
    private List<ColorLayer> colorLayers;

    public void AddPoxel(Vector3 point)
    {
        points.Add(point);
        foreach(ColorLayer layer in colorLayers)
        {
            layer.colors.Add(Color.Color8(0,0,0));//for now default color for all layers is black
        }
    }

    public void RemovePoxel(int index)
    {
        points.RemoveAt(index);
        foreach (ColorLayer layer in colorLayers)
        {
            layer.colors.RemoveAt(index);//for now default color for all layers is black
        }
    }

    public void SetColor(int index, int layer, Color newColor)
    {
        colorLayers[layer].colors[index] = newColor;
    }

    public void AddLayer(Color layerColor, string layerName)
    {
        ColorLayer layer = new ColorLayer();
        layer.name = layerName;
        layer.layerColor = layerColor;
        for(int i = 0; i < points.Count; i++)
        {
            //layer.
        }

        //colorLayers.Add(new ColorLayer())
    }
}