using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCFSScheduler : MonoBehaviour
{
    public Scheduler scheduler;
    public ChartMaker chartMaker;
    private Queue<PropertiesData> arrived;
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
        waiting.Sort(delegate (PropertiesData p1, PropertiesData p2)
            {
                return p1.ArrivalTime.CompareTo(p2.ArrivalTime);
            }
        );
    }
    public void reset()
    {
        waiting = new List<PropertiesData>();
        arrived = new Queue<PropertiesData>();
        CurrentlyProcessing = null;
        ProcessorFreeAt = 0.0f;
        processing = false;
        running = false;
    }
    public void Step()
    {
        if (!running)
        {
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
    void process()
    {
        if (running)
        {
            for (int i = 0; i < waiting.Count; i++)
            {
                PropertiesData propertiesData = waiting[i];
                if (scheduler.SchedulerTime >= propertiesData.ArrivalTime)
                {
                    arrived.Enqueue(propertiesData);
                    waiting.Remove(propertiesData);
                }
            }
            if (!processing)
            {
                if (arrived.Count > 0)
                {
                    CurrentlyProcessing = arrived.Dequeue();
                    processing = true;
                    ProcessorFreeAt = CurrentlyProcessing.BurstTime;
                    chartMaker.GenerateChartElement(CurrentlyProcessing, scheduler.SchedulerTime);
                }
            }
            if (processing)
            {
                if (ProcessorFreeAt <= 0)
                {
                    float SpeedAdjustment = -ProcessorFreeAt;
                    scheduler.makeSummary(CurrentlyProcessing, SpeedAdjustment);
                    processing = false;
                    if (waiting.Count == 0 && arrived.Count == 0)
                    {
                        running = false;
                        scheduler.running = false;
                    }
                }
                ProcessorFreeAt -= scheduler.SchedulerDeltaTime;
            }
        }
    }
    void Update()
    {
        process();
    }
}
