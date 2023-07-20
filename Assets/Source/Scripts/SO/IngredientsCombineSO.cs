using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New combine", menuName = "Ingredients combine/Create new combine")]
public class IngredientsCombineSO : ScriptableObject
{
    [SerializeField] private List<IngredientsCombine> _combines;

    public Dictionary<KitchenObjectType[], Mesh> GetCombines()
    {
        Dictionary<KitchenObjectType[], Mesh> combines = new();

        foreach (IngredientsCombine combine in _combines)
        {
            KeyValuePair<KitchenObjectType[], Mesh> currentCombine = combine.Combine;
            combines.Add(currentCombine.Key, currentCombine.Value);
        }

        return combines;
    }
}

[Serializable]
public class IngredientsCombine
{
    [SerializeField] private Mesh _mesh;
    [SerializeField] private List<KitchenObjectType> _types;

    public KeyValuePair<KitchenObjectType[], Mesh> Combine => new(_types.ToArray(), _mesh);
}
