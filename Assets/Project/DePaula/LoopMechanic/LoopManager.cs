using UnityEngine;

public class LoopManager : MonoBehaviour
{
    private void Start()
    {
        EventManager.TriggerEvent("StartRun", null);
    }
}
