using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class SessionManager : MonoBehaviour
{

    public AudioSource musique;

    private GameObject[] audioSources;


    void initialization()
    {
        // First time in game
        PlayerPrefs.SetString("sessionId", AnalyticsSessionInfo.sessionId.ToString());

        PlayerPrefs.SetInt("BrainWon", 0);
        PlayerPrefs.SetInt("RightEarWon", 0);
        PlayerPrefs.SetInt("LeftEarWon", 0);
        PlayerPrefs.SetInt("HeartWon", 0);
        PlayerPrefs.Save();

    }


    void Start()
    {

        string playerPrefSessionId = PlayerPrefs.GetString("sessionId", "null");
        if (playerPrefSessionId != AnalyticsSessionInfo.sessionId.ToString())
        {
            initialization();
        }

        int brainWon = PlayerPrefs.GetInt("BrainWon", 0);
        int rightWon = PlayerPrefs.GetInt("RightEarWon", 0);
        int leftWon = PlayerPrefs.GetInt("LeftEarWon", 0);
        int heartWon = PlayerPrefs.GetInt("HeartWon", 0);

        float musicLevel = 0;
        float soundLevel = 0;
        float rightLeftLevel = 0;

        if (rightWon == 1 && leftWon == 1)
        {
            musicLevel = 0.5f;
            soundLevel = 1;
        }
        else if (rightWon == 1)
        {
            rightLeftLevel = 0.95f;
            musicLevel = 0.35f;
            soundLevel = 0.5f;

        }
        else if (leftWon == 1)
        {
            rightLeftLevel = -0.95f;
            musicLevel = 0.35f;
            soundLevel = 0.5f;
        }
        else
        {
            musicLevel = 0.2f;
            soundLevel = 0.0f;
        }


        //Init the music with the right params
        AudioSource musicSrc = musique.GetComponent(typeof(AudioSource)) as AudioSource;
        if (musicSrc != null)
        {
  
            musicSrc.volume = musicLevel;
            musicSrc.panStereo = rightLeftLevel;
        }


        // if needed init all sound to these parametters 
        audioSources = GameObject.FindGameObjectsWithTag("Sound");
        foreach (GameObject audioSource in audioSources)
        {
            
            AudioSource tmp = audioSource.GetComponent(typeof(AudioSource)) as AudioSource;
            if (tmp != null)
            {
                tmp.volume = soundLevel;
                tmp.panStereo = rightLeftLevel;
            }

        }

    }



    // Update is called once per frame
    void Update()
    {

    }
}
