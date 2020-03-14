using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertiesManager : MonoBehaviour
{
    public GameObject Properties;
    public VariableContentSizeFitter fitter;
    public bool PropertiesVisible = true;
    public void ShowHideProperties()
    {
        PropertiesVisible = Properties.activeSelf;
        Properties.SetActive(!PropertiesVisible);
        fitter.FitSize();
    }
}
