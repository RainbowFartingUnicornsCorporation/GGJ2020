using UnityEngine;

public class AnchorBehaviour : MonoBehaviour
{
    public MonoBehaviour Interaction;
    public SpriteRenderer empty;
    public SpriteRenderer used;
    public bool isFree = true;

    private bool dead = false;

    private const string KEY = "Key";

    private Collider _keyCollider;

    void Update()
    {
        if (dead)
            return;

        if (_keyCollider != null)
        {
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
        _keyCollider.isTrigger = false;
        _keyCollider.gameObject.GetComponent<KeyBehaviour>().Detach();
        _keyCollider = null;
    }

    public virtual void Kill()
    {
        DetachKey();
        gameObject.SetActive(false);
        dead = true;
    }

   void OnTriggerEnter(Collider collider)
   {
       if (_keyCollider == null && collider.gameObject.tag == KEY)
       {
           _keyCollider = collider;
           collider.isTrigger = true;
 
           used.gameObject.SetActive(true);
           empty.gameObject.SetActive(false);
       }
   }

    void OnTriggerExit(Collider collider)
    {
        if (_keyCollider == collider && collider.gameObject.tag == KEY)
        {
            _keyCollider = null;
            collider.isTrigger = false;
           isFree = true;
            used.gameObject.SetActive(false);
            empty.gameObject.SetActive(true);
        }
    }

    public void RegisterKey()
    {
        isFree = false;
    }

    public void UnregisterKey()
    {
        isFree = true;
    }
}
