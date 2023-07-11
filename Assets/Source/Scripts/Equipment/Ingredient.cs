using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(InteractiveObject))]
[RequireComponent(typeof(ProgressBar))]
public class Ingredient : MonoBehaviour, IPickable, ICookable
{
    private InteractiveObject _interactive;
    private CookigProcessPresenter _cookingProcessPresenter;

    public bool CanInteract => _interactive.HasParent == false;

    private void Awake()
    {
        _interactive = GetComponent<InteractiveObject>();
        _cookingProcessPresenter = GetComponent<CookigProcessPresenter>();
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        if (objectInteractSystem.HasPickable)
            return;

        objectInteractSystem.TryGivePickable(this);
    }

    public bool CanPlace(KitchenObjectType type) => _cookingProcessPresenter.AvailablePlaceTypes.Contains(type);

    public void SetParent(Transform point)
    {
        _interactive.SetParent(point);
    }

    public void RemoveParent()
    {
        _interactive.RemoveParent();
    } 

    public void Cook(float step = 0)
    {
        _cookingProcessPresenter.Cook(step);
    }
}
