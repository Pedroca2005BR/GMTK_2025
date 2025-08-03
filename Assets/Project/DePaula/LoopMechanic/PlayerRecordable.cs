using UnityEngine;
using TimeSnapshot;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LV_Move))]
public class PlayerRecordable : MonoBehaviour, IRecordable
{
    private LV_Move moveComponent;
    private Rigidbody2D rb;

    [SerializeField] float replayIncrementSpeed;
    public float timeToReplay = 0f;
    private bool atTimeMachine = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveComponent = GetComponent<LV_Move>();
    }

    private void Update()
    {
        if (atTimeMachine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                timeToReplay += Time.deltaTime * replayIncrementSpeed;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Replay();
            }
        }
    }

    void Replay()
    {
        EventManager.TriggerEvent("Replay", timeToReplay);
        moveComponent.SetEnabled(false);
        rb.bodyType = RigidbodyType2D.Kinematic;
        timeToReplay = 0f;
        atTimeMachine = false;
    }





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
            Scale = transform.localScale
        };
    }
    void OnEnable()
    {
        EventManager.Subscribe("TimeBackToNormal", PrepareBackToNormal);
        EventManager.Subscribe("AtTimeMachine", OnTimemachine);
        EventManager.Subscribe("Rewind", PrepareToRewind);
        TimeSnapshotManager.instance.Register(this);
    }

    void OnDisable()
    {
        EventManager.Unsubscribe("TimeBackToNormal", PrepareBackToNormal);
        EventManager.Unsubscribe("AtTimeMachine", OnTimemachine);
        EventManager.Unsubscribe("Rewind", PrepareToRewind);
        TimeSnapshotManager.instance.Unregister(this);
    }

    private void PrepareBackToNormal(object parameter)
    {
        moveComponent.SetEnabled(true);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    private void OnTimemachine(object parameter)
    {
        atTimeMachine = true;
        moveComponent.SetEnabled(false);
    }
    private void PrepareToRewind(object parameter)
    {
        moveComponent.SetEnabled(false);
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
