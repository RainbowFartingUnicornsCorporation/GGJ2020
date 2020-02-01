using UnityEngine;

public class SynapseActivation : MonoBehaviour
{
    private const string ENERGY_DOT = "EnergyDot";
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == ENERGY_DOT)
            GetComponent<MeshRenderer>().material.color = Color.green;
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == ENERGY_DOT)
            GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
