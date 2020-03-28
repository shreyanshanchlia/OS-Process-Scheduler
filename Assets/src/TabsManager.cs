using System.Collections.Generic;
using UnityEngine;

public class TabsManager : MonoBehaviour
{
    public static TabsManager instance;
    public RectTransform WindowLauncher_RectTransform;
    public GameObject MainTabPrefab;
    public GameObject TabsHolder;
    public GameObject ButtonTabPrefab;
    public GameObject ButtonTabHolder;
    public List<TabManager> buttonTabs;

    void Start()
    {
        instance = this;
        WindowLauncher_RectTransform.SetAsLastSibling();
    }
    public void AddTab()
    {
        HideTabs();
        GameObject @object = Instantiate(MainTabPrefab, TabsHolder.transform);
        GameObject tab = Instantiate(ButtonTabPrefab, ButtonTabHolder.transform);
        buttonTabs.Add(tab.GetComponent<TabManager>());
        buttonTabs[buttonTabs.Count - 1].LinkedTab = @object;
    }
    public void HideTabs()
    {
        foreach (var buttonTab in buttonTabs)
        {
            if(buttonTab!=null)
                buttonTab.HideTab();
        }
    }
}
