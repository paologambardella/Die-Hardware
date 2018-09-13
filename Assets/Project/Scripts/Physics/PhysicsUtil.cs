using UnityEngine;
using System.Collections;

public static class PhysicsUtil 
{
    public static bool DoBoxesIntersect(Vector3 a, float aWidth, float aHeight, Vector3 b, float bWidth, float bHeight)
    {
        return DoBoxesIntersect(a.x, a.z, aWidth, aHeight, b.x, b.z, bWidth, bHeight);
    }

    public static bool DoBoxesIntersect(Vector3 aPos, Vector3 aSize, Vector3 bPos, Vector3 bSize)
    {
        return DoBoxesIntersect(aPos.x, aPos.z, aSize.x, aSize.z, bPos.x, bPos.z, bSize.x, bSize.z);
    }

    public static bool DoBoxesIntersect(float ax, float az, float aWidth, float aDepth, float bx, float bz, float bWidth, float bDepth)
    {
        float xDiff = ax - bx;
        xDiff = (xDiff < 0f) ? -xDiff : xDiff;

        float zDiff = az - bz;
        zDiff = (zDiff < 0f) ? -zDiff : zDiff;

        return (xDiff < (aWidth + bWidth) / 2f) &&
            (zDiff < (aDepth + bDepth) / 2f);
    }
}
