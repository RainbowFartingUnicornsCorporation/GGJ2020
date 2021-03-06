﻿using System.Collections.Generic;
using UnityEngine;

public class PathDrawer2 : MonoBehaviour
{
    public List<Transform> path_objs = new List<Transform>();
    Transform[] transforms;

    public float GetSize()
    {
        float totalDistance = 0;
        Transform previous = null;
        foreach (var path_obj in path_objs)
        {
            if (previous)
            {
                totalDistance += Vector3.Distance(previous.position, path_obj.position);
            }
            previous = path_obj;
        }
        return totalDistance;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        transforms = GetComponentsInChildren<Transform>();
        path_objs.Clear();

        foreach (var path_obj in transforms)
        {
            if (path_obj != this.transform)
                path_objs.Add(path_obj);
        }

        for (int i = 0; i < path_objs.Count; i++)
        {
            var position = path_objs[i].position;
            if (i > 0)
            {
                var previous = path_objs[i - 1].position;
                Gizmos.DrawLine(previous, position);
                Gizmos.DrawWireSphere(position, 0.3f);
            }
        }
    }
}
