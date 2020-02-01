using UnityEngine;

public class AnchorEventBroadcaster : MonoBehaviour
{
    public AnchorBehaviour[] ObservedAnchors;

    public void BoadcastDetach()
    {
        foreach (var anchor in ObservedAnchors)
        {
            anchor.DetachKey();
        }
    }
}
