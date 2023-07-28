using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class InteractedObjectView : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    private Transform _parent;

    public bool CanInteract => _parent == null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void LateUpdate()
    {
        if (_parent == null)
            return;

        transform.SetPositionAndRotation(_parent.position, _parent.rotation);
    }

    public void SetParent(Transform parent, bool isVisiable = true)
    {
        _parent = parent;
        transform.rotation = Quaternion.identity;
        SetInteractiveState(true);
        gameObject.SetActive(isVisiable);

        transform.SetParent(parent);
    }

    public void RemoveParent()
    {
        _parent = null;
        SetInteractiveState(false);
        gameObject.SetActive(true);

        transform.SetParent(null);
    }

    private void SetInteractiveState(bool state)
    {
        _rigidbody.isKinematic = state;
        _collider.isTrigger = state;
    }
}
