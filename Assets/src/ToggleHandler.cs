using UnityEngine;
public class ToggleHandler : MonoBehaviour
{
    public GameObject toToggle;
    public void toggleOff(bool OffSwitch)
    {
        print(!OffSwitch);
        toToggle.SetActive(!OffSwitch);
    }
}
