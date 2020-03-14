using UnityEngine;

public class PropertiesData : MonoBehaviour
{
    public string ProcessName = "Process";
    public int ArrivalTime = 0;
    public int BurstTime = 1;
    public int Priority = 0;

    public void UpdateProcessName(string _ProcessName)
    {
        ProcessName = _ProcessName;
    }
    public void UpdateArrivalTime(string _ArrivalTime)
    {
        ArrivalTime = int.Parse(_ArrivalTime);
    }
    public void UpdateBurstTime(string _BurstTime)
    {
        BurstTime = int.Parse(_BurstTime);
    }
    public void UpdatePriority(string _Priority)
    {
        Priority = int.Parse(_Priority);
    }
}
