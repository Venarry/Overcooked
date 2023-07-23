using System;
using UnityEngine;

public class CookingProcessModel
{
    private readonly CookableIngredientSO _stages;

    public CookingProcessModel(CookableIngredientSO stages)
    {
        _stages = stages;
        MaxCookedTime = _stages.GetCookingTimeByIndex(CurrentCookedStage);
    }

    public int CurrentCookedStage { get; private set; }
    public float MaxCookedTime { get; private set; }
    public float PastCookedTime { get; private set; }

    public KitchenObjectType Type => _stages.GetTypeByIndex(CurrentCookedStage);
    public KitchenObjectType[] AvailablePlaceTypes => _stages.GetAvailablePlaceTypesTimeByIndex(CurrentCookedStage);

    public event Action CookStepChanged;
    public event Action CookStageAdded;
    public event Action CookStageSubtracted;
    public event Action MaxStageReached;

    public void AddCookStage()
    {
        if (CurrentCookedStage >= _stages.MaxCookIndexStage)
        {
            return;
        }

        CurrentCookedStage++;
        RefreshNewStageData();
        CookStageAdded?.Invoke();

        if(CurrentCookedStage == _stages.MaxCookIndexStage)
        {
            MaxStageReached?.Invoke();
        }
    }

    public void SubtractCookStage()
    {
        if (CurrentCookedStage == 0)
            return;

        CurrentCookedStage--;
        RefreshNewStageData();
        CookStageSubtracted?.Invoke();
    }

    public void SetOvercookedStage()
    {
        CurrentCookedStage = _stages.MaxCookIndexStage;
        RefreshNewStageData();
        CookStageAdded?.Invoke();
    }

    public void SetMaxCookedTime(float time)
    {
        if (time < 0)
            time = 0;

        MaxCookedTime = time;
    }

    public void RecalculateCookedTime(float newTime)
    {
        float deltaTime = newTime - MaxCookedTime;

        if (CurrentCookedStage > 0)
        {
            CurrentCookedStage--;

            if (PastCookedTime < deltaTime)
            {
                PastCookedTime = MaxCookedTime + PastCookedTime;
            }
            else
            {
                PastCookedTime = MaxCookedTime;
            }

            CookStageSubtracted?.Invoke();
        }

        SetMaxCookedTime(newTime);
    }

    public void Cook(float step = 0)
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

        if (PastCookedTime >= MaxCookedTime)
        {
            AddCookStage();
        }
    }

    public void ResetStages()
    {
        if(CurrentCookedStage > 0)
        {
            CurrentCookedStage = 0;
            CookStageSubtracted?.Invoke();
        }
        
        if(PastCookedTime > 0)
        {
            PastCookedTime = 0;
            CookStepChanged?.Invoke();
        }

        MaxCookedTime = _stages.GetCookingTimeByIndex(CurrentCookedStage);
    }

    public void ResetStageProgress()
    {
        PastCookedTime = 0;
        CookStepChanged?.Invoke();
    }

    private void RefreshNewStageData()
    {
        PastCookedTime = 0;
        MaxCookedTime = _stages.GetCookingTimeByIndex(CurrentCookedStage);
    }
}
