using UnityEngine;

public static class Vector3Math
{
    public static Vector2 Mult(this Vector2 a, Vector2 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y);
    }
    public static Vector3 Mult(this Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    public static bool CompareApproximately(this Vector3 a, Vector3 b, float gap)
    {
        return Vector3.SqrMagnitude(a - b) < gap;
    }

    public static Vector3 Direction(this Vector3 pos, Vector3 target)
    {
        return (target - pos).normalized;
    }
}
