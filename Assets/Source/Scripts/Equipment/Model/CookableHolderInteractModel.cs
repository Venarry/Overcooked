using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CookableHolderInteractModel
{
    private readonly Transform _holdPoint;
    private readonly ICookableHolder _thisHolder;
    private readonly ITypeProvider _typeProvider;
    private readonly List<ICookable> _cookables = new();
    private readonly int _maxCookables = 1;
    private readonly bool _isVisiableCookables;

    public int CookableCount => _cookables.Count;
    public KitchenObjectType[] CookablesType => _cookables.Select(currentCookable => currentCookable.Type).ToArray();
    public float CookablesCookedTime => _cookables.Sum(currentCookable => currentCookable.MaxCookedTime);

    public event Action HolderCleared;
    public event Action<ICookable> CookableAdded;
    public event Action<ICookable> CookableRemoved;

    public CookableHolderInteractModel(ICookableHolder holder, ITypeProvider typeProvider, Transform holdPoint, int maxCookables, bool isVisiableCookables)
    {
        _holdPoint = holdPoint; // трансфоорм не передавать в модель
        _maxCookables = maxCookables;
        _isVisiableCookables = isVisiableCookables;
        _thisHolder = holder;
        _typeProvider = typeProvider;
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        if (objectInteractSystem.HasPickable)
        {
            if (TryInteractWithHolder(objectInteractSystem))
                return;

            if (TryGetCookable(objectInteractSystem))
                return;
        }
        else
        {
            if (_thisHolder is IPickable pickable)
                objectInteractSystem.TryGivePickable(pickable);
        }
    }

    public void AddCookableCookStages()
    {
        foreach (ICookable cookable in _cookables)
        {
            cookable.AddCookStage();
        }
    }

    public void SubtractCookableCookStages()
    {
        foreach (ICookable cookable in _cookables)
        {
            cookable.SubtractCookStage();
        }
    }

    public void SetCookableOvercookedStage()
    {
        foreach (ICookable cookable in _cookables)
        {
            cookable.SetOverCookedStage();
        }
    }

    public bool CanPlaceOn(KitchenObjectType type) =>
        _typeProvider.AvailablePlaceTypes.Contains(type);

    public bool TryAddCookable(ICookable cookable)
    {
        if (cookable.CanPlaceOn(_typeProvider.Type) == false)
            return false;

        if (_cookables.Count >= _maxCookables)
            return false;

        if (cookable is IPickable pickable)
        {
            pickable.SetParent(_holdPoint, _isVisiableCookables);
        }

        _cookables.Add(cookable);
        CookableAdded?.Invoke(cookable);

        return true;
    }

    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder)
    {
        int startCounter = _cookables.Count - 1;

        for (int i = startCounter; i >= 0; i--)
        {
            if (cookableHolder.TryAddCookable(_cookables[i]))
            {
                ICookable currentCookable = _cookables[i];
                _cookables.Remove(currentCookable);

                CookableRemoved?.Invoke(currentCookable);
            }
        }

        if (_cookables.Count == 0)
            HolderCleared?.Invoke();
    }

    private bool TryGetCookable(PlayerObjectInteract objectInteractSystem)
    {
        if (objectInteractSystem.TryGetPickableType(out ICookable cookable) == false)
            return false;

        if (objectInteractSystem.CanPlacePickable(_typeProvider.Type) == false)
            return false;

        if (_cookables.Count >= _maxCookables)
            return false;

        objectInteractSystem.RemovePickableRoot();
        TryAddCookable(cookable);

        return true;
    }

    private bool TryInteractWithHolder(PlayerObjectInteract objectInteractSystem)
    {
        if (objectInteractSystem.TryGetPickableType(out ICookableHolder cookableHolder) == false)
            return false;

        if (cookableHolder is DishView && cookableHolder.CookablesCount == 0)
        {
            GiveCookablesInOutHolder(cookableHolder);
            return true;
        }

        if (cookableHolder.CookablesCount == 0)
        {
            GiveCookablesInOutHolder(cookableHolder);
        }
        else
        {
            cookableHolder.GiveCookablesInOutHolder(_thisHolder);
        }

        return true;
    }
}
