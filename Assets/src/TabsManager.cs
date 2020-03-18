using UnityEngine;
using UnityEngine.UI;
public class TabsManager : MonoBehaviour
{
    public RectTransform WindowLauncher_RectTransform;
    public GameObject MainTabPrefab;
    public GameObject TabsHolder;
    public GameObject ButtonTabPrefab;
    public GameObject ButtonTabHolder;
    void Start()
    {
        WindowLauncher_RectTransform.SetAsLastSibling();
    }
    public void AddTab()
    {
        GameObject @object = Instantiate(MainTabPrefab, TabsHolder.transform);
        GameObject tab = Instantiate(ButtonTabPrefab, ButtonTabHolder.transform);
        tab.GetComponent<TabManager>().LinkedTab = @object;
    }
}
