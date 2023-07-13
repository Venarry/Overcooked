using System;
using Unity.VisualScripting;
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

    public void SetParent(Transform parent, bool isVisiable = true)
    {
        _parent = parent;
        transform.rotation = Quaternion.identity;
        SetInteractiveState(true);
        gameObject.SetActive(isVisiable);
        ParentSet?.Invoke();
    }

    public void RemoveParent()
    {
        _parent = null;
        SetInteractiveState(false);
        gameObject.SetActive(true);
        ParentRemove?.Invoke();
    }

    private void SetInteractiveState(bool state)
    {
        _rigidbody.isKinematic = state;
        _collider.isTrigger = state;
    }
}
