using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConvertUtils
{
    public static Vector3 ToUnityVec3(System.Numerics.Vector3 vec3)
    {
        return new Vector3(vec3.X, vec3.Y, vec3.Z);
    }
    public static System.Numerics.Vector3 ToSysVec3(Vector3 vec3)
    {
        return new System.Numerics.Vector3(vec3.x, vec3.y, vec3.z);
    }
}
