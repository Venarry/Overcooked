using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ObjectInteractive))]
[RequireComponent(typeof(CookingProcessModel))]
public class Ingredient : MonoBehaviour, IPickable, ICookable
{
    private ObjectInteractive _interactive;
    private CookingProcessModel _cookingProcess;

    private KitchenObjectType[] AvaiablePlaceTypes => _cookingProcess.AvailablePlaceTypes;

    public bool CanInteract => _interactive.HasParent == false;

    private void Awake()
    {
        _interactive = GetComponent<ObjectInteractive>();
        _cookingProcess = GetComponent<CookingProcessModel>();
    }

    public void Interact(InteractSystem interactSystem)
    {
        if (interactSystem.HasPickable)
            return;

        interactSystem.TryGivePickable(this);
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
