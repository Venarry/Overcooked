using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractSystem))]
public class InteractiveView : MonoBehaviour
{
    [SerializeField] private Transform _interactPoint;
    [SerializeField] private float _viewInteractRange;
    [SerializeField] private Color _viewColor;

    private Collider[] _colliders = new Collider[10];
    private Collider _target;

    private void FixedUpdate()
    {
        if(_target != null)
        {
            SetColor(_target, Color.white);
        }

        _target = FindInteractive();

        if (_target != null)
        {
            SetColor(_target, _viewColor);
        }
    }

    private Collider FindInteractive()
    {
        Physics.OverlapSphereNonAlloc(_interactPoint.position, _viewInteractRange, _colliders);

        float minDistance = _viewInteractRange;
        Collider targetObject = null;

        foreach (Collider collider in _colliders)
        {
            if (collider == null)
                continue;

            if (collider.TryGetComponent(out IInteractable interactive))
            {
                if (interactive.CanInteract == false)
                    continue;

                float currentDistnace = Vector3.Distance(_interactPoint.position, collider.gameObject.transform.position);

                if (currentDistnace < minDistance)
                {
                    targetObject = collider;
                    minDistance = currentDistnace;
                }
            }
        }

        return targetObject;
    }

    private void SetColor(Collider collider, Color color)
    {
        collider.GetComponentInChildren<MeshRenderer>().material.color = color;
    }
}
