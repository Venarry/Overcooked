using System.Collections.Generic;
using UnityEngine;

public class HolderIngredientsIconShower
{
    private const int Spacing = 1;
    private readonly Transform _spawnPoint;
    private readonly IngredientIcon _prefab;
    private readonly List<KeyValuePair<KitchenObjectType, Sprite>> _ingredientsIcon;
    private readonly Dictionary<ICookable, IngredientIcon> _activeIcons = new();

    public HolderIngredientsIconShower(Transform spawnPoint, 
        List<KeyValuePair<KitchenObjectType, Sprite>> ingredientsIcon)
    {
        _spawnPoint = spawnPoint;
        _prefab = Resources.Load<IngredientIcon>(AssetsPath.IngredientIcon);
        _ingredientsIcon = ingredientsIcon;
    }

    public void AddIngredient(ICookable cookable)
    {
        if (TryGetIngredientIcon(cookable.Type, out KeyValuePair<KitchenObjectType, Sprite> ingredient) == false)
            return;

        IngredientIcon spawnedIngredient = Object.Instantiate(_prefab); // отдельная фабрика
        spawnedIngredient.transform.SetParent(_spawnPoint);
        spawnedIngredient.transform.localRotation = Quaternion.identity;
        spawnedIngredient.transform.localScale = new(1, 1, 1);
        spawnedIngredient.transform.localPosition = Vector3.zero;

        spawnedIngredient.SetImage(ingredient.Value);

        _activeIcons.Add(cookable, spawnedIngredient);
        RecalculateIconsPosistion();
    }

    public void RemoveIngredient(ICookable cookable)
    {
        if (_activeIcons.ContainsKey(cookable) == false)
            return;

        Object.Destroy(_activeIcons[cookable].gameObject);
        _activeIcons.Remove(cookable);
        RecalculateIconsPosistion();
    }

    public void RefreshIngredientsIcon()
    {
        foreach (KeyValuePair<ICookable, IngredientIcon> icon in _activeIcons)
        {
            if (TryGetIngredientIcon(icon.Key.Type, out KeyValuePair<KitchenObjectType, Sprite> ingredient) == false)
                return;

            icon.Value.SetImage(ingredient.Value);
        }
    }

    private void RecalculateIconsPosistion()
    {
        float iconsCountNormalized = _activeIcons.Count - 1;

        if (iconsCountNormalized < 0)
            return;

        float targetPosition = iconsCountNormalized / 2 * -1;

        foreach (KeyValuePair<ICookable, IngredientIcon> icon in _activeIcons)
        {
            icon.Value.transform.localPosition = new Vector3(targetPosition, 0, 0);
            targetPosition += Spacing;
        }
    }

    private bool TryGetIngredientIcon(KitchenObjectType type, out KeyValuePair<KitchenObjectType, Sprite> value)
    {
        value = new();

        foreach (KeyValuePair<KitchenObjectType, Sprite> ingredient in _ingredientsIcon)
        {
            if(ingredient.Key == type)
            {
                value = ingredient;
                return true;
            }
        }

        return false;
    }
}
