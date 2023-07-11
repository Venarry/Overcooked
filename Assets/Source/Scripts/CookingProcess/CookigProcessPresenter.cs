using UnityEngine;

[RequireComponent(typeof(ProgressBar))]
public class CookigProcessPresenter : MonoBehaviour
{
    [SerializeField] private CookStagesSO _cookStages;
    private CookingProcess _cookingProcess;
    private ProgressBar _progressBar;

    public KitchenObjectType Type => _cookingProcess.Type;
    public KitchenObjectType[] AvailablePlaceTypes => _cookingProcess.AvailablePlaceTypes;

    private void Awake()
    {
        _cookingProcess = new CookingProcess(_cookStages);
        _progressBar = GetComponent<ProgressBar>();
    }

    public void Init(CookStagesSO stages)
    {
        _cookingProcess = new CookingProcess(stages);
        _progressBar = GetComponent<ProgressBar>();
    }

    public void Cook(float step = 0)
    {
        _cookingProcess.CookNextStep(step);
        _progressBar.SetValue(_cookingProcess.PastCookedTime / _cookingProcess.CookedTime);
    }
}
