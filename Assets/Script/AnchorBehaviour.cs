using UnityEngine;

public class AnchorBehaviour : MonoBehaviour
{
    public MonoBehaviour Interaction;

    private const string KEY = "Key";

    private Collider _keyCollider;

    void Update()
    {
        if (_keyCollider != null)
        {
            if (Input.GetKeyDown(_keyCollider.gameObject.GetComponent<KeyBehaviour>().Value))
            {
                ((IInteraction)Interaction).KeyPressedAction();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                DetachKey();
            }
        }
    }
    //TODO bug quand la touche est tenue par le joueur sur le collider de l'ancre
    public void DetachKey()
    {
        if (!_keyCollider)
            return;
        _keyCollider.gameObject.GetComponent<KeyBehaviour>().Detach();
        _keyCollider = null;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == KEY)
            _keyCollider = collider;
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == KEY)
            _keyCollider = null;
    }
}
