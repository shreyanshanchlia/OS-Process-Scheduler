using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityEnabler : MonoBehaviour
{
    public bool priority;
    public GameObject[] gameObjectsToEnable;

    void Update()
    {
        PriorityGameObjectControl();
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
