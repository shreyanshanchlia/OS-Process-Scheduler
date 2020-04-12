using UnityEngine;
using UnityUITable;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class ChartMaker : MonoBehaviour
{
    public GameObject prefabObject;
    public GameObject ganttChartHolder;
    public GanttChartSummaryManager GanttChartManager;
    private List<PropertiesData> summaryAdded;
    static int count = 0;
    private void Start()
    {
        RefreshSummaryList();
    }

    private void RefreshSummaryList()
    {
        summaryAdded = new List<PropertiesData>();
    }

    public void GenerateChartElement(PropertiesData Process, float timestamp)
    {
        GameObject @object = Instantiate(prefabObject, ganttChartHolder.transform);
        @object.GetComponent<ChartElement>().timeStamp.text = timestamp.ToString("f2");
        @object.GetComponent<ChartElement>().ProcessName.text = Process.ProcessName;
        summaryChartMaker(Process, timestamp);
    }
    public void summaryChartMaker(PropertiesData Process, float timestamp)
    {
        if (Process.chartData == null)
        {
            summaryAdded.Add(Process);
            GameObject @object = new GameObject("GanttChartDataHolder");
            @object.transform.parent = this.transform;
            @object.AddComponent<GanttChartData>();
            GanttChartData ganttChartData = @object.GetComponent<GanttChartData>();
            ganttChartData.ProcessName = Process.ProcessName;
            Texture2D texture = new Texture2D((int)(timestamp+Process.BurstTime), 1);
            texture.filterMode = FilterMode.Point;
            for (int x = 0; x < (int)timestamp; x++)
            {
                texture.SetPixel(x, 0, Color.clear);
            }
            for (int x = (int)(timestamp); x < (int)(timestamp + Process.remainingBurstTime); x++)
            {
                texture.SetPixel(x, 0, Color.blue);
            }
            texture.Apply();
            ganttChartData.texture = texture;
            Rect rec = new Rect(0, 0, texture.width, 1);
            ganttChartData.ProcessingPos = Sprite.Create(texture, rec, Vector2.zero, 0.02f);
            GanttChartManager.DetailedGanttChart.Add(ganttChartData);
            Process.chartData = ganttChartData;
            count++;
            print(count);
        }
        else
        {
            //retrieve texture resize and repeat
            Texture2D texture = new Texture2D((int)(timestamp + Process.BurstTime), 1);
            texture.filterMode = FilterMode.Point;
            Graphics.CopyTexture(Process.chartData.texture, 0, 0, 0, 0, Process.chartData.texture.width, 1, texture, 0, 0, 0, 0);
            texture.Apply();
            texture.SetPixel((int)(timestamp), 0, Color.blue);
            texture.Apply();
            Process.chartData.texture = texture;
            Rect rec = new Rect(0, 0, texture.width, 1);
            Process.chartData.ProcessingPos = Sprite.Create(texture, rec, Vector2.zero, 0.02f);
        }
    }
}
