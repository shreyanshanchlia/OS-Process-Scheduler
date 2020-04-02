using TMPro;
using UnityEngine;

public class RandomizeParameters : MonoBehaviour
{
    public TMP_InputField ArrivalTime;
    public int minArrivalTime, maxArrivalTime;
    public TMP_InputField BurstTime;
    public int minBurstTime, maxBurstTime;
    public TMP_InputField Priority;
    public int minPriority, maxPriority;

    public void RandomizeValues()
    {
        ArrivalTime.text = Random.Range(minArrivalTime, maxArrivalTime).ToString();
        BurstTime.text = Random.Range(minBurstTime, maxBurstTime).ToString();
        if(Priority!=null)
            Priority.text = Random.Range(minPriority, maxPriority).ToString();
    }
}
