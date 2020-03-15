using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SpeedSliderControl : MonoBehaviour
{
    public Slider SpeedSlider;
    public Toggle speedToggle;
    [ReadOnly] public float speed = 1.0f;
    public TMP_InputField speedText;
    public void Adjust(float value)
    {
        if (value == 0)
        {
            speed = 0.0f;
            speedToggle.isOn = false;
        }
        else if (value < 10)
        {
            speed = value / 10;
            speedToggle.isOn = true;
        }
        else
        {
            speed = value - 9;
            speedToggle.isOn = true;
        }
        RefereshSpeed();
    }
    public void RefereshSpeed()
    {
        Time.timeScale = speed;
        speedText.text = speed.ToString();
    }
    public void AddValue()
    {
        if(!speedToggle.isOn)
        {
            speedToggle.isOn = true;
            SpeedSlider.value = 1;
            return;
        }
        SpeedSlider.value += 1;
    }
    public void SubtractValue()
    {
        SpeedSlider.value -= 1;
    }
}
