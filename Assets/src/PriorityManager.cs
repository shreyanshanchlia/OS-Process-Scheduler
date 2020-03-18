using UnityEngine;

public class PriorityManager : MonoBehaviour
{
    public int PriorityAt = 3;
    public GameObject ProcessHolder;
    public bool priority = false;
    public void CheckPriority(int val)
    {
        if (val == PriorityAt)
        {
            priority = true;
            return;
        }
        priority = false;
    }
}
