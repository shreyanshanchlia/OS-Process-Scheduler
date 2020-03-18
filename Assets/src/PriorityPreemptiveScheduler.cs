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
    private float ProcessStartedAt = 0.0f;
    public void run()
    {
        waiting = new List<PropertiesData>();
        arrived = new List<PropertiesData>();
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
                    ProcessorFreeAt = Mathf.Min(CurrentlyProcessing.BurstTime, 1);
                    CurrentlyProcessing.remainingBurstTime -= ProcessorFreeAt;
                    ProcessStartedAt = scheduler.SchedulerTime;
                    chartMaker.GenerateChartElement(CurrentlyProcessing.ProcessName, scheduler.SchedulerTime);
                }
            }
            else
            {
                ProcessorFreeAt -= Time.deltaTime * Time.timeScale;
                if (ProcessorFreeAt <= 0)
                {
                    if (CurrentlyProcessing.remainingBurstTime <= 0)
                    {
                        scheduler.makeSummary(CurrentlyProcessing);
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
}
