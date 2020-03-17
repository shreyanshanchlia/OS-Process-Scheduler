using System;
using System.Collections.Generic;
using UnityEngine;

public class FCFSScheduler : MonoBehaviour
{
    public Scheduler scheduler;
    public ChartMaker chartMaker;
    public SummaryManager summaryManager;
    private Queue<PropertiesData> arrived;
    private List<PropertiesData> waiting;
    bool running = false;
    bool processing = false;
    PropertiesData CurrentlyProcessing;
    private float ProcessorFreeAt = 0.0f;
    public void run()
    {
        waiting = new List<PropertiesData>();
        arrived = new Queue<PropertiesData>();
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
                if (scheduler.SchedulerTime > propertiesData.ArrivalTime)
                {
                    arrived.Enqueue(propertiesData);
                    waiting.Remove(propertiesData);
                }
            }
            if(!processing)
            {
                if (arrived.Count > 0)
                { 
                    CurrentlyProcessing = arrived.Dequeue();
                    processing = true;
                    ProcessorFreeAt = CurrentlyProcessing.BurstTime;
                    chartMaker.GenerateChartElement(CurrentlyProcessing.ProcessName, scheduler.SchedulerTime);
                }
            }
            else
            {
                ProcessorFreeAt -= Time.deltaTime * Time.timeScale;
                if(ProcessorFreeAt <= 0)
                {
                    #region make Summary
                    SummaryData summaryData = new SummaryData();
                    summaryData.ProcessName = CurrentlyProcessing.ProcessName;
                    summaryData.ArrivalTime = CurrentlyProcessing.ArrivalTime;
                    summaryData.BurstTime = CurrentlyProcessing.BurstTime;
                    summaryData.CompilationTime = scheduler.SchedulerTime;
                    summaryData.TurnAroundTime = summaryData.CompilationTime - summaryData.ArrivalTime;
                    summaryData.WaitingTime = summaryData.TurnAroundTime - summaryData.BurstTime;
                    summaryManager.summaryDatas.Add(summaryData);
					#endregion
					processing = false;
                    if(waiting.Count == 0 && arrived.Count == 0)
                    {
                        running = false;
                        scheduler.running = false;
                    }
                }
            }
        }
    }
}
