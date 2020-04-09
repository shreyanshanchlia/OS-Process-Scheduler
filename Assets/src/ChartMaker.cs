using UnityEngine;
using UnityUITable;
using UnityEngine.UI;
public class ChartMaker : MonoBehaviour
{
    public GameObject prefabObject;
    public GameObject ganttChartHolder;
    public GanttChartSummaryManager GanttChartManager;
    public void GenerateChartElement(string ProcessName, float timestamp)
    {
        GameObject @object = Instantiate(prefabObject, ganttChartHolder.transform);
        @object.GetComponent<ChartElement>().timeStamp.text = timestamp.ToString("f2");
        @object.GetComponent<ChartElement>().ProcessName.text = ProcessName;

        GanttChartData ganttChartData = new GanttChartData();
        ganttChartData.ProcessName = ProcessName;
        Texture2D texture = new Texture2D((int)10 * 50, 50);    //replace 10 with total time.
        for(int x = 0; x <= (int)(timestamp * 50); x++)
        {
            for (int y = 0; y <= texture.height - 1; y++)
            {
                texture.SetPixel(x, y, Color.white);
            }
        }
        for (int x = (int)timestamp * 50; x <= (int)(timestamp * 50 + 50); x++)
        {
            for (int y = 0; y <= texture.height - 1; y++)
            {
                texture.SetPixel(x, y, Color.blue);
            }
        }
        texture.Apply();
        Rect rec = new Rect(0, 0, texture.width, texture.height);
        ganttChartData.ProcessingPos = Sprite.Create(texture, rec, Vector2.zero);
        
        GanttChartManager.DetailedGanttChart.Add(ganttChartData);
    }
}
