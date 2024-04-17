using System.Collections.Generic;
using System.Numerics;

public class CubicSpacePartition<T>
{
    private List<T>[,,] cells;
    Vector3 dimensions;
    Vector3 centerPos;
    int cellWidth;
    public CubicSpacePartition(Vector3 dimensionsI, Vector3 centerPosI, int cellWidthI)
    {
        this.dimensions = dimensionsI;
        this.centerPos = centerPosI;
        this.cellWidth = cellWidthI;
        cells = new List<T>[cellWidth,cellWidth,cellWidth];
        for(int i = 0; i < cellWidth; i++)
        {
            
        }
    }
}