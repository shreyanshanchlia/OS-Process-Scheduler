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
    public void run()
    {
        reset();
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
    }
    public void Step()
    {
        if (ProcessorFreeAt > 0)
        {
            scheduler.SchedulerTime += ProcessorFreeAt;
            ProcessorFreeAt = 0;
            process();
        }
        else
        {

        }
    }
    public void process()
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
        if(processing)
        {
            ProcessorFreeAt -= scheduler.SchedulerDeltaTime;
            if (ProcessorFreeAt <= 0)
            {
                float SpeedAdjustment = -ProcessorFreeAt;
                scheduler.makeSummary(CurrentlyProcessing, SpeedAdjustment);
                processing = false;
                if (waiting.Count == 0 && arrived.Count == 0)
                {
                    scheduler.running = false;
                }
            }
        }
    }
    void Update()
    {
        process();
    }
}
