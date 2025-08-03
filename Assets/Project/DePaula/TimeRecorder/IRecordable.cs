using UnityEngine;
using System.Collections.Generic;

public interface IRecordable
{
    public RecordState state { get; set; }
    public List<Record> records { get; set; }
    public void Register();
    public void Unregister();
    public void StartRecording();
    public void StopRecording();
    public void StartRewinding();
    public void StopRewinding();
    public void StartReplaying();
    public void StopReplaying();
    public void LoadRecording(Record record);
}

public class Record
{
    public Vector3 position;
    public Quaternion rotation;
    public bool bonusState;

    public Record(Vector3 position, Quaternion rotation, bool bonusState = false)
    {
        this.position = position;
        this.rotation = rotation;
        this.bonusState = bonusState;
    }
}

public enum RecordState
{
    Idle = 0,
    Recording = 1,
    Replaying = 2, 
    Rewinding = 3
}