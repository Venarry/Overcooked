using System;
using System.Collections.Generic;
using UnityEngine;

public class CookableHolder
{
    private Transform _holdPoint;
    private int _maxCookables = 1;
    private ICookableHolder _holder;
    private List<ICookable> _cookables = new List<ICookable>();

    public int CookableCount => _cookables.Count;

    public event Action HolderCleared;

    public CookableHolder(Transform holdPoint, int maxCookables, ICookableHolder holder)
    {
        _holdPoint = holdPoint;
        _maxCookables = maxCookables;
        _holder = holder;
    }

    public void OnCookStageChanged()
    {
        foreach (ICookable cookable in _cookables)
        {
            cookable.AddCookStage();
        }
    }

    public void Interact(PlayerObjectInteract objectInteractSystem, KitchenObjectType holderType)
    {
        if (objectInteractSystem.HasPickable)
        {
            if (TryInteractWithHolder(objectInteractSystem))
                return;

            if (TryGetCookable(objectInteractSystem, holderType))
                return;
        }
        else
        {
            if(_holder is IPickable pickable)
                objectInteractSystem.TryGivePickable(pickable);
        }
    }

    public bool TryAddCookable(ICookable cookable, KitchenObjectType holderType)
    {
        if (cookable.CanPlace(holderType) == false)
            return false;

        if (_cookables.Count >= _maxCookables)
            return false;

        if (cookable is IPickable pickable)
        {
            pickable.SetParent(_holdPoint);
        }

        _cookables.Add(cookable);

        return true;
    }

    public bool TryGetCookable(PlayerObjectInteract objectInteractSystem, KitchenObjectType holderType)
    {
        if (objectInteractSystem.TryGetPickableType(out ICookable cookable) == false)
            return false;

        if (objectInteractSystem.CanPlacePickable(holderType) == false)
            return false;

        if (_cookables.Count >= _maxCookables)
            return false;

        objectInteractSystem.RemovePickableRoot();
        TryAddCookable(cookable, holderType);

        return true;
    }

    public bool TryInteractWithHolder(PlayerObjectInteract objectInteractSystem)
    {
        if (objectInteractSystem.TryGetPickableType(out ICookableHolder cookableHolder) == false)
            return false;

        if (cookableHolder.CookablesCount == 0)
        {
            GiveCookablesInOutHolder(cookableHolder);
        }
        else
        {
            cookableHolder.GiveCookablesInOutHolder(_holder);
        }

        return true;
    }

    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder)
    {
        int startCounter = _cookables.Count - 1;

        for (int i = startCounter; i >= 0; i--)
        {
            if (cookableHolder.TryAddCookable(_cookables[i]))
            {
                _cookables.Remove(_cookables[i]);
            }
        }

        if (_cookables.Count == 0)
            HolderCleared?.Invoke();
    }
}
