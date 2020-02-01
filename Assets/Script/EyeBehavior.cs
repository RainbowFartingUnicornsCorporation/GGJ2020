using UnityEngine;

public class EyeBehavior : MonoBehaviour, IInteraction
{
    public AnchorEventBroadcaster anchorEventBroadcaster;

    private GameObject upCube;
    private GameObject downCube;
    public float eyeOpenness;


    // Start is called before the first frame update
    void Start()
    {
        upCube = this.transform.GetChild(0).gameObject;
        downCube = this.transform.GetChild(1).gameObject;
    }

    public void KeyPressedAction(KeyCode kc)
    {
        eyeOpenness = eyeOpenness - eyeOpenness / 4;
        if (eyeOpenness < 1.5)
            eyeOpenness = 0;
    }

    void UpdateEyePosition()
    {
        float opennessFactor = 5 * (-1f * Mathf.Cos(Mathf.PI * eyeOpenness / 10) + 1f);
        Vector3 upPos = new Vector3(0, 10 - opennessFactor, 5);
        Vector3 downPos = new Vector3(0, -10 + opennessFactor, 5);
        upCube.transform.position = upPos;
        downCube.transform.position = downPos;
    }


    // Update is called once per frame
    void Update()
    {
        if (eyeOpenness == 5)
        {
            return; // Eye closed
        }
        if (eyeOpenness > 5)
        {
            anchorEventBroadcaster.BoadcastDetach();
            eyeOpenness = 5;
        }
        else
        {
            eyeOpenness += Time.deltaTime;
        }
        UpdateEyePosition();
    }
}
