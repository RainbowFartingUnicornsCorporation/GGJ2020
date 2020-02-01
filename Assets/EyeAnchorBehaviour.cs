using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAnchorBehaviour : AnchorBehaviour
{
    public BoxCollider floor;

    public override void Kill()
    {
        floor.enabled = false;
        base.Kill();
    }
}
