using UnityEngine;
using UnityUITable;
using UnityEngine.UI;
public class ChartMaker : MonoBehaviour
{
    public GameObject prefabObject;
    public GameObject ganttChartHolder;
    public GanttChartSummaryManager GanttChartManager;
    public void GenerateChartElement(string ProcessName, float timestamp, int burstTime = 0)
    {
        GameObject @object = Instantiate(prefabObject, ganttChartHolder.transform);
        @object.GetComponent<ChartElement>().timeStamp.text = timestamp.ToString("f2");
        @object.GetComponent<ChartElement>().ProcessName.text = ProcessName;
        summaryChartMaker(ProcessName, timestamp, burstTime);
    }
    public void summaryChartMaker(string ProcessName, float startTime, int BurstTime = 0)
    {
        GanttChartData ganttChartData = new GanttChartData();
        ganttChartData.ProcessName = ProcessName;
        Texture2D texture = new Texture2D((int)10, 1);    //replace 10 with total time.
        texture.filterMode = FilterMode.Point;
        for (int x = 0; x <= (int)(startTime); x++)
        {
            texture.SetPixel(x, 0, Color.white);
        }
        for (int x = (int)startTime; x < (int)(startTime + BurstTime); x++)
        {
            texture.SetPixel(x, 0, Color.blue);            
        }
        texture.Apply();
        Rect rec = new Rect(0, 0, texture.width, 1);
        ganttChartData.ProcessingPos = Sprite.Create(texture, rec, Vector2.zero, 0.02f);
        GanttChartManager.DetailedGanttChart.Add(ganttChartData);
    }
}
