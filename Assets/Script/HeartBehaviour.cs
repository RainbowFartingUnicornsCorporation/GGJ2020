using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartBehaviour : MonoBehaviour, IInteraction
{
    public Animator animator;
    private KeyCode keyCode;
    private float timeToHitSecondKey = 0;
    public float timeToWait = 0.4f;
    public float timeToHit = 1;
    public AnchorEventBroadcaster aeb;

    public Transform anchorLeft;
    public Transform anchorRight;
    public GameObject particle;

    private bool playing = true;
    private bool notStarted = true;

    public int goal = 5;

    public HeartbeatPathFollower hpf;

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

    private int score = 0;
    
    void Start()
    {
        secondBtwBeat = 60.0f / bpm;
        if (PlayerPrefs.GetInt("BrainWon", 0) == 0)
        {
            particle.SetActive(false);
        }
    }
    
    void Update()
    {
        if (!playing)
            return;

        //print(timeToHitSecondKey);

        // Try to Hit
        if (timeToHitSecondKey <= 0)
        {
            timeToHitSecondKey = 0;
        }
        else
        {
            if (notStarted)
            {
                notStarted = false;
            }
            timeToHitSecondKey -= Time.deltaTime;
        }

        //Wait a bit to start
        if (notStarted)
        {
            return;
        }

        timePassed += Time.deltaTime;

        hpf.timeToTravel = secondBtwBeat;
        
        if (timePassed > secondBtwBeat)
        {
            if (flag)
            {
                Accelerate();
                score = 0;
            }
            flag = true;
            timePassed = 0;
            bip.PlayDelayed(secondBtwBeat/2);
            hpf.Enable();
        }

        //print(score);
        if (score >= goal)
        {
            print("Gagné");
            SceneManager.LoadScene("VictoryScreen", LoadSceneMode.Single);
        }
    }

    void Accelerate()
    {
        animator.SetTrigger("Suffer");
        bpm += bpm * (acceleration / 100);
        secondBtwBeat = 60.0f / bpm;
        if (bpm > 210)
            Lose();
    }

    void Decelerate()
    {
        if (bpm > 80)
        bpm -= bpm * (acceleration / 100);
        secondBtwBeat = 60.0f / bpm;
        if (bpm > 210)
            Lose();
    }

    bool HasCorrectlyHit()
    {
        return timePassed > secondBtwBeat / 2 - deltaPush * secondBtwBeat && timePassed < secondBtwBeat / 2 + deltaPush * secondBtwBeat;
        // two condition to consider the hit after the beginning of the sound
        //return (secondBtwBeat - timePassed) / secondBtwBeat <= deltaPush || timePassed / secondBtwBeat <= deltaPush;
    }

    void Lose()
    {
        if (!playing)
            return;
        death.Play();
        playing = false;
        animator.SetTrigger("Explode");
        aeb.Kill();
        hpf.Disable();
        print("Perdu");
        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu()
    {
        PlayerPrefs.DeleteAll();
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
    }

    public void HitIt()
    {
        if (HasCorrectlyHit())
        {
            Decelerate();
            score++;
            flag = false;
            animator.SetBool("Beat", true);
            beat2.Play();
            print("HIT");
        }
        else
        {
            score = 0;
            Accelerate();
            print("FAILED");
        }
    }

    public void KeyPressedAction(KeyCode kc)
    {
        if (timeToHitSecondKey == 0)
        {
            timeToHitSecondKey = timeToWait + timeToHit;
            keyCode = kc;
            beat1.Play();
            hpf.timeToTravel = secondBtwBeat;
            //hpf.Enable();
        }
        else if (keyCode != kc)
        {
            if (timeToHitSecondKey <= timeToHit)
            {
                HitIt();
            }
            timeToHitSecondKey = 0;
        }
    }
}
