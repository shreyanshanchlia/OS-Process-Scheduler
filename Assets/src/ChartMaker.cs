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
    private List<GameObject> GanttChartDataHolders;
    public TabData tabData;
    private void Start()
    {
        summaryAdded = new List<PropertiesData>();
    }

    public void RefreshSummaryList()
    {
        summaryAdded = new List<PropertiesData>();
        foreach (GameObject @object in GanttChartDataHolders.ToArray())
        {
            Destroy(@object);
        }
        GanttChartDataHolders = new List<GameObject>();
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
            GameObject @object = new GameObject("GanttChartDataHolder");
            try
            {
                summaryAdded.Add(Process);
                @object.transform.parent = this.transform;
                @object.AddComponent<GanttChartData>();
                GanttChartDataHolders.Add(@object);
            }
            catch
            {
                print("Malfunction");
            }
            GanttChartData ganttChartData = @object.GetComponent<GanttChartData>();
            ganttChartData.ProcessName = Process.ProcessName;
            Texture2D texture;
            if (!tabData.GetPreemptive())
            {
                texture = new Texture2D((int)(timestamp + Process.BurstTime), 1);
            }
            else
            {
                texture = new Texture2D((int)timestamp + 1, 1);
            }
            texture.filterMode = FilterMode.Point;
            for (int x = 0; x < (int)timestamp; x++)
            {
                texture.SetPixel(x, 0, Color.clear);
            }
            if (!tabData.GetPreemptive())
            {
                for (int x = (int)(timestamp); x < (int)(timestamp + Process.remainingBurstTime); x++)
                {
                    texture.SetPixel(x, 0, Color.blue);
                }
            }
            else
            {
                texture.SetPixel((int)timestamp, 0, Color.blue);
            }
            texture.Apply();
            ganttChartData.texture = texture;
            Rect rec = new Rect(0, 0, texture.width, 1);
            //float scalingFactor = 2.0f / Mathf.Max((int)Mathf.Log10((int)(timestamp + Process.remainingBurstTime)), 3);
            ganttChartData.ProcessingPos = Sprite.Create(texture, rec, Vector2.zero, 0.01f);
            GanttChartManager.DetailedGanttChart.Add(ganttChartData);
            Process.chartData = ganttChartData;
        }
        else
        {
            //retrieve texture resize and repeat
            Texture2D texture = Process.chartData.texture;
            int textureWidth = texture.width;
            Color32[] textureColorsBackup = texture.GetPixels32(0);
            texture.Resize((int)(timestamp + 1), 1);
            texture.Apply();
            for (int x = textureWidth; x < (int)timestamp; x++)
            {
                texture.SetPixel(x, 0, Color.clear);
            }
            texture.Apply();
            for (int i = 0; i < textureColorsBackup.Length; i++)
            {
                texture.SetPixel(i, 0, textureColorsBackup[i]);
            }
            texture.Apply();
            texture.SetPixel((int)timestamp, 0, Color.blue);
            texture.Apply();
            Rect rec = new Rect(0, 0, texture.width, 1);
            Process.chartData.texture = texture;
            Process.chartData.ProcessingPos = Sprite.Create(texture, rec, Vector2.zero, 0.01f);
        }
    }
}
