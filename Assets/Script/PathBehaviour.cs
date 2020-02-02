using UnityEngine;

public class PathBehaviour : MonoBehaviour, IInteraction
{
    public PathFollower pathFollower;
    public AnchorEventBroadcaster anchorEventBroadcaster;

    public void KeyPressedAction(KeyCode kc)
    {
        pathFollower.paused = !pathFollower.paused;
    }
}
