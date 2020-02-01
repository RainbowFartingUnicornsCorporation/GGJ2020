using UnityEngine;

public class PathBehaviour : MonoBehaviour, IInteraction
{
    public PathFollower pathFollower;
    public AnchorEventBroadcaster anchorEventBroadcaster;

    public void KeyPressedAction()
    {
        pathFollower.paused = !pathFollower.paused;
        if (pathFollower.CurrentWayPointId <= 0)
        {
            pathFollower.CurrentWayPointId = pathFollower.MaxPointNumber - 1;
            anchorEventBroadcaster.BoadcastDetach();
        }
    }

    void Update()
    {
        //pathFollower.paused = false;
    }
}
