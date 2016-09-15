using UnityEngine;
using System.Collections;

public class StaticMethods {

    /// <summary>
    /// Returns a vector 3 of the supplied vector 2.
    /// </summary>
    /// <param name="v2"></param>
    /// <returns>Vector 3</returns>
    public static Vector3 V2toV3 (Vector2 v2)
    {
        return new Vector3(v2.x, v2.y);
    }

	/// <summary>
	/// Returns a Vector3 created from a Vector2 with the given z value.
	/// </summary>
	/// <returns>The Vector3 created.</returns>
	/// <param name="v2">V2.</param>
	/// <param name="cameraZ">The z coordinate.</param>
    public static Vector3 V2toV3 (Vector2 v2, float cameraZ)
    {
        return new Vector3(v2.x, v2.y, cameraZ);
    }

	/// <summary>
	/// Changes the z of a vector 3.
	/// </summary>
	/// <returns>The Vector3 with the edited z value.</returns>
	/// <param name="v3">Vector</param>
	/// <param name="z">The z coordinate.</param>
    public static Vector3 ChangeZ (Vector3 v3, float z)
    {
        return new Vector3(v3.x, v3.y, z);
    }

	/// <summary>
	/// Calculates the distance between the vectors v1 and v2.
	/// </summary>
	/// <returns>The distance between the given vectors.</returns>
	/// <param name="v1">Vector 1</param>
	/// <param name="v2">Vector 2</param>
	public static float Distance(Vector2 v1, Vector2 v2)
	{
		return (v2 - v1).magnitude;
	}

	/// <summary>
	/// Calculates the distance between the vectors v1 and v2.
	/// </summary>
	/// <returns>The distance between the given vectors.</returns>
	/// <param name="v1">Vector 1</param>
	/// <param name="v2">Vector 2</param>
	public static float Distance(Vector3 v1, Vector3 v2)
	{
		return (v2 - v1).magnitude;
	}

}
