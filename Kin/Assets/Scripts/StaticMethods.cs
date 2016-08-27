using UnityEngine;
using System.Collections;

public class StaticMethods {

    /// <summary>
    /// returns a vector 3 of the supplied vector 2
    /// </summary>
    /// <param name="v2"></param>
    /// <returns>Vector 3</returns>
    public static Vector3 V2toV3 (Vector2 v2)
    {
        return new Vector3(v2.x, v2.y);
    }

    public static Vector3 V2toV3 (Vector2 v2, float cameraZ)
    {
        return new Vector3(v2.x, v2.y, cameraZ);
    }

    public static Vector3 ChangeZ (Vector3 v3, float z)
    {
        return new Vector3(v3.x, v3.y, z);
    }

}
