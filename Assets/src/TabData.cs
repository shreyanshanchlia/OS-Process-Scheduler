using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TabData : MonoBehaviour
{
    public enum schedulerType
    { FirstComeFirstServe, ShortestJobFirst, RoundRobin, ShortestRemainingTimeFirst};
    [Tooltip("FirstComeFirstServe, ShortestJobFirst, RoundRobin, ShortestRemainingTimeFirst")]
    public int Scheduler;
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
}
