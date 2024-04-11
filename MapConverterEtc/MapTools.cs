using Godot;
using System;

public static class MapTools
{
    /// <summary>
    /// out x is longitude, out y is latitute. In vec3: x is center of map, y is east, z is north
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static Vector2 SphereToEquirect(Vector3 pos)
    {
        float latitude = Mathf.Asin(pos.Z);

        float xyMagnitude = Mathf.Sqrt((pos.X * pos.X) + (pos.Y * pos.Y));

        float longitude = Mathf.Acos(pos.X / xyMagnitude);


        Vector2 output = new Vector2(longitude, latitude);

        return output;
    }

    /// <summary>
    /// in x is longitude, out y is latitute. out vec3: x is center of map (prime meridian meets equator), y is east, z is north
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static Vector3 EquirectToSphere(Vector2 pos)
    {
        //norm x and y are just the xy direction vector that the point is in
        float normX = Mathf.Cos(pos.X);
        float normY = Mathf.Sin(pos.X);
        float Z = Mathf.Sin(pos.Y);
        
        //x and y scaled to match where the point is on the sphere
        float scaledX = normX * Mathf.Cos(pos.Y);
        float scaledY = normY * Mathf.Cos(pos.Y);


        Vector3 output = new Vector3(scaledX,scaledY,Z);

        return output;
    }

    public static Vector3 EquirectToSphere(Vector2 pos, Vector2 bounds, Vector2 center)
    {
        Vector2 posAdjusted = pos - center; //centers
        posAdjusted /= bounds; //makes from -0.5 to 0.5
        posAdjusted *= new Vector2(Mathf.Pi * 2f, Mathf.Pi);

        //norm x and y are just the xy direction vector that the point is in
        float normX = Mathf.Cos(posAdjusted.X);
        float normY = Mathf.Sin(posAdjusted.X);
        float Z = Mathf.Sin(posAdjusted.Y);

        //x and y scaled to match where the point is on the sphere
        float scaledX = normX * Mathf.Cos(posAdjusted.Y);
        float scaledY = normY * Mathf.Cos(posAdjusted.Y);


        Vector3 output = new Vector3(scaledX, scaledY, Z);

        return output;
    }

    /// <summary>
    /// in the input vector, x is center of map, y is east, and z is north. Output coords are put on a cartesian plane (for making a pixel image)
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static Vector2 SphereToEquiAzimuthalRect(Vector3 pos)
    {
        Vector3 posNorm = pos.Normalized();

        //surface distance
        float dist = Mathf.Acos(posNorm.X);//cos ^{ -1}\left(a\right);

        float yzMagnitude = Mathf.Sqrt(posNorm.Y * posNorm.Y + posNorm.Z * posNorm.Z);

        //angle around center where north is "y" and east is "x"
        //!!!!!!!!!!THE "RETURNS 0 IF 0" ASPECT OF PosOrNegNoZero(x) IS POTENTIALLY PROBLEMATIC (Brobably not actually, nvr mind)
        float dir = Mathf.Acos(posNorm.Y/yzMagnitude) * PosOrNegNoZero(posNorm.Z);
        Vector2 output = new Vector2(dist * Mathf.Cos(dir), dist * Mathf.Sin(dir));
        return output;
    }

    /// <summary>
    /// in the input vector, x is center of map, y is east, and z is north. Output: x is radius, y is angle
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static Vector2 SphereToEquiAzimuthal(Vector3 pos)
    {
        Vector3 posNorm = pos.Normalized();

        //surface distance
        float dist = Mathf.Acos(posNorm.X);//cos ^{ -1}\left(a\right);

        float yzMagnitude = Mathf.Sqrt(posNorm.Y * posNorm.Y + posNorm.Z * posNorm.Z);

        //angle around center where north is "y" and east is "x"
        //!!!!!!!!!!THE "RETURNS 0 IF 0" ASPECT OF PosOrNegNoZero(x) IS POTENTIALLY PROBLEMATIC (Brobably not actually, nvr mind)
        float dir = Mathf.Acos(posNorm.Y / yzMagnitude) * PosOrNegNoZero(posNorm.Z);
        Vector2 output = new Vector2(dist * Mathf.Cos(dir), dist * Mathf.Sin(dir));
        return output;
    }



    /// <summary>
    /// Incomplete!!!!!
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static Vector3 EquiAzimuthalToSphere(Vector2 pos)
    {
        return Vector3.Zero;
    }

    //public static Texture2D convert

    public static float PosOrNegNoZero(float input)
    {
        float output = 1f;

        if(input < 0f)
        {
            output = -1f;
        }
        
        return output;
    }
}

/// <summary>
/// Vector 3 double
/// </summary>
struct Vector3D
{
    //Incomplete
    double x, y, z;
}

/// <summary>
/// Vector 2 double
/// </summary>
struct Vector2D
{
    //Incomplete
    double x, y;
}