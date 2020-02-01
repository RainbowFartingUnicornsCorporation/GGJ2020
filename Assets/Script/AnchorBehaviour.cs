﻿using UnityEngine;

public class AnchorBehaviour : MonoBehaviour
{
    public MonoBehaviour Interaction;
    public SpriteRenderer empty;
    public SpriteRenderer used;

    private const string KEY = "Key";

    private Collider _keyCollider;

    void Update()
    {
        if (_keyCollider != null)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                DetachKey();
                return;
            }

            _keyCollider.gameObject.GetComponent<KeyBehaviour>().transform.eulerAngles = new Vector3(0, 0, 180);

            if (Input.GetKeyDown(_keyCollider.gameObject.GetComponent<KeyBehaviour>().Value))
            {
                ((IInteraction)Interaction).KeyPressedAction(_keyCollider.gameObject.GetComponent<KeyBehaviour>().Value);
            }
        }
    }
    //TODO bug quand la touche est tenue par le joueur sur le collider de l'ancre
    public void DetachKey()
    {
        if (!_keyCollider)
            return;
        used.gameObject.SetActive(false);
        empty.gameObject.SetActive(true);
        _keyCollider.gameObject.GetComponent<KeyBehaviour>().Detach();
        _keyCollider = null;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == KEY)
        {
            _keyCollider = collider;

            used.gameObject.SetActive(true);
            empty.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == KEY)
        {
            _keyCollider = null;
            used.gameObject.SetActive(false);
            empty.gameObject.SetActive(true);
        }
    }
}
