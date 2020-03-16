using UnityEngine;

public class AddObject : MonoBehaviour
{
    public GameObject PrefabToInstantiate;
    public GameObject parentGameObject;

    public TabData tabData;
    private GameObject InstantiatedGameObject;
    PropertiesData instantiatedPropertiesData;
    public void AddPrefabInstance()
    {
        InstantiatedGameObject = Instantiate(PrefabToInstantiate, parentGameObject.transform);
        try
        {
            instantiatedPropertiesData = InstantiatedGameObject.GetComponent<PropertiesData>();
            AddToData();
        }
        catch { }
    }
    public void AddToData()
    {
        tabData.propertiesDatas.Add(instantiatedPropertiesData);
        tabData.RefreshData();
    }
}
