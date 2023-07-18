using UnityEngine;

public class DishFactory
{
    private DishView _prefab = Resources.Load<DishView>(AssetsPath.Dish);

    public DishView Create(int maxCookables)
    {
        DishView dishView = Object.Instantiate(_prefab);
        DishBuilder dishBuilder = new();
        dishBuilder.Build(dishView, maxCookables);

        return dishView;
    }
}
