using UnityEngine;

public class TabManager : MonoBehaviour
{
    public GameObject LinkedTab;
    public void CloseTab()
    {
        Destroy(LinkedTab);
        Destroy(this.gameObject);
    }
    public void OpenTab()
    {
        LinkedTab.GetComponent<RectTransform>().SetAsLastSibling();
    }
}
