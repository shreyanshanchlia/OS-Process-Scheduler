﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FCFSScheduler : MonoBehaviour
{
    public Scheduler scheduler;
    private List<PropertiesData> arrived;
    private List<PropertiesData> waiting;
    bool running = false;
    bool processing = false;
    PropertiesData CurrentlyProcessing;
    private float ProcessorFreeAt = 0.0f;
    public void run()
    {
        waiting = new List<PropertiesData>();
        foreach (var _process in scheduler.ProcessList)
        {
            waiting.Add(_process);
        }
        running = true;
    }
    void Update()
    {
        if (running)
        {
            foreach (PropertiesData propertiesData in waiting)
            {
                if (scheduler.SchedulerTime < propertiesData.ArrivalTime)
                {
                    arrived.Add(propertiesData);
                    waiting.Remove(propertiesData);
                }
            }
            if(!processing)
            {
                if (arrived.Count > 0)
                {
                    CurrentlyProcessing = arrived[0];
                    arrived.RemoveAt(0);
                    processing = true;
                    ProcessorFreeAt = CurrentlyProcessing.BurstTime;
                    print(CurrentlyProcessing);
                }
            }
            else
            {
                ProcessorFreeAt -= Time.deltaTime * Time.timeScale;
                if(ProcessorFreeAt <= 0)
                {
                    processing = false;
                    if(waiting.Count == 0 && arrived.Count == 0)
                    {
                        running = false;
                    }
                }
            }
        }
    }
}