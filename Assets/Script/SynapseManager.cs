using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SynapseManager : MonoBehaviour
{
    public SynapseActivation[] synapses;
    private bool win;

    void Start()
    {
        win = false;
    }


    IEnumerator Success()
    {
        // Pop.Play();
        yield return new WaitForSeconds(0.3f);
        // eeaaaahSound.Play();

        PlayerPrefs.SetInt("BrainWon", 1);
        PlayerPrefs.Save();

        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
    }

    private void Update()
    {
        if (!win)
        {
            foreach (var synapse in synapses)
            {
                if (!synapse.isActivated)
                    return;
            }
            print("win");
            win = true;
            StartCoroutine(Success());
        }
    }
}
