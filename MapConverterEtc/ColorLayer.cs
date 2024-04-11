using Godot;
using System;
using System.Collections.Generic;

public class ColorLayer
{
    public string name;
    public Color layerColor;
    public List<Color> colors;

    public void AddSlots(int count, Color color)
    {
        for (int i = 0; i < count; i++)
        {
            colors.Add(color);
        }
    }
}
