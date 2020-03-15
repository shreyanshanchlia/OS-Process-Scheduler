using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SpeedSliderControl : MonoBehaviour
{
    public Slider SpeedSlider;
    [ReadOnly] public float speed = 1.0f;
    public TMP_InputField speedText;
    public void Adjust(float value)
    {
        if (value == 0)
        {
            speed = 0.0f;
        }
        else if (value < 10)
        {
            speed = value / 10;
        }
        else
        {
            speed = value - 9;
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
        SpeedSlider.value += 1;
    }
    public void SubtractValue()
    {
        SpeedSlider.value -= 1;
    }
}
