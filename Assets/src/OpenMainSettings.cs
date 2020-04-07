using UnityEngine;

public class OpenMainSettings : MonoBehaviour
{
    [ReadOnly] public GameObject mainSettings;
    private void Start()
    {
        mainSettings = GameObject.FindGameObjectWithTag("Settings");
    }
    public void OpenSettings()
    {
        mainSettings?.GetComponent<RectTransform>().SetAsLastSibling();        
    }
}
