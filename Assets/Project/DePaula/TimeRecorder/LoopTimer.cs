using UnityEngine;

public class LoopTimer : Pedroca2005BR.Utilities.Stopwatch
{

    private void Start()
    {
        EventManager.TriggerEvent("StartRun", null);
        EventManager.TriggerEvent("Record", null);
    }

    private void OnEnable()
    {
        EventManager.Subscribe("StartRun", StartRun);
        EventManager.Subscribe("Rewind", StartRewind);
        EventManager.Subscribe("Replay", StartReplay);
        EventManager.Subscribe("TimeBackToNormal", SetTimeBackToNormal);
        EventManager.Subscribe("AtTimeMachine", RestartAtTimeMachine);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("StartRun", StartRun);
        EventManager.Unsubscribe("Rewind", StartRewind);
        EventManager.Unsubscribe("Replay", StartReplay);
        EventManager.Unsubscribe("TimeBackToNormal", SetTimeBackToNormal);
        EventManager.Unsubscribe("AtTimeMachine", RestartAtTimeMachine);



    }

    private void StartRun(object parameter)
    {
        SetCountDown();
        ForceContinueTimerUntil(0f);
        SetEvent(ReturnFull);
    }

    private void StartRewind(object parameter)
    {
        float newData = (float)parameter;
        ChangeTimerDirection(_currentTime + newData);
        //SetEvent(RestartAtTimeMachine);

        Time.timeScale = 5f;
    }

    private void StartReplay(object parameter)
    {
        float newData = (float)parameter;
        ForceContinueTimerUntil(_currentTime - newData);
        //SetEvent(SetTimeBackToNormal);

        Time.timeScale = 2f;
    }

    private void RestartAtTimeMachine(object parameter)
    {
        Debug.Log("At Time Machine!");

        //EventManager.TriggerEvent("AtTimeMachine", null);
        ForceStopTimer();
        Time.timeScale = 1f;
        SetTimeBackToNormal(null);
    }

    private void SetTimeBackToNormal(object parameter)
    {
        Debug.Log("BackToNormal,Baby");


        //EventManager.TriggerEvent("TimeBackToNormal", null);
        Time.timeScale = 1f;
        //StartRun(null);
    }

    private void ReturnFull()
    {
        EventManager.TriggerEvent("Rewind", _startTime);
    }
}
