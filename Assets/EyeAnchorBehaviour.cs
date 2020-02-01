using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAnchorBehaviour : AnchorBehaviour
{
    public BoxCollider floor;

    public override void Kill()
    {
        print("aaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        floor.enabled = false;
        base.Kill();
    }
}
