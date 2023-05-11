using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerInteracter : MonoBehaviour
{
    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    [SerializeField] private KeyCode _alternateInteractKey = KeyCode.R;
    [SerializeField] private KeyCode _dropKey = KeyCode.G;
    [SerializeField] private float _interactRange = 2f;
    [SerializeField] private Transform _grabTransform;

    private IPickable _pickable;
    Collider[] _result = new Collider[24];

    public float InteractRange => _interactRange;
    public bool HasPickable => _pickable != null;

    private void Update()
    {
        if (Input.GetKeyDown(_interactKey))
        {
            Interact();
        }

        if (Input.GetKeyDown(_dropKey))
        {
            TryDrop();
        }
    }

    public bool TryGetPickableType<T>(out T component)
    {
        component = default;

        if (_pickable == null)
            return false;

        if (_pickable is T type)
        {
            component = type;
            return true;
        }

        return false;
    }

    public bool CanPlacePickable(KitchenObjectType type) => 
        _pickable != null ? _pickable.CanPlace(type) : true;

    public void TryRemoveIPickable()
    {
        if (_pickable == null)
            return;

        _pickable.RemoveParent();
        _pickable = null;
    }

    public bool TryTakePickable(out IPickable pickable)
    {
        pickable = null;

        if (HasPickable)
        {
            pickable = _pickable;
            _pickable = null;
            return true;
        }

        return false;
    }

    public bool TryGivePickable(IPickable pickable)
    {
        if(_pickable == null)
        {
            SetFood(pickable);
            return true;
        }

        return false;
    }

    public void RemovePickableRoot()
    {
        _pickable = null;
    }

    private IInteractable FindInteractive()
    {
        Physics.OverlapSphereNonAlloc(_grabTransform.position, _interactRange, _result);

        float minDistance = _interactRange;
        IInteractable targetObject = null;

        foreach (Collider collider in _result)
        {
            if(collider == null)
                return targetObject;

            if (collider.TryGetComponent(out IInteractable interactive))
            {
                if (interactive.CanInteract == false)
                    continue;

                float currentDistnace = Vector3.Distance(_grabTransform.position, collider.gameObject.transform.position);

                if (currentDistnace < minDistance)
                {
                    targetObject = interactive;
                    minDistance = currentDistnace;
                }
            }
        }

        return targetObject;
    }

    private void Interact()
    {
        IInteractable interactive = FindInteractive();

        if(interactive != null)
        {
            interactive.Interact(this);
            return;
        }

        TryDrop();
    }

    private bool TryDrop()
    {
        if (_pickable == null)
        {
            return false;
        }

        _pickable.RemoveParent();
        _pickable = null;

        return true;
    }

    private void SetFood(IPickable pickable)
    {
        _pickable = pickable;
        _pickable.SetParent(_grabTransform);
    }
}
