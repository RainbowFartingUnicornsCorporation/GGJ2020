using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    public KeyCode Value;

    private int MAX_DISTANCE = 1;
    private const string ANCHOR = "Anchor";

    private Rigidbody _keyRigidBody;
    private Collider _anchorCollider;

    void Start()
    {
        _keyRigidBody = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == ANCHOR)
            _anchorCollider = collider;
    }

    void OnMouseDrag()
    {
        _keyRigidBody.useGravity = false;
        _keyRigidBody.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
    }

    void OnMouseUp()
    {
        if (CanBeAttached())
        {
            _keyRigidBody.useGravity = false;
            _keyRigidBody.position = _anchorCollider.transform.position;
            _keyRigidBody.velocity = Vector3.zero;
            _keyRigidBody.angularVelocity = Vector3.zero;
        }
        else
        {
            _keyRigidBody.useGravity = true;
            _anchorCollider = null;
        }
    }

    private bool CanBeAttached()
        => _anchorCollider != null &&
                    Mathf.Abs(_anchorCollider.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x) < MAX_DISTANCE &&
                    Mathf.Abs(_anchorCollider.transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y) < MAX_DISTANCE;

    public void Detach()
    {
        _keyRigidBody.useGravity = true;
    }
}
