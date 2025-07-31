using UnityEngine;
using System.Collections.Generic;

public static class TimeRecordManager
{
    public static List<IRecordable> recordables = new List<IRecordable>();

    public static void Register(IRecordable recordable)
    {
        recordables.Add(recordable);
    }

    public static void Unregister(IRecordable recordable)
    {
        recordables.Remove(recordable);
    }



    public static void StartRecording()
    {
        Debug.Log("Recording...");
        for (int i = 0; i < recordables.Count; i++)
        {
            recordables[i].StartRecording();
        }
    }

    public static void StopRecording()
    {
        for(int i = 0;i < recordables.Count;i++)
        {
            recordables[i].StopRecording();
        }
    }

    public static void StartRewinding()
    {
        Debug.Log("Rewinding...");
        for (int i = 0; i < recordables.Count; i++)
        {
            recordables[i].StartRewinding();
        }
    }

    public static void StopRewinding()
    {
        for (int i = 0; i < recordables.Count; i++)
        {
            recordables[i].StopRewinding();
        }
    }

    public static void StartReplaying()
    {
        Debug.Log("Replaying...");
        for (int i = 0; i < recordables.Count; i++)
        {
            recordables[i].StartReplaying();
        }
    }

    public static void StopReplaying()
    {
        for (int i = 0; i < recordables.Count; i++)
        {
            recordables[i].StopReplaying();
        }
    }
}
