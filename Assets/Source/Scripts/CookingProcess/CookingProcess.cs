using System;
using System.Collections.Generic;
using UnityEngine;

public class CookingProcess
{
    private CookStagesSO _stages;
    private int _currentCookedStage;

    public CookingProcess(CookStagesSO stages)
    {
        _stages = stages;
        RefreshData();
    }

    public float CookedTime { get; private set; }
    public float PastCookedTime { get; private set; }

    public KitchenObjectType Type => _stages.GetTypeByIndex(_currentCookedStage);
    public KitchenObjectType[] AvailablePlaceTypes => _stages.GetAvailablePlaceTypesTimeByIndex(_currentCookedStage);

    public event Action<Mesh> NextCookingStage;

    public void CookNextStage()
    {
        if (_currentCookedStage >= _stages.MaxCookIndexStage)
            return;

        AddCookingStage();
    }

    public void CookNextStep(float step = 0)
    {
        if (_currentCookedStage >= _stages.MaxCookIndexStage)
            return;

        if(step == 0)
        {
            PastCookedTime += Time.deltaTime;
        }
        else
        {
            PastCookedTime += step;
        }

        if (PastCookedTime >= CookedTime)
        {
            AddCookingStage();
        }
    }

    public void ResetStages()
    {
        _currentCookedStage = 0;
        RefreshData();
    }

    private void AddCookingStage()
    {
        _currentCookedStage++;
        PastCookedTime = 0;

        RefreshData();

        NextCookingStage?.Invoke(_stages.GetMeshByIndex(_currentCookedStage));
    }

    private void RefreshData()
    {
        CookedTime = _stages.GetCookingTimeByIndex(_currentCookedStage);
    }
}
