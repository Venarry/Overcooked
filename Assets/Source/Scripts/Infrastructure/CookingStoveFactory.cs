using UnityEngine;

public class CookingStoveFactory
{
    private readonly CookingStoveCounterView _prefab = Resources.Load<CookingStoveCounterView>(AssetsPath.CookingStove);
    private readonly CookingStoveBuilder _cookingStoveBuilder = new();

    public CookingStoveCounterView Create()
    {
        CookingStoveCounterView cookingStoveCounterView = Object.Instantiate(_prefab);
        _cookingStoveBuilder.Build(cookingStoveCounterView);
        return cookingStoveCounterView;
    }
}
