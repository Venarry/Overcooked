using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InteractiveObject))]
[RequireComponent(typeof(CookingProcess))]
public class Ingredient : MonoBehaviour, IPickable, ICookable
{
    private InteractiveObject _interactive;
    private CookingProcess _cookingProcess;

    private KitchenObjectType[] AvaiablePlaceTypes => _cookingProcess.AvailablePlaceTypes;

    public bool CanInteract => _interactive.HasParent == false;

    private void Awake()
    {
        _interactive = GetComponent<InteractiveObject>();
        _cookingProcess = GetComponent<CookingProcess>();
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        if (objectInteractSystem.HasPickable)
            return;

        objectInteractSystem.TryGivePickable(this);
    }

    public bool CanPlace(KitchenObjectType type) => AvaiablePlaceTypes.Contains(type);

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
        _cookingProcess.CookNextStep(step);
    }
}
