using System;
using System.Collections.Generic;
using UnityEngine;

public class PresetsInitialize : MonoBehaviour
{
    [SerializeField] private List<PresetHolderData<CookingHolderView>> _cookingPots;
    [SerializeField] private List<PresetHolderData<CookingHolderView>> _cookingPans;
    [SerializeField] private List<PresetHolderData<DishView>> _dishes;
    [SerializeField] private List<CookingStoveCounterView> _cookingStoves;

    private CookingPotBuilder _cookingPotBuilder;
    private CookingPanBuilder _cookingPanBuilder;
    private DishBuilder _dishBuilder;
    private CookingStoveBuilder _cookingStoveBuilder;

    public void MakeInit()
    {
        _cookingPotBuilder = new();
        _cookingPanBuilder = new();
        _dishBuilder = new();
        _cookingStoveBuilder = new();

        InitHolders();
    }

    private void InitHolders()
    {
        foreach (var holderData in _cookingPots)
        {
            _cookingPotBuilder.Build(holderData.Holder, holderData.MaxValue);
        }

        foreach (var holderData in _cookingPans)
        {
            _cookingPanBuilder.Build(holderData.Holder, holderData.MaxValue);
        }

        foreach (var holderData in _dishes)
        {
            _dishBuilder.Build(holderData.Holder, holderData.MaxValue);
            //holderData.Holder.WashDish();
        }

        foreach (var holderData in _cookingStoves)
        {
            _cookingStoveBuilder.Build(holderData);
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
