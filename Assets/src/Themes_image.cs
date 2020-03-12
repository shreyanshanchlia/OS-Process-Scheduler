using System;
using UnityEngine;
using UnityEngine.UI;
public class Themes_image : MonoBehaviour
{
    public Sprite dark_image, light_image;
    public Image refImage;
    [Header("theme for color")]
    public bool useColor;
    public Color32 dark_color = new Color32(32,32,32,255), light_color = new Color32(223, 223, 223, 255);
    void Start()
    {
        changeTheme();
    }

    void changeTheme()
    {
        int theme = PlayerPrefs.GetInt("theme", 0);
        if (theme == 0) return;
        if (!useColor)
        {
            changeThemeImage();
        }
        else
        {
            changeThemeColor();
        }
    }

    private void changeThemeColor()
    {
        refImage.color = light_color;
    }

    void changeThemeImage()
    {
        refImage.sprite = light_image;
    }
}
