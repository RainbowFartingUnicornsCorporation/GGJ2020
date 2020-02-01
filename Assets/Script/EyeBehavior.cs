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
        Vector3 upPos = new Vector3(0, 10 - eyeOpenness, 0);
        Vector3 downPos = new Vector3(0, -10 + eyeOpenness, 0);
        upCube.transform.position = upPos;
        downCube.transform.position = downPos;
    }


    // Update is called once per frame
    void Update()
    {
        eyeOpenness += Time.deltaTime;
        print(eyeOpenness);
        if (Input.GetKeyDown(keyOpenEye))
        {
            eyeOpenness = eyeOpenness - eyeOpenness / 4;
        }

        UpdateEyePosition();
    }
}
