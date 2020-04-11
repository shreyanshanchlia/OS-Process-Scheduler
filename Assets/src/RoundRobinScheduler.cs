using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRobinScheduler : MonoBehaviour
{
    public Scheduler scheduler;
    public ChartMaker chartMaker;
    private Queue<PropertiesData> Ready;
    private List<PropertiesData> waiting;
    public float Tq = 1;
    bool running = false;
    bool processing = false;
    PropertiesData CurrentlyProcessing;
    private float ProcessorFreeAt = 0.0f;
    private float ProcessStartedAt = 0.0f;
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
        Ready = new Queue<PropertiesData>();
        CurrentlyProcessing = null;
        ProcessorFreeAt = 0.0f;
        processing = false;
        running = false;
        ProcessStartedAt = 0.0f;
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
                foreach (PropertiesData prop in waiting)
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
                    Ready.Enqueue(propertiesData);
                    waiting.Remove(propertiesData);
                }
            }
            if (!processing)
            {
                if (Ready.Count > 0)
                {
                    CurrentlyProcessing = Ready.Peek();
                    Ready.Dequeue();
                    processing = true;
                    ProcessorFreeAt = Mathf.Min(CurrentlyProcessing.BurstTime, Tq);
                    CurrentlyProcessing.remainingBurstTime -= ProcessorFreeAt;
                    ProcessStartedAt = scheduler.SchedulerTime;
                    chartMaker.GenerateChartElement(CurrentlyProcessing, scheduler.SchedulerTime);
                }
            }
            else
            {
                ProcessorFreeAt -= scheduler.SchedulerDeltaTime;
                if (ProcessorFreeAt <= 0)
                {
                    if (CurrentlyProcessing.remainingBurstTime > 0)
                    {
                        Ready.Enqueue(CurrentlyProcessing);
                    }
                    else
                    {
                        float SpeedAdjustment = -ProcessorFreeAt;
                        scheduler.makeSummary(CurrentlyProcessing, SpeedAdjustment);
                    }
                    processing = false;
                    if (Ready.Count == 0 && waiting.Count == 0)
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
