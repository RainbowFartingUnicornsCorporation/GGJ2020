using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollBehavior : MonoBehaviour
{

    public AudioSource friction_1;
    public AudioSource friction_2;
    public float penality;
    public float winCoefficient;
    public Animator earStickAnimator;
    
    private float score;



    void ComputeScore(float scrollScore)
    {
        //print(score);
        if(scrollScore == 0f)
        {
            score -= penality;
        }
        else
        {
            score += scrollScore;
        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        score = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float scrollScore = Input.GetAxis("Mouse ScrollWheel");
        float localScore = Mathf.Abs(scrollScore) * winCoefficient;
        ComputeScore(localScore);
        if (!friction_1.isPlaying && !friction_2.isPlaying)
        {
            if (scrollScore > 0f)
            {
                friction_1.Play();

            }
            else if (scrollScore < 0f)
            {
                friction_2.Play();
            }
        }
        print(score);
        earStickAnimator.SetFloat("speedEarStickAnim", localScore / 2);

        if (score >= 200)
        {
            earStickAnimator.SetBool("stickOut", true);
            // WIN

            StartCoroutine(Success());

        }
    }


    IEnumerator Success()
    {
        // Pop.Play();
        yield return new WaitForSeconds(0.3f);
       // eeaaaahSound.Play();

        PlayerPrefs.SetInt("LeftEarWon", 1);
        PlayerPrefs.Save();
        

        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
    }


}
