using System;
using UnityEngine;

[RequireComponent(typeof(ProgressBar))]
public class CookigProcessPresenter : MonoBehaviour
{
    [SerializeField] private CookStagesSO _cookStages;
    [SerializeField] private MeshFilter _model;

    private CookingProcess _cookingProcess;
    private ProgressBar _progressBar;
    private CookStageMeshShower _meshShower;

    public event Action CookStageChanged;

    public KitchenObjectType Type => _cookingProcess.Type;
    public KitchenObjectType[] AvailablePlaceTypes => _cookingProcess.AvailablePlaceTypes;

    private void Awake()
    {
        _cookingProcess = new CookingProcess(_cookStages);
        _meshShower = new CookStageMeshShower(_model);
        _progressBar = GetComponent<ProgressBar>();
        _meshShower.ShowMesh(_cookStages.GetMeshByIndex(_cookingProcess.CurrentCookedStage));
    }

    private void OnEnable()
    {
        _cookingProcess.CookStageChanged += OnCookStageChanged;
        _cookingProcess.CookStepChanged += OnCookStepChanged;
    }

    private void OnDisable()
    {
        _cookingProcess.CookStageChanged -= OnCookStageChanged;
        _cookingProcess.CookStepChanged -= OnCookStepChanged;
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
