using System.Collections.Generic;
using UnityEngine;

public class PriorityScheduler : MonoBehaviour
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
        foreach (var _process in scheduler.ProcessList)
        {
            waiting.Add(_process);
        }
        running = true;
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
    void Update()
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
                    for (int i = 0; i < arrived.Count; i++)
                    {
                        PropertiesData processes = arrived[i];
                        if (processes.Priority < minPriority)
                        {
                            minPriority = arrived[i].Priority;
                            CurrentlyProcessing = arrived[i];
                        }
                    }
                    arrived.Remove(CurrentlyProcessing);
                    processing = true;
                    ProcessorFreeAt = CurrentlyProcessing.BurstTime;
                    chartMaker.GenerateChartElement(CurrentlyProcessing.ProcessName, scheduler.SchedulerTime);
                }
            }
            else
            {
                ProcessorFreeAt -= scheduler.SchedulerDeltaTime;
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
            }
        }
    }
}
