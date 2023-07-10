using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class InteractiveObject : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    private Transform _parent;

    public event Action ParentSet;
    public event Action ParentRemove;

    public bool HasParent => _parent != null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        if (_parent == null)
            return;

        transform.position = _parent.position;
        transform.rotation = _parent.rotation;
    }

    public void SetParent(Transform parent)
    {
        _parent = parent;
        _rigidbody.isKinematic = true;
        _collider.isTrigger = true;
        transform.rotation = Quaternion.identity;
        ParentSet?.Invoke();
    }

    public void RemoveParent()
    {
        _parent = null;
        _rigidbody.isKinematic = false;
        _collider.isTrigger = false;
        ParentRemove?.Invoke();
    }
}
