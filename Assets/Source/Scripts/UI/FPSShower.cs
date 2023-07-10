using System.Collections;
using TMPro;
using UnityEngine;

public class FPSShower : MonoBehaviour
{
    private const float ShowDelay = 0.2f;
    
    [SerializeField] private TextMeshProUGUI _label;
    private readonly WaitForSeconds _waitForSeconds = new(ShowDelay);

    private void Start()
    {
        StartCoroutine(ShowFPS());
        //Application.targetFrameRate = 22;
    }

    private IEnumerator ShowFPS()
    {
        while (true)
        {
            const float FrameCount = 1;
            _label.text = Mathf.Round(FrameCount / Time.deltaTime).ToString();
            yield return _waitForSeconds;
        }
    }
}
