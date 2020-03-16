using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Scheduler : MonoBehaviour
{
    [SerializeField] private TabData tabData;
    public FCFSScheduler fcfsScheduler;
    public float SchedulerTime; 
    public bool running = false;
    public List<PropertiesData> ProcessList;
    public TextMeshProUGUI schedulerTimeText;
    public void RunScheduler()     
    {
        ProcessList = new List<PropertiesData>();
        ProcessList = tabData.propertiesDatas;
        running = true;
        if (tabData.Scheduler == 0)
        {
            fcfsScheduler.run();
        }
    }
    private void Update()
    {
        if (running)
        {
            SchedulerTime += Time.deltaTime * Time.timeScale;
            schedulerTimeText.text = SchedulerTime.ToString("f4");
        }
    }
}
