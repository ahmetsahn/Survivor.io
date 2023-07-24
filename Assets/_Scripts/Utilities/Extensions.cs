using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Quaternion CalculateAngleRotationBetweenPoints(this Vector3 fromPoint, Vector3 toPoint)
    {
        Vector3 difference = toPoint - fromPoint;
        difference.Normalize();
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }

    public static Quaternion CalculateRotationFromVelocity(this Rigidbody2D rb)
    {
        var velocity = rb.velocity;
        var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

  
}
