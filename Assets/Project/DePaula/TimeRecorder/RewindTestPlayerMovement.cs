using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RewindTestPlayerMovement : Pedroca2005BR.Platformer_2D.PlayerMovement, IRecordable
{
    public List<Record> records { get; set;}
    public RecordState state { get; set;}

    //Rigidbody2D rb;

    //public float moveSpeed;

    int aux;

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        records = new List<Record>();
        Register();
    }

    private void OnDisable()
    {
        Unregister();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TimeRecordManager.StartRecording();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            TimeRecordManager.StartReplaying();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            TimeRecordManager.StartRewinding();
        }
    }

    public void FixedUpdate()
    {
        if (CheckForGround())
        {
            lastGroundedTime = Time.time;
        }

        switch (state)
        {
            case RecordState.Idle:
                Move();
                break;

            case RecordState.Rewinding:
                LoadRecording(records[aux]);
                if (aux <= 0)
                {
                    StopRewinding();
                }
                aux--;
                break;

            case RecordState.Recording:
                Move();
                records.Add(new Record(transform.position, transform.rotation));
                break;

            case RecordState.Replaying:
                LoadRecording(records[aux]);
                aux++;
                if (aux >= records.Count)
                {
                    StopReplaying();
                }
                
                break;

        }
    }

    //private void Move()
    //{
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        rb.linearVelocity = new Vector2(moveSpeed * Time.deltaTime, rb.linearVelocityY); 
    //    }
    //    else
    //    {
    //        rb.linearVelocity = new Vector2(0f, rb.linearVelocityY);
    //    }

    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        rb.linearVelocity = new Vector2(rb.linearVelocityX, moveSpeed * Time.deltaTime);
    //    }
    //    else
    //    {
    //        rb.linearVelocity = new Vector2(rb.linearVelocityX, 0f);
    //    }

    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        TimeRecordManager.StartRecording();
    //    }
    //    else if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        TimeRecordManager.StartReplaying();
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        TimeRecordManager.StartRewinding();
    //    }
    //}



    public void StartRecording()
    {
        state = RecordState.Recording;
    }

    public void StartReplaying()
    {
        state = RecordState.Replaying;
        aux = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void StartRewinding()
    {
        state = RecordState.Rewinding;
        aux = records.Count-1;
        rb.bodyType = RigidbodyType2D.Kinematic;

    }

    public void StopRecording()
    {
        state = RecordState.Idle;
    }

    public void StopReplaying()
    {
        state = RecordState.Idle;
        rb.bodyType = RigidbodyType2D.Dynamic;

    }

    public void StopRewinding()
    {
        state = RecordState.Idle;
        rb.bodyType = RigidbodyType2D.Dynamic;

    }

    public void Register()
    {
        TimeRecordManager.Register(this);
    }

    public void Unregister()
    {
        TimeRecordManager.Unregister(this);
    }

    public void LoadRecording(Record record)
    {
        transform.position = record.position;
        transform.rotation = record.rotation;
    }
}
