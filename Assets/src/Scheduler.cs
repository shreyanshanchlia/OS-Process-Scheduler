using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Scheduler : MonoBehaviour
{
    [SerializeField] private TabData tabData;
    public SummaryManager summaryManager;
    public FCFSScheduler fcfsScheduler;
    [HideInInspector] public float SchedulerTime; 
    [HideInInspector] public bool running = false;
    [HideInInspector] public List<PropertiesData> ProcessList;
    public TextMeshProUGUI schedulerTimeText;
    public void RunScheduler()     
    {
        ProcessList = new List<PropertiesData>();
        ProcessList = tabData.propertiesDatas;
        running = true;
        if (tabData.Scheduler == 0)
        {
            fcfsScheduler.run();
        }
    }
    public void makeSummary(PropertiesData CurrentlyProcessing)
    {
        SummaryData summaryData = new SummaryData();
        summaryData.ProcessName = CurrentlyProcessing.ProcessName;
        summaryData.ArrivalTime = CurrentlyProcessing.ArrivalTime;
        summaryData.BurstTime = CurrentlyProcessing.BurstTime;
        summaryData.CompilationTime = SchedulerTime;
        summaryData.TurnAroundTime = summaryData.CompilationTime - summaryData.ArrivalTime;
        summaryData.WaitingTime = summaryData.TurnAroundTime - summaryData.BurstTime;
        summaryManager.summaryDatas.Add(summaryData);
    }
    private void Update()
    {
        if (running)
        {
            SchedulerTime += Time.deltaTime * Time.timeScale;
            schedulerTimeText.text = SchedulerTime.ToString("f4");
        }
    }
}
