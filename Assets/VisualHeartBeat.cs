using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualHeartBeat : MonoBehaviour
{
    public List<Transform> path_objs = new List<Transform>();
    Transform[] transforms;

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
