using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(InteractiveObject))]
[RequireComponent(typeof(CookingProcessPresenter))]
public class CookingPot : MonoBehaviour, ICookableHolder, ICookable, IPickable
{
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private int _maxCookables = 1;

    private InteractiveObject _interactive;
    private CookingProcessPresenter _cookingProcessPresenter;
    private CookableHolder _cookableHolder;

    public int CookablesCount => _cookableHolder.CookableCount;
    public bool CanInteract => _interactive.HasParent == false;

    private void Awake()
    {
        _interactive = GetComponent<InteractiveObject>();
        _cookingProcessPresenter = GetComponent<CookingProcessPresenter>();
        _cookableHolder = new CookableHolder(_holdPoint, _maxCookables, this);
    }

    private void OnEnable()
    {
        _cookingProcessPresenter.CookStageChanged += _cookableHolder.OnCookStageChanged;
        _cookableHolder.HolderCleared += _cookingProcessPresenter.ResetStages;
    }

    private void OnDisable()
    {
        _cookingProcessPresenter.CookStageChanged -= _cookableHolder.OnCookStageChanged;
        _cookableHolder.HolderCleared -= _cookingProcessPresenter.ResetStages;
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _cookableHolder.Interact(objectInteractSystem, _cookingProcessPresenter.Type);
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
        if(_cookableHolder.CookableCount > 0)
            _cookingProcessPresenter.Cook(step);
    }

    public bool TryAddCookable(ICookable cookable) =>
        _cookableHolder.TryAddCookable(cookable, _cookingProcessPresenter.Type);

    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder)
    {
        _cookableHolder.GiveCookablesInOutHolder(cookableHolder);
    }

    public void AddCookStage()
    {
        _cookingProcessPresenter.AddCookStage();
    }
}
