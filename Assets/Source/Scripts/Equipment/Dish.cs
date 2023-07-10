using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractiveObject))]
public class Dish : MonoBehaviour, ICookableHolder, IPickable
{
    private List<ICookable> _cookables = new List<ICookable>();
    private InteractiveObject _interactive;

    public bool CanInteract => _interactive.HasParent == false;
    public int CookablesCount => _cookables.Count;

    private void Awake()
    {
        _interactive = GetComponent<InteractiveObject>();
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        if(objectInteractSystem.HasPickable == false)
            objectInteractSystem.TryGivePickable(this);

        if (objectInteractSystem.TryGetPickableType(out ICookable coockable))
            _cookables.Add(coockable);

        if (objectInteractSystem.TryGetPickableType(out ICookableHolder coockableHolder))
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
