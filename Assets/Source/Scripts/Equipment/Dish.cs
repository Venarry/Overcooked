using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectInteractive))]
public class Dish : MonoBehaviour, ICookableHolder, IPickable
{
    private List<ICookable> _cookables = new List<ICookable>();
    private ObjectInteractive _interactive;

    public bool CanInteract => _interactive.HasParent == false;
    public int CookablesCount => _cookables.Count;

    private void Awake()
    {
        _interactive = GetComponent<ObjectInteractive>();
    }

    public void Interact(PlayerInteracter interactSystem)
    {
        if(interactSystem.HasPickable == false)
            interactSystem.TryGivePickable(this);

        if (interactSystem.TryGetPickableType(out ICookable coockable))
            _cookables.Add(coockable);

        if (interactSystem.TryGetPickableType(out ICookableHolder coockableHolder))
            coockableHolder.GiveCookablesInOutHolder(this);
    }

    public bool CanPlace(KitchenObjectType type)
    {
        return true;
    }

    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder)
    {
        return;
    }

    public void RemoveParent()
    {
        _interactive.RemoveParent();
    }

    public void SetParent(Transform point)
    {
        _interactive.SetParent(point);
    }

    public bool TryAddCookable(ICookable cookable)
    {
        _cookables.Add(cookable);

        if (cookable is IPickable pickable)
        {
            pickable.SetParent(transform);
        }

        return true;
    }
}
