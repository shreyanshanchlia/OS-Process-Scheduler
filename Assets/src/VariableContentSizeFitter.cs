using UnityEngine;

public class VariableContentSizeFitter : MonoBehaviour
{
    public RectTransform ToHandleRectTransform;
    public RectTransform[] childRectTransform;
    float AdditionalOffset = 0;
    void Start()
    {
        FitSize();
    }

    public void FitSize()
    {
        AdditionalOffset = 0;
        foreach (RectTransform rectTransform in childRectTransform)
        {
            if (rectTransform.gameObject != null && rectTransform.gameObject.activeSelf)
                AdditionalOffset += rectTransform.sizeDelta.y;
        }
        ToHandleRectTransform.sizeDelta = new Vector2(ToHandleRectTransform.sizeDelta.x, AdditionalOffset);
    }
}
