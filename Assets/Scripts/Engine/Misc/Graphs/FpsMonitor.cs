using TMPro;
using UnityEngine;

public class FpsMonitor : MonoBehaviour
{
    [Header("Komponenty")]
    public TextMeshProUGUI textFpsValue;
    public TextMeshProUGUI textMsValue;


    [Header("Wartosci")]
    public int updateRate = 4;

    [Header("Thresholdy")]
    public int goodFpsThreshold = 60;
    public Color goodFpsThresholdColor;
    public Color badFpsThresholdColor;


    [Header("Debug")]
    [ReadOnly] public ushort currentFps;
    [ReadOnly] public float currentMs;

    private float _deltaTime;
    private int _frameCount;


    private void Update()
    {
        _deltaTime += Time.unscaledDeltaTime;
        _frameCount++;

        if (_deltaTime > 1f / updateRate)
        {
            currentFps = (ushort)Mathf.RoundToInt(_frameCount / _deltaTime);
            currentMs = _deltaTime / _frameCount * 1000f;

            textFpsValue.color = currentFps >= goodFpsThreshold ? goodFpsThresholdColor : badFpsThresholdColor;
            textFpsValue.SetText(currentFps.ToString());

            textMsValue.SetText(currentMs.ToString("0.0"));

            _deltaTime = 0;
            _frameCount = 0;
        }
    }
}
