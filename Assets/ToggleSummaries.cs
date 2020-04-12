using UnityEngine;
using TMPro;
public class ToggleSummaries : MonoBehaviour
{
    [System.Serializable]
    public struct SummaryHolder
    {
        public GameObject Summary;
        public string ButtonName;
    }
    public SummaryHolder[] Summaries;
    public TextMeshProUGUI SummaryButtonName;
    private int currentSummary = 0;

    public void Toggle()
    {
        for (int i = 0; i < Summaries.Length; i++)
        {
            Summaries[i].Summary.SetActive(false);
        }
        Summaries[(++currentSummary) % Summaries.Length].Summary.SetActive(true);
        SummaryButtonName.text = Summaries[(currentSummary) % Summaries.Length].ButtonName;
    }
}
