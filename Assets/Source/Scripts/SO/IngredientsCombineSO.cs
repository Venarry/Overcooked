using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New combine", menuName = "Ingredients combine/Create new combine")]
public class IngredientsCombineSO : ScriptableObject
{
    [SerializeField] private List<Combine> _combines;

    public Dictionary<KitchenObjectType[], Mesh> GetCombines()
    {
        Dictionary<KitchenObjectType[], Mesh> combines = new Dictionary<KitchenObjectType[], Mesh>();

        foreach (Combine combine in _combines)
        {
            KeyValuePair<KitchenObjectType[], Mesh> currentCombine = combine.Get;
            combines.Add(currentCombine.Key, currentCombine.Value);
        }

        return combines;
    }
}

[Serializable]
public class Combine
{
    [SerializeField] private Mesh _mesh;
    [SerializeField] private List<KitchenObjectType> _types;

    public KeyValuePair<KitchenObjectType[], Mesh> Get => new KeyValuePair<KitchenObjectType[], Mesh>(_types.ToArray(), _mesh);
}
