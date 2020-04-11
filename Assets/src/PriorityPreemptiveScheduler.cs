using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityPreemptiveScheduler : MonoBehaviour
{
    public Scheduler scheduler;
    public ChartMaker chartMaker;
    private List<PropertiesData> arrived;
    private List<PropertiesData> waiting;
    bool running = false;
    bool processing = false;
    PropertiesData CurrentlyProcessing;
    private float ProcessorFreeAt = 0.0f;
    public void run()
    {
        reset();
        running = true;
        InitializeWaitingList();
        if (Time.timeScale == 0.0f)
        {
            StartCoroutine(StepCompute());
        }
    }

    IEnumerator StepCompute()
    {
        while (running)
        {
            Step();
            yield return null;
        }
    }

    private void InitializeWaitingList()
    {
        foreach (var _process in scheduler.ProcessList)
        {
            waiting.Add(_process);
        }
    }

    public void reset()
    {
        waiting = new List<PropertiesData>();
        arrived = new List<PropertiesData>();
        CurrentlyProcessing = null;
        ProcessorFreeAt = 0.0f;
        processing = false;
        running = false;
    }
    public void Step()
    {
        if (!running)
        {
            reset();
            InitializeWaitingList();
            running = true;
        }
        if (ProcessorFreeAt > 0)
        {
            scheduler.SchedulerTime += ProcessorFreeAt;
            scheduler.SetTimerText();
            ProcessorFreeAt = 0;
            scheduler.SchedulerDeltaTime = 0.0f;
        }
        else
        {
            if (waiting.Count > 0)
            {
                float SetToTime = waiting[0].ArrivalTime;
                foreach (var prop in waiting)
                {
                    SetToTime = Mathf.Min(prop.ArrivalTime, SetToTime);
                }
                scheduler.SchedulerTime = SetToTime;
                scheduler.SetTimerText();
            }
        }
        process();
    }
    private void process()
    {
        if (running)
        {
            for (int i = 0; i < waiting.Count; i++)
            {
                PropertiesData propertiesData = waiting[i];
                if (scheduler.SchedulerTime >= propertiesData.ArrivalTime)
                {
                    arrived.Add(propertiesData);
                    waiting.Remove(propertiesData);
                }
            }
            if (!processing)
            {
                if (arrived.Count > 0)
                {
                    float minPriority = arrived[0].Priority;
                    CurrentlyProcessing = arrived[0];
                    foreach (PropertiesData processes in arrived)
                    {
                        if (processes.Priority < minPriority)
                        {
                            minPriority = processes.Priority;
                            CurrentlyProcessing = processes;
                        }
                    }
                    arrived.Remove(CurrentlyProcessing);
                    processing = true;
                    ProcessorFreeAt = Mathf.Min(CurrentlyProcessing.BurstTime, 1);
                    CurrentlyProcessing.remainingBurstTime -= ProcessorFreeAt;
                    chartMaker.GenerateChartElement(CurrentlyProcessing, scheduler.SchedulerTime);
                }
            }
            else
            {
                ProcessorFreeAt -= scheduler.SchedulerDeltaTime;
                if (ProcessorFreeAt <= 0)
                {
                    if (CurrentlyProcessing.remainingBurstTime <= 0)
                    {
                        float SpeedAdjustment = -ProcessorFreeAt;
                        scheduler.makeSummary(CurrentlyProcessing, SpeedAdjustment);
                    }
                    else
                    {
                        arrived.Add(CurrentlyProcessing);
                    }
                    processing = false;
                    if (waiting.Count == 0 && arrived.Count == 0)
                    {
                        running = false;
                        scheduler.running = false;
                    }
                }
            }
        }
    }
    void Update()
    {
        process();
    }
}
