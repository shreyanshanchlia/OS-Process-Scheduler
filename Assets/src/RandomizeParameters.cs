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
        minArrivalTime = PlayerPrefs.GetInt("minArrivalTime", 0);
        maxArrivalTime = PlayerPrefs.GetInt("maxArrivalTime", 20);
        minBurstTime = PlayerPrefs.GetInt("minBurstTime", 1);
        maxBurstTime = PlayerPrefs.GetInt("maxBurstTime", 10);
        minPriority = PlayerPrefs.GetInt("minPriority", 0);
        maxPriority = PlayerPrefs.GetInt("maxPriority", 25);

        ArrivalTime.text = Random.Range(minArrivalTime, maxArrivalTime).ToString();
        BurstTime.text = Random.Range(minBurstTime, maxBurstTime).ToString();
        if (Priority != null)
            Priority.text = Random.Range(minPriority, maxPriority).ToString();
    }
}
