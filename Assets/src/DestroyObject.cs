using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject ToDestroy;

    public void destroyObject()
    {
        if (ToDestroy == null)
            return;
        Destroy(ToDestroy);
    }
}
