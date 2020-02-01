using UnityEngine;

public class AnchorBehaviour : MonoBehaviour
{
    private const string KEY = "Key";

    private Collider _keyCollider;

    void Update()
    {
        if (_keyCollider != null && Input.GetKeyDown((KeyCode)_keyCollider.gameObject.GetComponent<KeyBehaviour>().Value))
        {
            print("It works !");
        }

        if (_keyCollider != null && Input.GetKeyDown(KeyCode.Space))
        {
            _keyCollider.gameObject.GetComponent<KeyBehaviour>().Detach();
            _keyCollider = null;
        }
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
