using UnityEngine;
using UnityEngine.UI;
public class TabsManager : MonoBehaviour
{
    public RectTransform WindowLauncher_RectTransform;

    void Start()
    {
        WindowLauncher_RectTransform.SetAsLastSibling();
    }

}
