using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityEnabler : MonoBehaviour
{
    public bool priority;
    public GameObject[] gameObjectsToEnable;
    public VariableContentSizeFitter sizeFitter;
    public VariableContentSizeFitter sizeFitterParent;
    private bool PreviousPriority = false;
    void Update()
    {
        priority = this.transform.parent.gameObject.GetComponent<PriorityManager>().priority;
        if(priority != PreviousPriority)
        {
            PriorityGameObjectControl();
            sizeFitter.FitSize();
            sizeFitterParent.FitSize();
            PreviousPriority = priority;
        }
    }

    private void PriorityGameObjectControl()
    {
        if (priority)
        {
            foreach (var @object in gameObjectsToEnable)
            {
                @object.SetActive(true);
            }
        }
        else
        {
            foreach(var @object in gameObjectsToEnable)
            {
                @object.SetActive(false);
            }
        }
    }
}
