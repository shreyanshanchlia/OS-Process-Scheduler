using System;
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
    public GameObject AddTabButton;
    public int MaxTabs;
    [ReadOnly] public int CurrentTabCount;
    void Start()
    {
        instance = this;
        WindowLauncher_RectTransform.SetAsLastSibling();
        CurrentTabCount += 1;
    }
    public void AddTab()
    {
        if (CurrentTabCount >= MaxTabs)
        {
            return;
        }
        CurrentTabCount++;
        HideTabs();
        GameObject @object = Instantiate(MainTabPrefab, TabsHolder.transform);
        GameObject tab = Instantiate(ButtonTabPrefab, ButtonTabHolder.transform);
        buttonTabs.Add(tab.GetComponent<TabManager>());
        buttonTabs[buttonTabs.Count - 1].LinkedTab = @object;
        CheckTabs();
    }
    public void reduceTabsCount()
    {
        CurrentTabCount--;
        CheckTabs();
    }
    private void CheckTabs()
    {
        if(CurrentTabCount < MaxTabs)
        {
            AddTabButton.SetActive(true);
        }
        else
        {
            AddTabButton.SetActive(false);
        }
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
