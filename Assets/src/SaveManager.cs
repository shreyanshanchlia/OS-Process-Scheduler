using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public string SaveString;
    public TMP_InputField inputField;
    public void LoadPrefs()
    {
        if (PlayerPrefs.HasKey(SaveString))
        {
            inputField.text = PlayerPrefs.GetInt(SaveString).ToString();
        }
        else
        {
            PlayerPrefs.SetInt(SaveString, int.Parse(inputField.text));
        }
    }
    public void SavePrefs(string value)
    {
        PlayerPrefs.SetInt(SaveString, int.Parse(value));
    }
    private void OnEnable()
    {
        LoadPrefs();
    }
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
    }
}
