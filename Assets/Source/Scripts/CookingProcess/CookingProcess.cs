using System;
using System.Collections.Generic;
using UnityEngine;

public class CookingProcess : MonoBehaviour
{
    [SerializeField] private List<CookStage> _stages = new List<CookStage>();

    private int _maxCookedStage;
    private int _currentCookedStage;
    private float _cookedTime;
    private float _currentCookedTime;

    public int CurrentStage => _currentCookedStage;
    public KitchenObjectType Type => _stages[_currentCookedStage].Type;
    public KitchenObjectType[] AvailablePlaceTypes => _stages[_currentCookedStage].AvailablePlaceTypes;
    private float _cookedProgress => _currentCookedTime / _cookedTime;

    public event Action<Mesh> NextCookingStage;
    public event Action<float> NextCookingStep;

    private void Start()
    {
        _maxCookedStage = _stages.Count - 1;
        RefreshData();
    }

    public void CookNextStage()
    {
        if (_currentCookedStage >= _maxCookedStage)
            return;

        AddCookingStage();
    }

    public void CookNextStep(float step = 0)
    {
        if (_currentCookedStage >= _maxCookedStage)
            return;

        if(step == 0)
        {
            _currentCookedTime += Time.deltaTime;
        }
        else
        {
            _currentCookedTime += step;
        }

        NextCookingStep?.Invoke(_cookedProgress);

        if (_currentCookedTime >= _cookedTime)
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
        _currentCookedTime = 0;

        RefreshData();

        NextCookingStage?.Invoke(_stages[_currentCookedStage].Mesh);
    }

    private void RefreshData()
    {
        //_model.mesh = _stages[_currentCookedStage].Mesh;
        _cookedTime = _stages[_currentCookedStage].CookingTime;
    }
}

[Serializable]
public class CookStage
{
    [SerializeField] private MeshFilter _mesh;
    [SerializeField] private float _cookingTime;
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private List<KitchenObjectType> _availablePlaceObjects;

    public Mesh Mesh => _mesh.sharedMesh;
    public float CookingTime => _cookingTime;
    public KitchenObjectType Type => _type;
    public KitchenObjectType[] AvailablePlaceTypes => _availablePlaceObjects.ToArray();
}
