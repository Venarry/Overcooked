using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CookableHolder
{
    private Transform _holdPoint;
    private int _maxCookables = 1;
    private ICookableHolder _holder;
    private List<ICookable> _cookables = new List<ICookable>();
    private CombineIngredientShower _combineIngredientShower;

    public int CookableCount => _cookables.Count;
    public KitchenObjectType[] CookablesType => _cookables.Select(currentCookable => currentCookable.Type).ToArray();

    public event Action HolderCleared;

    public CookableHolder(Transform holdPoint, int maxCookables, ICookableHolder holder, Dictionary<KitchenObjectType[], Mesh> combines)
    {
        _holdPoint = holdPoint;
        _maxCookables = maxCookables;
        _holder = holder;

        _combineIngredientShower = new CombineIngredientShower(holdPoint, combines);
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
            pickable.SetParent(_holdPoint, false);
        }

        _cookables.Add(cookable);

        _combineIngredientShower.RefreshModel(CookablesType);

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

        _combineIngredientShower.RefreshModel(CookablesType);
    }

    private bool TryGetCookable(PlayerObjectInteract objectInteractSystem, KitchenObjectType holderType)
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

    private bool TryInteractWithHolder(PlayerObjectInteract objectInteractSystem)
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
}
