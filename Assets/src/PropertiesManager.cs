using UnityEngine;
using UnityEngine.UI;
public class PropertiesManager : MonoBehaviour
{
    public GameObject Properties;
    public VariableContentSizeFitter fitter;
    public bool PropertiesVisible = true;
    public Sprite downSprite, upSprite;
    public Image ArrowSprite;
    public void ShowHideProperties()
    {
        PropertiesVisible = Properties.activeSelf;
        Properties.SetActive(!PropertiesVisible);
        if(!PropertiesVisible)
        {
            ArrowSprite.sprite = upSprite;
        }
        else
        {
            ArrowSprite.sprite = downSprite;
        }
        fitter.FitSize();
    }
}
