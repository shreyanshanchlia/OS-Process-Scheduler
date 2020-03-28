using UnityEngine;
using UnityEngine.UI;
public class TabManager : MonoBehaviour
{
    public GameObject LinkedTab;
    public Image Tab_Image;
    public Sprite DarkTab;
    public Sprite HighlightedTab;
    public GameObject closeButton;
    public void CloseTab()
    {
        Destroy(LinkedTab);
        Destroy(this.gameObject);
    }
    public void OpenTab()
    {
        TabsManager.instance.HideTabs();
        closeButton.SetActive(false);
        LinkedTab.GetComponent<RectTransform>().SetAsLastSibling();
        Tab_Image.sprite = HighlightedTab;
    }
    public void HideTab()
    {
        closeButton.SetActive(true);
        Tab_Image.sprite = DarkTab;
    }
}
