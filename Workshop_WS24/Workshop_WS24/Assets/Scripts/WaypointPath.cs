using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public Transform[] waypoints;

    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2) return;
        for (int i = 0; i < waypoints.Length - 1; i++)
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
    }
}
