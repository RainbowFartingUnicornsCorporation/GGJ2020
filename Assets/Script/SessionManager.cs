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

        PlayerPrefs.SetInt("brainWon", 0);
        PlayerPrefs.SetInt("rightWon", 0);
        PlayerPrefs.SetInt("leftWon", 0);
        PlayerPrefs.SetInt("heartWon", 0);
        PlayerPrefs.Save();

    }


    void Start()
    {

        string playerPrefSessionId = PlayerPrefs.GetString("sessionId", "null");
        if (playerPrefSessionId != AnalyticsSessionInfo.sessionId.ToString())
        {
            initialization();
        }

        int brainWon = PlayerPrefs.GetInt("brainWon", 0);
        int rightWon = PlayerPrefs.GetInt("rightWon", 0);
        int leftWon = PlayerPrefs.GetInt("leftWon", 0);
        int heartWon = PlayerPrefs.GetInt("heartWon", 0);

        float musicLevel = 0;
        float soundLevel = 0;
        float rightLeftLevel = 0;

        if (rightWon == 1 && leftWon == 1)
        {
            musicLevel = 0.5f;
        }
        else if (rightWon == 1)
        {
            rightLeftLevel = 0.5f;
            musicLevel = 0.3f;
            soundLevel = 1.0f;

        }
        else if (leftWon == 1)
        {
            rightLeftLevel = -0.5f;
            musicLevel = 0.3f;
            soundLevel = 1.0f;
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

            print(musicSrc);
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
