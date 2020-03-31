﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SJFScheduler : MonoBehaviour
{
    public Scheduler scheduler;
    public ChartMaker chartMaker;
    private List<PropertiesData> arrived;
    private List<PropertiesData> waiting;
    bool processing = false;
    PropertiesData CurrentlyProcessing;
    private float ProcessorFreeAt = 0.0f;
    bool running = false;
    public void run()
    {
        reset();
        running = true;
        InitializeWaitingList();
        if(Time.timeScale == 0.0f)
        {
            StartCoroutine(StepCompute());
        }
    }

    IEnumerator StepCompute()
    {
        while(running)
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
        running = false;
        CurrentlyProcessing = null;
        ProcessorFreeAt = 0.0f;
        processing = false;
    }
    public void Step()
    {
        if(!running)
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
    public void process()
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
                    float minBurst = arrived[0].BurstTime;
                    CurrentlyProcessing = arrived[0];
                    for (int i = 0; i < arrived.Count; i++)
                    {
                        PropertiesData processes = arrived[i];
                        if (processes.BurstTime < minBurst)
                        {
                            minBurst = arrived[i].BurstTime;
                            CurrentlyProcessing = arrived[i];
                        }
                    }
                    arrived.Remove(CurrentlyProcessing);
                    processing = true;
                    ProcessorFreeAt = CurrentlyProcessing.BurstTime;
                    chartMaker.GenerateChartElement(CurrentlyProcessing.ProcessName, scheduler.SchedulerTime);
                }
            }
            if (processing)
            {
                if (ProcessorFreeAt <= 0)
                {
                    scheduler.makeSummary(CurrentlyProcessing);
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
