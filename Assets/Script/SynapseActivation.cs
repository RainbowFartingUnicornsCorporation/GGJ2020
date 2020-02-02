using UnityEngine;

public class SynapseActivation : MonoBehaviour
{
    public bool isActivated = false;

    private const string ENERGY_DOT = "EnergyDot";
    public AudioSource buzz;
    private bool isIn;

    void Start()
    {
        isIn = true;
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<SpriteRenderer>().gameObject.tag == ENERGY_DOT)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            isActivated = true;
            if (isIn)
            {
                buzz.Play();
                isIn = false;
            }

        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == ENERGY_DOT)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            isActivated = false;
        }
        if (!isIn)
        {
            isIn = true;
        }
    }
}
