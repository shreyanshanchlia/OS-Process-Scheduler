using UnityEngine;

public class AddObject : MonoBehaviour
{
    public GameObject PrefabToInstanciate;
    public GameObject parentGameObject;
    public void AddPrefabInstance()
    {
        Instantiate(PrefabToInstanciate, parentGameObject.transform);
    }
}
