using UnityEngine;

public class ChartMaker : MonoBehaviour
{
    public GameObject prefabObject;
    public GameObject ganttChartHolder;

    public void GenerateChartElement(string ProcessName, float timestamp)
    {
        GameObject @object = Instantiate(prefabObject, ganttChartHolder.transform);
        @object.GetComponent<ChartElement>().timeStamp.text = timestamp.ToString();
        @object.GetComponent<ChartElement>().ProcessName.text = ProcessName;
    }
}
