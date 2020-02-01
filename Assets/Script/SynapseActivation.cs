using UnityEngine;

public class SynapseActivation : MonoBehaviour
{
    public bool isActivated = false;

    private const string ENERGY_DOT = "EnergyDot";

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == ENERGY_DOT)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
            isActivated = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == ENERGY_DOT)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            isActivated = false;
        }
    }
}
