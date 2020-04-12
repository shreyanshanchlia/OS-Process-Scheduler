using UnityEngine;
using System.Collections.Generic;

public class PropertiesData : MonoBehaviour
{
    [ReadOnly] public int ProcessId;
    public string ProcessName = "Process";
    public int ArrivalTime = 0;
    public int BurstTime = 1;
    public int Priority = 0;
    public float remainingBurstTime = 0;
    public List<KeyValuePair<float, float>> ProcessingChange;
    [HideInInspector] public GanttChartData chartData;
    private void Start()
    {
        ProcessId = GetInstanceID();
    }
    public void UpdateProcessName(string _ProcessName)
    {
        ProcessName = _ProcessName;
    }
    public void UpdateArrivalTime(string _ArrivalTime)
    {
        try
        {
            ArrivalTime = int.Parse(_ArrivalTime);
        }
        catch { }
    }
    public void UpdateBurstTime(string _BurstTime)
    {
        try
        {
            BurstTime = int.Parse(_BurstTime);
            remainingBurstTime = BurstTime;
        }
        catch { }
    }
    public void UpdatePriority(string _Priority)
    {
        try
        {
            Priority = int.Parse(_Priority);
        }
        catch { }
    }
}
