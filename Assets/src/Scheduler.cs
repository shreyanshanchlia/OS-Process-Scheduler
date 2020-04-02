using System.Collections;
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
    public GameObject ClearAllOnRefresh;
    [HideInInspector] public float SchedulerTime; 
    [HideInInspector] public float SchedulerDeltaTime;
    [HideInInspector] public bool running = false;
    [HideInInspector] public List<PropertiesData> ProcessList;
    public TextMeshProUGUI schedulerTimeText;
    private bool SchedulerPause = false;
    public void RunScheduler()     
    {
        SchedulerDeltaTime = 0.0f;
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
        SetTimerText();
        tabData.ResetPropertiesData();
        running = false;
        SchedulerPause = false;
        foreach (Transform element in ClearAllOnRefresh.transform)
        {
            Destroy(element.gameObject);
        }
        summaryManager.summaryDatas = new List<SummaryData>();
        fcfsScheduler.reset();
        sjfScheduler.reset();
        sjfPreemptiveScheduler.reset();
        roundRobinScheduler.reset();
        priorityScheduler.reset();
        priorityPreemptiveScheduler.reset();
    }
    public void PauseScheduler()
    {
        SchedulerPause = true;
        SchedulerDeltaTime = 0.0f;
    }
    public void StepScheduler()
    {
        //StartCoroutine(StepSchedulerEnumerator());
        if (tabData.Scheduler == 0)
        {
            fcfsScheduler.Step();
        }
        if (tabData.Scheduler == 1)
        {
            if (tabData.preemptive)
            {
                sjfPreemptiveScheduler.Step();
            }
            else
            {
                sjfScheduler.Step();
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
                priorityPreemptiveScheduler.Step();
            }
            else
            {
                priorityScheduler.Step();
            }
        }
    }
    //IEnumerator StepSchedulerEnumerator()
    //{
    //    SchedulerPause = true;
    //    SchedulerDeltaTime = Mathf.Ceil(SchedulerTime) - SchedulerTime;
    //    yield return null;
    //    SchedulerDeltaTime = 1.0f;
    //}
    public void makeSummary(PropertiesData CurrentlyProcessing, float SpeedAdjustment = 0.0f)
    {
        SummaryData summaryData = new SummaryData();
        summaryData.ProcessName = CurrentlyProcessing.ProcessName;
        summaryData.ArrivalTime = CurrentlyProcessing.ArrivalTime;
        summaryData.BurstTime = CurrentlyProcessing.BurstTime;
        summaryData.CompletionTime = (float)System.Math.Round(SchedulerTime - SpeedAdjustment, 2);
        summaryData.TurnAroundTime = summaryData.CompletionTime - (float)summaryData.ArrivalTime;
        summaryData.WaitingTime = (float)System.Math.Round((float)(summaryData.TurnAroundTime - summaryData.BurstTime), 2);
        summaryManager.summaryDatas.Add(summaryData);
    }
    private void Update()
    {
        if (running)
        {
            if (!SchedulerPause)
            {
                SchedulerDeltaTime = Time.deltaTime;
            }
            SchedulerTime += SchedulerDeltaTime;
            SetTimerText();
        }
    }
    public void SetTimerText()
    {
        schedulerTimeText.text = SchedulerTime.ToString("f4");
    }
}
