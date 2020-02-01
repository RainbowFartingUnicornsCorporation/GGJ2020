using UnityEngine;

public class AnchorEventBroadcaster : MonoBehaviour
{
    public AnchorBehaviour[] ObservedAnchors;

    public void BroadcastDetach()
    {
        foreach (var anchor in ObservedAnchors)
        {
            anchor.DetachKey();
        }
    }

    public void Kill()
    {
        foreach (var anchor in ObservedAnchors)
        {
            anchor.Kill();
        }
    }
}
