using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Themes_image : MonoBehaviour
{
    public enum ThemeType
    {
        ImageSwap,
        ImageColor,
        TextColor,
        TMPColor,

    }
    public Sprite dark_image, light_image;
    public Image refImage;
    [Header("Theme Type")]
    public ThemeType themeType;
    public Color32 dark_color = new Color32(32,32,32,255), light_color = new Color32(223, 223, 223, 255);
    void Start()
    {
        changeTheme();
    }

    void changeTheme()
    {
        int theme = PlayerPrefs.GetInt("theme", 0);
        if (theme == 0) return;
        if (themeType == ThemeType.ImageSwap)
        {
            changeThemeImage();
        }
        else if(themeType == ThemeType.ImageColor)
        {
            changeThemeImageColor();
        }
        else
        {
            Debug.Log("Not yet implemented");
        }
    }

    private void changeThemeImageColor()
    {
        refImage.color = light_color;
    }

    void changeThemeImage()
    {
        refImage.sprite = light_image;
    }
}
