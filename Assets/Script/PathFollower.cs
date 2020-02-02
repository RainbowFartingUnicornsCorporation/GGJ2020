using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public PathDrawer2 pathToFollow;
    public bool paused = false;

    public int CurrentWayPointId = 0;
    public int MaxPointNumber = 0;
    private float speed = 5.0f;
    private float reachDistance = 1.0f;

    private void Start()
    {
        MaxPointNumber = pathToFollow.path_objs.Count;
    }

    void Update()
    {
        if (paused) return;

        var nextPosition = pathToFollow.path_objs[CurrentWayPointId].position;
        float distance = Vector3.Distance(nextPosition, transform.position);

        if (distance <= reachDistance)
            CurrentWayPointId++;

        if (CurrentWayPointId >= pathToFollow.path_objs.Count)
        {
            CurrentWayPointId = 0;
            transform.position = pathToFollow.path_objs[CurrentWayPointId].position;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * speed);
        }

            
    }
}
