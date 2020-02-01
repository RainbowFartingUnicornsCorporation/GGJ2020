using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBehaviour : MonoBehaviour, IInteraction
{
    public float timeBeforeStarting = 20;


    public Animator animator;
    private int timeToHitSecondKey = 0;
    private KeyCode keyCode;
    public int timeToWait = 10;
    public int timeToHit = 20;



    public AudioSource heartbeat;
    public float bpm = 60;
    public float acceleration = 5;
    public float deltaPush = 0.2f;
    private float secondBtwBeat;
    private float timePassed;
    private bool flag;
    private int score;
    
    void Start()
    {
        secondBtwBeat = 60.0f / bpm;
        score = 18;
    }
    
    void Update()
    {
        // Try to Hit
        if (timeToHitSecondKey <= 0)
        {
            timeToHitSecondKey = 0;
        }
        else
        {
            if (timeBeforeStarting > 0)
            {
                timeBeforeStarting = 0;
            }
            timeToHitSecondKey--;
        }

        //Wait a bit to start
        if (timeBeforeStarting > 0 && timeToHitSecondKey == 0)
        {
            timeBeforeStarting -= Time.deltaTime;
            return;
        }

        timePassed += Time.deltaTime;
        if (timePassed > secondBtwBeat)
        {
            UpdateScore();
            flag = true;
            PlayHeartbeat();
            timePassed = 0;
        }

        print(score);
        if (score < 0)
        {
            animator.SetTrigger("Explode");
            print("Perdu");
        }
        else if (score > 30)
        {
            print("Gagné");
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
        return Mathf.Abs(timePassed - secondBtwBeat) / secondBtwBeat <= deltaPush || timePassed / secondBtwBeat <= deltaPush;
    }

    public void HitIt()
    {
        if (HasCorrectlyHit())
        {
            flag = false;
        }
        else
        {
            Accelerate();
        }
    }

    void UpdateScore()
    {
        if (flag)
        {
            score -= 1;
        }
        else
        {
            score += 1;
        }
    }

    public void PlayHeartbeat()
    {
        heartbeat.Stop();
        heartbeat.Play();
    }

    public void KeyPressedAction(KeyCode kc)
    {
        if (timeToHitSecondKey == 0)
        {
            timeToHitSecondKey = timeToWait + timeToHit;
            keyCode = kc;
        }
        else if (keyCode != kc)
        {
            if (timeToHitSecondKey <= timeToHit)
            {
                animator.SetBool("Beat", true);
                HitIt();
            }
            timeToHitSecondKey = 0;
        }
    }
}
