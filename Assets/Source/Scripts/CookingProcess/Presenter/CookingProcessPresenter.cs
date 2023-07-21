using System;
using UnityEngine;

public class CookingProcessPresenter : ITypeProvider
{
    private readonly CookableIngredientSO _cookStages;
    private readonly CookingProcessModel _cookingProcessModel;
    private readonly ProgressBar _progressBar;
    private readonly CookStageMeshShower _meshShower;

    public event Action CookStageAdded;
    public event Action CookStageSubtracted;
    public event Action MaxStageReached;

    public KitchenObjectType Type => _cookingProcessModel.Type;
    public KitchenObjectType[] AvailablePlaceTypes => _cookingProcessModel.AvailablePlaceTypes;
    public float MaxCookedTime => _cookingProcessModel.MaxCookedTime;

    public CookingProcessPresenter(CookableIngredientSO cookStages, MeshFilter meshFilter, ProgressBar progressBar)
    {
        _cookStages = cookStages;
        _cookingProcessModel = new CookingProcessModel(_cookStages);
        _meshShower = new CookStageMeshShower(meshFilter);
        _progressBar = progressBar;

        _meshShower.ShowMesh(_cookStages.GetMeshByIndex(_cookingProcessModel.CurrentCookedStage));
    }

    public void Enable()
    {
        _cookingProcessModel.CookStepChanged += OnCookStepChanged;
        _cookingProcessModel.CookStageAdded += OnCookStageAdded;
        _cookingProcessModel.CookStageSubtracted += OnCookStageSubtracted;
        _cookingProcessModel.MaxStageReached += OnMaxStageReached;
    }

    public void Disable()
    {
        _cookingProcessModel.CookStepChanged -= OnCookStepChanged;
        _cookingProcessModel.CookStageAdded -= OnCookStageAdded;
        _cookingProcessModel.CookStageSubtracted -= OnCookStageSubtracted;
        _cookingProcessModel.MaxStageReached -= OnMaxStageReached;
    }

    public void Cook(float step = 0)
    {
        _cookingProcessModel.Cook(step);
    }

    public void AddCookStage()
    {
        _cookingProcessModel.AddCookStage();
    }

    public void SubtractCookStage()
    {
        _cookingProcessModel.SubtractCookStage();
    }

    public void SetOvercookedStage()
    {
        _cookingProcessModel.SetOvercookedStage();
    }

    public void ResetStages()
    {
        _cookingProcessModel.ResetStages();
    }

    public void ResetStageProgress()
    {
        _cookingProcessModel.ResetStageProgress();
    }

    public void SetMaxCookedTime(float time)
    {
        _cookingProcessModel.SetMaxCookedTime(time);
    }

    public void RecalculateCookedTime(float newTime)
    {
        _cookingProcessModel.RecalculateCookedTime(newTime);
    }

    private void OnCookStepChanged()
    {
        float cookedTime = _cookingProcessModel.MaxCookedTime;

        if (cookedTime != 0)
            _progressBar.SetValue(_cookingProcessModel.PastCookedTime / cookedTime);
        else
            _progressBar.SetValue(cookedTime);
    }

    private void OnCookStageAdded()
    {
        _meshShower.ShowMesh(_cookStages.GetMeshByIndex(_cookingProcessModel.CurrentCookedStage));
        CookStageAdded?.Invoke();
    }

    private void OnCookStageSubtracted()
    {
        _meshShower.ShowMesh(_cookStages.GetMeshByIndex(_cookingProcessModel.CurrentCookedStage));
        CookStageSubtracted?.Invoke();
    }

    private void OnMaxStageReached()
    {
        MaxStageReached?.Invoke();
    }
}
