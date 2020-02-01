using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBehavior : MonoBehaviour
{
    private GameObject upCube;
    private GameObject downCube;
    public KeyCode keyOpenEye;

    private float eyeOpenness;
    public int test;


    // Start is called before the first frame update
    void Start()
    {
        upCube = this.transform.GetChild(0).gameObject;
        downCube = this.transform.GetChild(1).gameObject;
        eyeOpenness = 0;
    }


    void UpdateEyePosition()
    {
        float opennessFactor = 5 * (-1f * Mathf.Cos( Mathf.PI * eyeOpenness / 10) + 1f);
        Vector3 upPos = new Vector3(0, 10 - opennessFactor, 0);
        Vector3 downPos = new Vector3(0, -10 + opennessFactor, 0);
        upCube.transform.position = upPos;
        downCube.transform.position = downPos;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyOpenEye))
        {
            eyeOpenness = eyeOpenness - eyeOpenness / 4;
        }

        if (eyeOpenness == 5)
            return; // Eye closed
        if( eyeOpenness > 5)
        {
            eyeOpenness = 5;
        }
        else
        {
            eyeOpenness += Time.deltaTime;
        }
        print(eyeOpenness);
        UpdateEyePosition();
    }
}
