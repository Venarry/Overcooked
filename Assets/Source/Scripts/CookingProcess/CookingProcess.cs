using System;
using UnityEngine;

public class CookingProcess
{
    private CookStagesSO _stages;

    public CookingProcess(CookStagesSO stages)
    {
        _stages = stages;
        CookedTime = _stages.GetCookingTimeByIndex(CurrentCookedStage);
    }

    public int CurrentCookedStage { get; private set; }
    public float CookedTime { get; private set; }
    public float PastCookedTime { get; private set; }

    public KitchenObjectType Type => _stages.GetTypeByIndex(CurrentCookedStage);
    public KitchenObjectType[] AvailablePlaceTypes => _stages.GetAvailablePlaceTypesTimeByIndex(CurrentCookedStage);

    public event Action CookStepChanged;
    public event Action CookStageChanged;

    public void AddCookStage()
    {
        if (CurrentCookedStage >= _stages.MaxCookIndexStage)
            return;

        CurrentCookedStage++;
        OnCookStageChanged();
    }

    public void SubtractCookStage()
    {
        if (CurrentCookedStage == 0)
            return;

        CurrentCookedStage--;
        OnCookStageChanged();
    }

    public void CookNextStep(float step = 0)
    {
        if (CurrentCookedStage >= _stages.MaxCookIndexStage)
            return;

        if(step == 0)
        {
            PastCookedTime += Time.deltaTime;
        }
        else
        {
            PastCookedTime += step;
        }

        CookStepChanged?.Invoke();

        if (PastCookedTime >= CookedTime)
        {
            AddCookStage();
        }
    }

    public void ResetStages()
    {
        CurrentCookedStage = 0;
        PastCookedTime = 0;
        CookedTime = _stages.GetCookingTimeByIndex(CurrentCookedStage);
    }

    private void OnCookStageChanged()
    {
        PastCookedTime = 0;
        CookedTime = _stages.GetCookingTimeByIndex(CurrentCookedStage);
        CookStageChanged?.Invoke();
    }
}
