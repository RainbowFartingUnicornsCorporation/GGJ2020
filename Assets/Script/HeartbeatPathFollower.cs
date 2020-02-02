using UnityEngine;

public class HeartbeatPathFollower : MonoBehaviour
{
    public PathDrawer2 pathToFollow;

    public int CurrentWayPointId = 0;
    private int MaxPointNumber = 0;

    private float pathSize;
    private bool started = false;
    public float timeToTravel = 0;

    private void Start()
    {
        MaxPointNumber = pathToFollow.path_objs.Count;
        pathSize = pathToFollow.GetSize();
    }

    public void Enable()
    {
        started = true;
        CurrentWayPointId = 0;
        transform.position = new Vector3(pathToFollow.path_objs[0].position.x, pathToFollow.path_objs[0].position.y, transform.position.z);
    }

    public void Disable()
    {
        if (started)
        {
            started = false;
            transform.position = new Vector3(1000, 1000, transform.position.z);
        }
    }

    void Update()
    {
        if (!started)
            return;
        if (timeToTravel == 0)
            return;

        var nextPosition = pathToFollow.path_objs[CurrentWayPointId + 1].position;
        float speed = pathSize / timeToTravel;
        float availableDistance = Time.deltaTime * speed;
        while (availableDistance > 0)
        {
            float distance = Vector3.Distance(transform.position, nextPosition);
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, availableDistance);
            availableDistance -= distance;
            if (transform.position == nextPosition)
            {
                CurrentWayPointId++;
                if (CurrentWayPointId + 1 >= pathToFollow.path_objs.Count)
                {
                    CurrentWayPointId = 0;
                    transform.position = new Vector3(pathToFollow.path_objs[0].position.x, pathToFollow.path_objs[0].position.y, transform.position.z);
                    break;
                }
                nextPosition = pathToFollow.path_objs[CurrentWayPointId + 1].position;
            }
        }
        /*

        //var lastPosition = pathToFollow.path_objs[CurrentWayPointId].position;
        var nextPosition = pathToFollow.path_objs[CurrentWayPointId + 1].position;
        //distance = Vector3.Distance(lastPosition, nextPosition);

        if (CurrentWayPointId + 1 >= pathToFollow.path_objs.Count)
        {
            CurrentWayPointId = 0;
            transform.position = new Vector3(pathToFollow.path_objs[0].position.x, pathToFollow.path_objs[0].position.y, transform.position.z);
        }
        else
        {
            float speed = pathSize / timeToTravel;

            float availableDistance = Time.deltaTime * speed;
            float distance = Vector3.Distance(transform.position, nextPosition);
            while (availableDistance > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, distance);
                availableDistance -= distance;
                if (transform.position == nextPosition)
                {
                    CurrentWayPointId++;
                    nextPosition = pathToFollow.path_objs[CurrentWayPointId + 1].position;
                }
            }
        }  */          
    }
}
