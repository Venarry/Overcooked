using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InteractiveObject))]
public class Dish : MonoBehaviour, ICookableHolder, IPickable
{
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private int _maxCookables = 1;
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private KitchenObjectType[] _availablePlaceTypes;
    [SerializeField] private IngredientsCombineSO _ingredientsCombineSO;

    private InteractiveObject _interactive;
    private CookableHolder _cookableHolder;

    public bool CanInteract => _interactive.HasParent == false;
    public int CookablesCount => _cookableHolder.CookableCount;

    private void Awake()
    {
        _interactive = GetComponent<InteractiveObject>();
        _cookableHolder = new CookableHolder(_holdPoint, _maxCookables, this, _ingredientsCombineSO.GetCombines());
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _cookableHolder.Interact(objectInteractSystem, _type);
    }

    public bool CanPlace(KitchenObjectType type) =>
        _availablePlaceTypes.Contains(type);


    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder)
    {
        _cookableHolder.GiveCookablesInOutHolder(cookableHolder);
    }

    public void RemoveParent()
    {
        _interactive.RemoveParent();
    }

    public void SetParent(Transform point, bool isVisiable)
    {
        _interactive.SetParent(point, isVisiable);
    }

    public bool TryAddCookable(ICookable cookable) =>
        _cookableHolder.TryAddCookable(cookable, _type);
}
