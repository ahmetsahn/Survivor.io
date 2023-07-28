using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void LookAtMouse(this Transform transform)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static void RotateFromMovementDirection(this Transform transform)
    {
        Vector3 movementDirection = transform.forward;
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


}
