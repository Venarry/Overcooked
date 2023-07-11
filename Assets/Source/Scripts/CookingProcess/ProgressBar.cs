using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _cookingProgressBackground;
    [SerializeField] private Image _cookingProgress;

    private void Start()
    {
        SetVisiable(false);
        _cookingProgress.fillAmount = 0;
    }

    public void SetValue(float progress)
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

    private void SetVisiable(bool state)
    {
        _cookingProgress.enabled = state;
        _cookingProgressBackground.enabled = state;
    }
}
