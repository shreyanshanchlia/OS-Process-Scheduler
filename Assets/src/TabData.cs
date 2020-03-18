using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public enum schedulerType
{ FirstComeFirstServe, ShortestJobFirst, RoundRobin, Priority};

public class TabData : MonoBehaviour
{
    [Tooltip("FirstComeFirstServe, ShortestJobFirst, RoundRobin, Priority")]
    public int Scheduler;
    public bool preemptive = true;
    public List<PropertiesData> propertiesDatas;

    public void AssignScheduler(int _scheduler)
    {
        Scheduler = _scheduler;
    }
    public void RefreshData()
    {
        StartCoroutine(Refresh());
    }
    IEnumerator Refresh()
    {
        yield return null;
        propertiesDatas.RemoveAll(item => item == null);
    }
    public void SetPreemptive(bool _preemptive)
    {
        preemptive = _preemptive;
    }
}
