using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Scheduler : MonoBehaviour
{
    [SerializeField] private TabData tabData;
    public SummaryManager summaryManager;
    public FCFSScheduler fcfsScheduler;
    public SJFScheduler sjfScheduler;
    public SJFPreemptiveScheduler sjfPreemptiveScheduler;
    public RoundRobinScheduler roundRobinScheduler;
    public PriorityScheduler priorityScheduler;
    public PriorityPreemptiveScheduler priorityPreemptiveScheduler;
    [HideInInspector] public float SchedulerTime; 
    [HideInInspector] public float SchedulerDeltaTime;
    [HideInInspector] public bool running = false;
    [HideInInspector] public List<PropertiesData> ProcessList;
    public TextMeshProUGUI schedulerTimeText;
    private bool SchedulerPause = false;
    public void RunScheduler()     
    {
        SchedulerDeltaTime = Time.deltaTime * Time.timeScale;
        if (running && SchedulerPause)
        {
            SchedulerPause = false;
            return;
        }
        ProcessList = new List<PropertiesData>();
        ProcessList = tabData.propertiesDatas;
        running = true;
        if (tabData.Scheduler == 0)
        {
            fcfsScheduler.run();
        }
        if (tabData.Scheduler == 1)
        {
            if (tabData.preemptive)
            {
                sjfPreemptiveScheduler.run();
            }
            else
            {
                sjfScheduler.run();
            }
        }
        if (tabData.Scheduler == 2)
        {
            if (tabData.preemptive)
            {
                roundRobinScheduler.run();
            }
            else
            {
                roundRobinScheduler.run();
            }
        }
        if (tabData.Scheduler == 3)
        {
            if (tabData.preemptive)
            {
                priorityPreemptiveScheduler.run();
            }
            else
            {
                priorityScheduler.run();
            }
        }
    }
    public void SchedulerReset()
    {
        SchedulerTime = 0.0f;
        schedulerTimeText.text = SchedulerTime.ToString("f4");
        running = false;
        SchedulerPause = false;
    }
    public void PauseScheduler()
    {
        SchedulerPause = true;
        SchedulerDeltaTime = 0.0f;
    }
    public void StepScheduler()
    {
        SchedulerPause = true;
        SchedulerTime = Mathf.Floor(SchedulerTime);
        SchedulerDeltaTime = 1.0f;
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
            if (!SchedulerPause)
            {
                SchedulerDeltaTime = Time.deltaTime * Time.timeScale;
            }
            SchedulerTime += SchedulerDeltaTime;
            schedulerTimeText.text = SchedulerTime.ToString("f4");
        }
    }
}
