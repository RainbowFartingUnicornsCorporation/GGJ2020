using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class SoundManager : MonoBehaviour
{
   
    void initialization()
    {
        PlayerPrefs.SetString("sessionId", AnalyticsSessionInfo.sessionId.ToString());
        PlayerPrefs.SetInt("brainWon", 0);
        PlayerPrefs.SetInt("rightWon", 0);
        PlayerPrefs.SetInt("leftWon", 0);
        PlayerPrefs.SetInt("heartWon", 0);
    }


    void Start()
    {
        string playerPrefSessionId = PlayerPrefs.GetString("sessionId","null");
        if(playerPrefSessionId != AnalyticsSessionInfo.sessionId.ToString())
        {
            initialization();
        }
        else // initialization of the scene from playerpref 
        {

        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
