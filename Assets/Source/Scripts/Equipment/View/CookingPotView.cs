using System;
using UnityEngine;

[RequireComponent(typeof(InteractedObjectView))]
[RequireComponent(typeof(ProgressBar))]
public class CookingPotView : MonoBehaviour, ICookableHolder, ICookable, IPickable
{
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private Transform _ingredientsIconPoint; // можно поставить вторую стадию CookingPot а не Ready и тогда можно будет класть туда еду и возвращать процесс приготовления
    [SerializeField] private MeshFilter _meshFilter;

    private InteractedObjectView _interactive;
    private CookingProcessPresenter _cookingProcessPresenter;
    private CookableHolderInteractPresenter _cookableHolderInteractPresnter;

    public KitchenObjectType Type => _cookingProcessPresenter.Type;
    public int CookablesCount => _cookableHolderInteractPresnter.CookableCount;
    public bool CanInteract => _interactive.CanInteract;
    public Transform HoldTransform => _holdPoint;
    public Transform IngredientsIconPoint => _ingredientsIconPoint;
    public MeshFilter MeshFilter => _meshFilter;

    public event Action CookStageAdded;

    private void Awake()
    {
        _interactive = GetComponent<InteractedObjectView>();
    }

    public void Enable()
    {
        _cookingProcessPresenter.CookStageAdded += OnCookStageAdded;
        _cookingProcessPresenter.CookStageSubtracted += _cookableHolderInteractPresnter.SubtractCookableCookStage;
        _cookingProcessPresenter.MaxStageReached += SetOvercookedStage;
        _cookableHolderInteractPresnter.HolderCleared += _cookingProcessPresenter.ResetStages;

        _cookingProcessPresenter.Enable();
        _cookableHolderInteractPresnter.Enable();
    }

    public void Disable()
    {
        _cookingProcessPresenter.CookStageAdded -= OnCookStageAdded;
        _cookingProcessPresenter.CookStageSubtracted -= _cookableHolderInteractPresnter.SubtractCookableCookStage;
        _cookingProcessPresenter.MaxStageReached -= SetOvercookedStage;
        _cookableHolderInteractPresnter.HolderCleared -= _cookingProcessPresenter.ResetStages;

        _cookingProcessPresenter.Disable();
        _cookableHolderInteractPresnter.Disable();
    }

    public void Init(CookingProcessPresenter cookingProcessPresenter,
        CookableHolderInteractPresenter cookableHolderInteractPresenter)
    {
        _cookingProcessPresenter = cookingProcessPresenter;
        _cookableHolderInteractPresnter = cookableHolderInteractPresenter;
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _cookableHolderInteractPresnter.Interact(objectInteractSystem);
    }

    public bool CanPlaceOn(KitchenObjectType type) =>
        _cookableHolderInteractPresnter.CanPlaceOn(type);

    public void SetParent(Transform point, bool isVisiable)
    {
        _interactive.SetParent(point, isVisiable);
    }

    public void RemoveParent()
    {
        _interactive.RemoveParent();
    }

    public void Cook(float step = 0)
    {
        if(_cookableHolderInteractPresnter.CookableCount > 0)
            _cookingProcessPresenter.Cook(step);
    }

    public bool TryAddCookable(ICookable cookable) =>
        _cookableHolderInteractPresnter.TryAddCookable(cookable);

    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder)
    {
        _cookableHolderInteractPresnter.GiveCookablesInOutHolder(cookableHolder);
    }

    public void AddCookStage()
    {
        _cookingProcessPresenter.AddCookStage();
    }

    public void SubtractCookStage()
    {
        _cookingProcessPresenter.SubtractCookStage();
    }

    public void SetOvercookedStage()
    {
        _cookingProcessPresenter.SetOvercookedStage();
    }

    private void OnCookStageAdded()
    {
        _cookableHolderInteractPresnter.AddCookableCookStage();
        _cookableHolderInteractPresnter.RefreshIngredientsIcon();
        CookStageAdded?.Invoke();
    }
}
