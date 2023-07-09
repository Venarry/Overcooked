using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CookingProcess))]
public class CookingProcessView : MonoBehaviour
{
    [SerializeField] private Image _cookingProgressBackground;
    [SerializeField] private Image _cookingProgress;
    [SerializeField] private MeshFilter _model;

    private CookingProcess _cookingProcess;

    private void Awake()
    {
        _cookingProcess = GetComponent<CookingProcess>();
    }

    private void Start()
    {
        SetVisiable(false);
        _cookingProgress.fillAmount = 0;
    }

    private void OnEnable()
    {
        _cookingProcess.NextCookingStep += OnNextCookingStep;
        _cookingProcess.NextCookingStage += OnNextCookStage;
    }

    private void OnDisable()
    {
        _cookingProcess.NextCookingStep -= OnNextCookingStep;
    }

    public void OnNextCookingStep(float progress)
    {
        if(progress == 0f || progress >= 1f)
        {
            SetVisiable(false);
        }
        else
        {
            SetVisiable(true);
        }

        _cookingProgress.fillAmount = progress;
    }

    public void OnNextCookStage(Mesh mesh)
    {
        _model.mesh = mesh;
    }

    private void SetVisiable(bool state)
    {
        _cookingProgress.enabled = state;
        _cookingProgressBackground.enabled = state;
    }
}
