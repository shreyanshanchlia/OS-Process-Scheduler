using UnityEngine.UI;
using UnityEngine;

public class ToggleSlider : MonoBehaviour
{
    public Slider ToToggleSlider;
    [SerializeField] private int defaultSliderValue = 10;
    private void Start()
    {
        defaultSliderValue = PlayerPrefs.GetInt("DefaultSpeed", 10);
    }
    public void SliderToZero()
    {
        ToToggleSlider.value = 0;
    }
    public void SliderToDefault()
    {
        ToToggleSlider.value = defaultSliderValue;
    }
    public void toggleSlider()
    {
        if (ToToggleSlider.value != 0)
        {
            SliderToZero();
        }
        else
        {
            SliderToDefault();
        }
    }
    public void toggleSliderOnValue(bool value)
    {
        if (!value)
        {
            SliderToZero();
        }
        else
        {
            SliderToDefault();
        }
    }
}
