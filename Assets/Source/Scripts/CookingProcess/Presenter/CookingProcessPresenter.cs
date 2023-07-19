using System;
using UnityEngine;

public class CookingProcessPresenter : ITypeProvider
{
    private readonly CookableIngredientSO _cookStages;
    private readonly CookingProcess _cookingProcess;
    private readonly ProgressBar _progressBar;
    private readonly CookStageMeshShower _meshShower;

    public event Action CookStageAdded;
    public event Action CookStageSubtracted;

    public KitchenObjectType Type => _cookingProcess.Type;
    public KitchenObjectType[] AvailablePlaceTypes => _cookingProcess.AvailablePlaceTypes;

    public CookingProcessPresenter(CookableIngredientSO cookStages, MeshFilter meshFilter, ProgressBar progressBar)
    {
        _cookStages = cookStages;
        _cookingProcess = new CookingProcess(_cookStages);
        _meshShower = new CookStageMeshShower(meshFilter);
        _progressBar = progressBar;

        _meshShower.ShowMesh(_cookStages.GetMeshByIndex(_cookingProcess.CurrentCookedStage));
    }

    public void Enable()
    {
        _cookingProcess.CookStepChanged += OnCookStepChanged;

        _cookingProcess.CookStageAdded += OnCookStageAdded;
        _cookingProcess.CookStageSubtracted += OnCookStageSubtracted;
    }

    public void Disable()
    {
        _cookingProcess.CookStepChanged -= OnCookStepChanged;

        _cookingProcess.CookStageAdded -= OnCookStageAdded;
        _cookingProcess.CookStageSubtracted -= OnCookStageSubtracted;
    }

    public void Cook(float step = 0)
    {
        _cookingProcess.CookNextStep(step);
    }

    public void AddCookStage()
    {
        _cookingProcess.AddCookStage();
    }

    public void SubtractCookStage()
    {
        _cookingProcess.SubtractCookStage();
    }

    public void ResetStages()
    {
        _cookingProcess.ResetStages();
    }

    public void ResetStageProgress()
    {
        _cookingProcess.ResetStageProgress();
    }

    private void OnCookStepChanged()
    {
        float cookedTime = _cookingProcess.CookedTime;

        if (cookedTime != 0)
            _progressBar.SetValue(_cookingProcess.PastCookedTime / cookedTime);
        else
            _progressBar.SetValue(cookedTime);
    }

    private void OnCookStageAdded()
    {
        _meshShower.ShowMesh(_cookStages.GetMeshByIndex(_cookingProcess.CurrentCookedStage));
        CookStageAdded?.Invoke();
    }

    private void OnCookStageSubtracted()
    {
        _meshShower.ShowMesh(_cookStages.GetMeshByIndex(_cookingProcess.CurrentCookedStage));
        CookStageSubtracted?.Invoke();
    }
}
