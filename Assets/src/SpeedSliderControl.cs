using UnityEngine;
using TMPro;
public class SpeedSliderControl : MonoBehaviour
{
    [ReadOnly] public float speed = 1.0f;
    public TMP_InputField speedText;
    public void Adjust(float value)
    {
        if (value == 0)
        {
            speed = 0.0f;
        }
        else if (value <= 10)
        {
            speed = value / 10;
        }
        else
        {
            speed = value - 10;
        }
        RefereshSpeed();
    }
    public void RefereshSpeed()
    {
        Time.timeScale = speed;
        speedText.text = speed.ToString();
    }
}
