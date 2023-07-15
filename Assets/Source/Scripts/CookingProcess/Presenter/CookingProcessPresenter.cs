using System;
using UnityEngine;

public class CookingProcessPresenter
{
    private CookStagesSO _cookStages;
    private CookingProcess _cookingProcess;
    private ProgressBar _progressBar;
    private CookStageMeshShower _meshShower;

    public event Action CookStageChanged;

    public KitchenObjectType Type => _cookingProcess.Type;
    public KitchenObjectType[] AvailablePlaceTypes => _cookingProcess.AvailablePlaceTypes;

    public CookingProcessPresenter(CookStagesSO cookStages, MeshFilter model, ProgressBar progressBar)
    {
        _cookStages = cookStages;
        _cookingProcess = new CookingProcess(_cookStages);
        _meshShower = new CookStageMeshShower(model);
        _progressBar = progressBar;

        _meshShower.ShowMesh(_cookStages.GetMeshByIndex(_cookingProcess.CurrentCookedStage));
    }

    public void Enable()
    {
        _cookingProcess.CookStepChanged += OnCookStepChanged;
        _cookingProcess.CookStageChanged += OnCookStageChanged;
    }

    public void Disable()
    {
        _cookingProcess.CookStepChanged -= OnCookStepChanged;
        _cookingProcess.CookStageChanged -= OnCookStageChanged;
    }

    public void Cook(float step = 0)
    {
        _cookingProcess.CookNextStep(step);
    }

    public void AddCookStage()
    {
        _cookingProcess.AddCookStage();
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

    private void OnCookStageChanged()
    {
        _meshShower.ShowMesh(_cookStages.GetMeshByIndex(_cookingProcess.CurrentCookedStage));
        CookStageChanged?.Invoke();
    }
}
