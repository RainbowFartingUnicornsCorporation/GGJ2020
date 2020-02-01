using UnityEngine;

public class SynapseManager : MonoBehaviour
{
    public SynapseActivation[] synapses;

    private void Update()
    {
        foreach(var synapse in synapses)
        {
            if (!synapse.isActivated)
                return;
        }
        print("win");
    }
}
