using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatbeatSound : MonoBehaviour

{

    public AudioSource beat1;
    public AudioSource beat2;
    
    

    public IEnumerator BoomBoom()
    {
        beat1.Stop();
        beat2.Stop();

        beat1.Play();
        yield return new WaitWhile(() => beat1.isPlaying);
        beat2.Play();

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       

    }
}
