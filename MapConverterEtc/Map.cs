using Godot;
using System;

public class Map
{
    public MapImage[] images;

    public Func<Vector3, Vector2> GlobeToMapTransform;
    public Func<Vector2, Vector3> MapToGlobeTransform;
    public Func<Vector3, int> GlobeToImage;
    public Func<Vector3, MapImage, Vector2I> GlobeToPixel;
}