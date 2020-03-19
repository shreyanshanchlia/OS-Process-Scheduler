using UnityEngine;

public class ProcessManager : MonoBehaviour
{
    public GameObject Processes;
    public void ResetPage()
    {
        foreach(Transform process in Processes.transform)
        {
            Destroy(process.gameObject);
        }
    }
}
