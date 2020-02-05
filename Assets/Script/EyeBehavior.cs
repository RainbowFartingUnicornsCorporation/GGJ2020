using UnityEngine;

public class EyeBehavior : MonoBehaviour, IInteraction
{
    public AnchorEventBroadcaster anchorEventBroadcaster;

    private GameObject upCube;
    private GameObject downCube;
    public float eyeOpenness;
    private float lockTime = 0;
    private float startPosZ;


    // Start is called before the first frame update
    void Start()
    {
        upCube = this.transform.GetChild(0).gameObject;
        downCube = this.transform.GetChild(1).gameObject;
        startPosZ = upCube.transform.position.z;
    }

    public void KeyPressedAction(KeyCode kc)
    {
        eyeOpenness = eyeOpenness - eyeOpenness / 4;
        if (eyeOpenness < 1.5)
        {
            eyeOpenness = 0;
            lockTime = Random.Range(1, 5);
        }
    }

    void UpdateEyePosition()
    {
        float opennessFactor = 5 * (-1f * Mathf.Cos(Mathf.PI * eyeOpenness / 10) + 1f);
        Vector3 upPos = new Vector3(0, 10 - opennessFactor, startPosZ);
        Vector3 downPos = new Vector3(0, -10 + opennessFactor, startPosZ);
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
            anchorEventBroadcaster.BroadcastDetach();
            eyeOpenness = 5;
        }
        else
        {
            lockTime -= Time.deltaTime;
            if (lockTime <= 0)
                eyeOpenness += Time.deltaTime;
        }
        UpdateEyePosition();
    }
}
