using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBehaviour : MonoBehaviour, IInteraction
{
    public float timeBeforeStarting = 20;


    public Animator animator;
    private KeyCode keyCode;
    private float timeToHitSecondKey = 0;
    public float timeToWait = 0.4f;
    public float timeToHit = 1;
    public AnchorEventBroadcaster aeb;

    private bool playing = true;


    public AudioSource beat1;
    public AudioSource beat2;
    public AudioSource bip;
    public AudioSource death;
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
        beat1.volume = 10;
        beat2.volume = 10;
    }
    
    void Update()
    {
        if (!playing)
            return;

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
            timeToHitSecondKey -= Time.deltaTime;
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
            timePassed = 0;
            bip.Play();
        }

        print(score);
        if (score < 0)
        {
            Lose();
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

    void Lose()
    {
        if (!playing)
            return;
        death.Play();
        playing = false;
        animator.SetTrigger("Explode");
        aeb.Kill();
        print("Perdu");
        
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

    public void KeyPressedAction(KeyCode kc)
    {
        if (timeToHitSecondKey == 0)
        {
            timeToHitSecondKey = timeToWait + timeToHit;
            keyCode = kc;
            beat1.Play();
        }
        else if (keyCode != kc)
        {
            if (timeToHitSecondKey <= timeToHit)
            {
                animator.SetBool("Beat", true);
                HitIt();
                beat2.Play();
            }
            timeToHitSecondKey = 0;
        }
    }
}
