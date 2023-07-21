using System;
using UnityEngine;

[RequireComponent(typeof(InteractedObjectView))]
[RequireComponent(typeof(ProgressBar))]
public class CookingPotView : MonoBehaviour, ICookableHolder, ICookable, IPickable
{
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private Transform _ingredientsIconPoint;
    [SerializeField] private MeshFilter _meshFilter;

    private InteractedObjectView _interactive;
    private CookingProcessPresenter _cookingProcessPresenter;
    private CookableHolderInteractPresenter _cookableHolderInteractPresnter;

    public KitchenObjectType Type => _cookingProcessPresenter.Type;
    public int CookablesCount => _cookableHolderInteractPresnter.CookableCount;
    public float CookablesCookedTime => _cookableHolderInteractPresnter.CookablesCookedTime;
    public float MaxCookedTime => _cookingProcessPresenter.MaxCookedTime;
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
        _cookingProcessPresenter.CookStageSubtracted += OncookStageSubtracted;
        _cookingProcessPresenter.MaxStageReached += SetOvercookedStage;

        _cookableHolderInteractPresnter.HolderCleared += OnHolderCleared;
        _cookableHolderInteractPresnter.CookablesChanged += OnCookablesChanged;
        _cookableHolderInteractPresnter.CookableAdded += OnCookableAdded;

        _cookingProcessPresenter.Enable();
        _cookableHolderInteractPresnter.Enable();
    }

    public void Disable()
    {
        _cookingProcessPresenter.CookStageAdded -= OnCookStageAdded;
        _cookingProcessPresenter.CookStageSubtracted -= OncookStageSubtracted;
        _cookingProcessPresenter.MaxStageReached -= SetOvercookedStage;

        _cookableHolderInteractPresnter.HolderCleared -= OnHolderCleared;
        _cookableHolderInteractPresnter.CookablesChanged -= OnCookablesChanged;
        _cookableHolderInteractPresnter.CookableAdded -= OnCookableAdded; // добавить возвращение в процессе при добавлении еды на событие добавление еды и возвращение на прошлую стадию и добавление времени готовки если стадия больше начальной

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
        _cookingProcessPresenter.SetMaxCookedTime(CookablesCookedTime);
        CookStageAdded?.Invoke();
    }

    private void OncookStageSubtracted()
    {
        _cookableHolderInteractPresnter.SubtractCookableCookStage();
        _cookableHolderInteractPresnter.RefreshIngredientsIcon();
        _cookingProcessPresenter.SetMaxCookedTime(CookablesCookedTime);
    }

    private void OnCookablesChanged(ICookable[] cookables)
    {
        _cookingProcessPresenter.RecalculateCookedTime(CookablesCookedTime);
    }

    private void OnCookableAdded(ICookable cookable)
    {
        //_cookingProcessPresenter.RecalculateCookedTime(cookable.MaxCookedTime);
    }

    private void OnHolderCleared()
    {
        _cookingProcessPresenter.ResetStages();
    }
}
