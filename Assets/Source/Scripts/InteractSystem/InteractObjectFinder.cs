using System;
using UnityEngine;

public class InteractObjectFinder
{
    private readonly Transform _grabTransform;
    private readonly float _interactRange;
    private readonly Collider[] _result = new Collider[24];

    public InteractObjectFinder(Transform grabTransform, float interactRange)
    {
        _grabTransform = grabTransform;
        _interactRange = interactRange;
    }
    
    public event Action<Collider> InteractObjectFound;

    public bool TryFindInteractive(out IInteractable interactable)
    {
        Physics.OverlapSphereNonAlloc(_grabTransform.position, _interactRange, _result);

        float minDistance = _interactRange;
        interactable = null;

        foreach (Collider collider in _result)
        {
            if(collider == null)
                break;

            if (collider.TryGetComponent(out IInteractable interactive) == false) 
                continue;
            
            if (interactive.CanInteract == false)
                continue;

            float currentDistance = Vector3.Distance(_grabTransform.position, collider.gameObject.transform.position);

            if (currentDistance >= minDistance)
                continue;
            
            interactable = interactive;
            minDistance = currentDistance;
            
            InteractObjectFound?.Invoke(collider);
        }
        
        return interactable != null;
    }
}
