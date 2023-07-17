using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ingredient", menuName = "Ingredient/Create new ingredient")]
public class CookableIngredientSO : ScriptableObject
{
    [SerializeField] private List<CookStage> _cookStages;

    public int MaxCookIndexStage => _cookStages.Count - 1;

    public Mesh GetMeshByIndex(int index) =>
        _cookStages[GetNormalizedIndex(index)].Mesh;

    public float GetCookingTimeByIndex(int index) =>
        _cookStages[GetNormalizedIndex(index)].CookingTime;

    public KitchenObjectType GetTypeByIndex(int index) =>
        _cookStages[GetNormalizedIndex(index)].Type;

    public KitchenObjectType[] GetAvailablePlaceTypesTimeByIndex(int index) =>
        _cookStages[GetNormalizedIndex(index)].AvailablePlaceTypes;

    private int GetNormalizedIndex(int index)
    {
        if (index >= _cookStages.Count)
            index = _cookStages.Count - 1;

        if (index < 0)
            index = 0;

        return index;
    }
}

[Serializable]
public class CookStage
{
    [SerializeField] private Mesh _mesh;
    [SerializeField, Range(0.5f, 30)] private float _cookingTime;
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private List<KitchenObjectType> _availablePlaceObjects;

    public Mesh Mesh => _mesh;
    public float CookingTime => _cookingTime;
    public KitchenObjectType Type => _type;
    public KitchenObjectType[] AvailablePlaceTypes => _availablePlaceObjects.ToArray();
}
