using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class UI_SliderController : MonoBehaviour
{
    Slider slider;
    public void Init(int MaxValue)
    {
        slider = GetComponent<Slider>();
        slider.maxValue = MaxValue;
    }

    public void Set(int value)
    {
        if (slider.maxValue == 0)
        {
            slider.maxValue = (int)(StageManager.Instance.GetMap().GetAreaCount() * 0.8f);
        }
        slider.value = value;
    }
}
