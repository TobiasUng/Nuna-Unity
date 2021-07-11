using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle : MonoBehaviour
{

   
    public static float AngleDir(Vector3 forward, Vector3 targetDir, Vector3 up)
    {
        Vector3 perpendicular = Vector3.Cross(forward, targetDir);
        float dir = Vector3.Dot(perpendicular, up);

        return dir;
    }


    public static float AngleDir(Transform player, Transform target)
    {

        Vector3 targetDir = (player.transform.position - target.transform.position).normalized;
        Vector3 perpendicular = Vector3.Cross(player.transform.forward, targetDir);
        float dir = Vector3.Dot(perpendicular, player.transform.up);

        return dir;
    }
}
