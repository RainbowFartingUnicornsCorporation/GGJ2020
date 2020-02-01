using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeat : MonoBehaviour
{

    public KeyCode keyHeartbeat;
    public AudioSource heartbeat;
    public float bpm;
    public float acceleration;
    public float deltaPush;

    private float secondBtwBeat;
    private float timePassed;

    private bool flag;
    private int score;

    public void PlayHeartbeat()
    {
        heartbeat.Stop();
        heartbeat.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        secondBtwBeat = 60.0f / bpm;
        score = 10;
    }

    void UpdateScore()
    {
        if (flag)
        {
            score -= 1;
        } else
        {
            score += 1;
        }
    }

    void Accelerate()
    {
        bpm += bpm * (acceleration / 100);
        secondBtwBeat = 60.0f / bpm;
    }

    bool HasCorrectlyHit()
    {
        // two condition to consider the hit after the beginning of the sound
        return Mathf.Abs(timePassed-secondBtwBeat)/secondBtwBeat <= deltaPush || timePassed/secondBtwBeat <= deltaPush;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if( timePassed > secondBtwBeat )
        {
            UpdateScore();
            flag = true;
            PlayHeartbeat();
            timePassed = 0;
        }
        if (Input.GetKeyDown(keyHeartbeat))
        {
            if (HasCorrectlyHit())
            {
                flag = false;
            } else
            {
                Accelerate();
            }
        }
        if (heartbeat.isPlaying)
        {
            //TODO animation to push
        }

        //print(score);
        //print(bpm);

        if(score < 0)
        {
            Time.timeScale = 0;
            print("NOP");
        } else if (score > 30)
        {
            Time.timeScale = 0;
            print("YEP");
        }

    }
}
