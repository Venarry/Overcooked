using System.Collections;
using UnityEngine;

public class DishConveyor : MonoBehaviour
{
    private readonly DishesCounterView _dishesCounter;
    private readonly WaitForSeconds _waitForSeconds;
    private bool _isWashedDishes;

    public DishConveyor(DishesCounterView dishesCounter, float dishRespawnTimer)
    {
        _dishesCounter = dishesCounter;
        _waitForSeconds = new WaitForSeconds(dishRespawnTimer);
    }

    public void AddDishes(DishView dish)
    {
        dish.Hide();
        StartCoroutine(RespawnDish(dish));
    }

    private IEnumerator RespawnDish(DishView dish)
    {
        yield return _waitForSeconds;

        _dishesCounter.TryAddDish(dish);
        dish.Show();
    }
}
