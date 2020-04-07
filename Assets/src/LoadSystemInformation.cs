using System.Collections;
using TMPro;
using UnityEngine;

public class LoadSystemInformation : MonoBehaviour
{
    public TextMeshProUGUI ProcessorInfoText;
    private void Start()
    {
        StartCoroutine(LoadProcessor());
    }
    IEnumerator LoadProcessor()
    {
        yield return new WaitForSeconds(1.0f);
        ProcessorInfoText.text = "Using " + SystemInfo.processorType + ", " + SystemInfo.processorCount + " cores.";
    }
}
