using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ingredients icon", menuName = "Ingredients icon/Create new ingredients icon")]
public class IngredientsIconSO : ScriptableObject
{
    [SerializeField] private List<IngredientTypeIcon> _ingredientTypeIcons;

    public List<KeyValuePair<KitchenObjectType, Sprite>> GetIngredientsIcon()
    {
        List<KeyValuePair<KitchenObjectType, Sprite>> combines = new();

        foreach (IngredientTypeIcon ingredient in _ingredientTypeIcons)
        {
            KeyValuePair<KitchenObjectType, Sprite> currentCombine = ingredient.Icon;
            combines.Add(new KeyValuePair<KitchenObjectType, Sprite>(currentCombine.Key, currentCombine.Value));
        }

        return combines;
    }
}

[Serializable]
public class IngredientTypeIcon
{
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private Sprite _sprite;

    public KeyValuePair<KitchenObjectType, Sprite> Icon => new(_type, _sprite);
}
