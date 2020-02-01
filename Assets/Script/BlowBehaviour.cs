using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowBehaviour : MonoBehaviour
{
    public static float MicLoudness;


    public GameObject blowingTarget;

    private string _device;
    private AudioClip _clipRecord;
    private float currentPos;
    private float cumulativeLoudnessLevel;

    //mic initialization
    void InitMic()
    {
        if (_device == null) _device = Microphone.devices[0];
        _clipRecord = Microphone.Start(_device, true, 999, 44100);
        currentPos = 0.0f;
    }

    void StopMicrophone()
    {
        Microphone.End(_device);
    }


    int _sampleWindow = 128;

    //get data from microphone into audioclip
    float LevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }



    void Update()
    {
        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere
        MicLoudness = LevelMax();
        cumulativeLoudnessLevel += (MicLoudness * 10 -  cumulativeLoudnessLevel/2) * Time.deltaTime;


        Vector3 scaleChange = new Vector3(1 + 2 * MicLoudness,1, 1);
        float rand1 = Random.Range(0.0f, Mathf.PI);
        float rand2 = Random.Range(0.0f, Mathf.PI);
        blowingTarget.transform.localScale = new Vector3(1 + 0.6f * MicLoudness * Mathf.Cos(rand1), 1 + 0.6f * MicLoudness * Mathf.Cos(rand2), 1); ;
        

        if(currentPos > 8)
        {
            print("KABOOOM"); // Success Event
            blowingTarget.GetComponent<Rigidbody>().velocity = new Vector3(5,0,0);
            blowingTarget.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 10);
        }
        else
        {
            blowingTarget.transform.position = new Vector3(-4 + currentPos, 0, 0);
            var rot = blowingTarget.transform.rotation;
            rot.z = Mathf.Cos(currentPos/3);
            blowingTarget.transform.rotation = rot;
            blowingTarget.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        
        if (cumulativeLoudnessLevel > 2)
        {
            print(currentPos);
            currentPos += MicLoudness* 10 * Time.deltaTime;
        }
        

    }

    bool _isInitialized;
    // start mic when scene starts
    void OnEnable()
    {
        InitMic();
        _isInitialized = true;
        cumulativeLoudnessLevel = 0;
    }

    //stop mic when loading a new level or quit application
    void OnDisable()
    {
        StopMicrophone();
    }

    void OnDestroy()
    {
        StopMicrophone();
    }


    // make sure the mic gets started & stopped when application gets focused
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //Debug.Log("Focus");

            if (!_isInitialized)
            {
                //Debug.Log("Init Mic");
                InitMic();
                _isInitialized = true;
            }
        }
        if (!focus)
        {
            //Debug.Log("Pause");
            StopMicrophone();
            //Debug.Log("Stop Mic");
            _isInitialized = false;

        }
    }
}
