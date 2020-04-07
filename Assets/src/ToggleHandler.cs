using TMPro;
using UnityEngine;
public class ToggleHandler : MonoBehaviour
{
    public GameObject toToggle;
    public TextMeshProUGUI ToggleText;
    public string Offstring, Onstring;
    public void toggleOff(bool OffSwitch)
    {
        toToggle.SetActive(!OffSwitch);
        if(OffSwitch)
        {
            ToggleText.text = Onstring;
        }
        else
        {
            ToggleText.text = Offstring;
        }
    }
}
