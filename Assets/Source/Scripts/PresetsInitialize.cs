using System;
using System.Collections.Generic;
using UnityEngine;

public class PresetsInitialize : MonoBehaviour
{
    [SerializeField] private List<PresetHolderData<CookingPotView>> _cookingPots;
    [SerializeField] private List<PresetHolderData<DishView>> _dishes;

    private CookingPotBuilder _cookingPotbuilder;
    private DishBuilder _dishBuilder;

    public void MakeInit()
    {
        _cookingPotbuilder = new();
        _dishBuilder = new();

        InitHolders();
    }

    private void InitHolders()
    {
        foreach (PresetHolderData<CookingPotView> holderData in _cookingPots)
        {
            _cookingPotbuilder.Build(holderData.Holder, holderData.MaxValue);
            holderData.Holder.Enable();
        }

        foreach (PresetHolderData<DishView> holderData in _dishes)
        {
            _dishBuilder.Build(holderData.Holder, holderData.MaxValue);
            holderData.Holder.Enable();
        }
    }
}

[Serializable]
public class PresetHolderData<T>
{
    [SerializeField] private T _holder;
    [SerializeField] private int _maxValue;

    public T Holder => _holder;
    public int MaxValue => _maxValue;
}
