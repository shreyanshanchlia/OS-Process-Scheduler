using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject ToDestroy;

    public void destroyObject()
    {
        if (ToDestroy == null)
            return;
        foreach(var tabData in FindObjectsOfType<TabData>())
        {
            tabData.RefreshData();
        }
        Destroy(ToDestroy);
    }
}
