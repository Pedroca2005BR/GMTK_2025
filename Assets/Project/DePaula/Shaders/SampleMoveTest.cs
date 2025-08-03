using System;
using TimeSnapshot;
using UnityEngine;
using UnityEngine.InputSystem;

public class SampleMoveTest : Pedroca2005BR.Platformer_2D.PlayerMovement, TimeSnapshot.IRecordable
{
    string _id;

    public string GetId()
    {
        if (_id == null)
        {
            _id = Guid.NewGuid().ToString();
        }
        
        return _id;        
    }

    public void LoadSnapshot(SnapshotInfo info)
    {
        PhysicsSnapshotInfo _info = info as PhysicsSnapshotInfo;

        transform.position = _info.Position;
        transform.rotation = _info.Rotation;
        transform.localScale = _info.Scale;
        //rb.bodyType = RigidbodyType2D.Kinematic;
        //rb.linearVelocity = _info.rb.linearVelocity;
    }

    public SnapshotInfo SaveSnapshot()
    {
        return new PhysicsSnapshotInfo()
        {
            Position = transform.position,
            Rotation = transform.rotation,
            Scale = transform.localScale,
            rb = rb
        };
    }

    void OnPrevious(InputValue input)
    {
        EventManager.TriggerEvent("Rewind", 10f);
        DisableOrEnableInput();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnNext(InputValue input)
    {
        EventManager.TriggerEvent("Replay", 10f);
        DisableOrEnableInput();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnAttack(InputValue input)
    {
        EventManager.TriggerEvent("StartRun", null);
        EventManager.TriggerEvent("Record", null);
    }

    void OnEnable()
    {
        EventManager.Subscribe("TimeBackToNormal", PrepareBackToNormal);
        TimeSnapshotManager.instance.Register(this);
    }

    void OnDisable()
    {
        EventManager.Unsubscribe("TimeBackToNormal", PrepareBackToNormal);
        TimeSnapshotManager.instance.Unregister(this);
    }

    private void PrepareBackToNormal(object parameter)
    {
        DisableOrEnableInput(true);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
