using UnityEngine;
using System.Collections.Generic;
using System;

namespace TimeSnapshot
{
    public class TimeSnapshotManager : MonoBehaviour
    {
        #region Singleton

        public static TimeSnapshotManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
        #endregion

        public enum State
        {
            Idle = 0,
            Recording = 1,
            Rewinding = 2,
            Replaying = 3
        }

        List<IRecordable> recordables = new List<IRecordable>();
        public SnapshotDataStorage snapshotStorage;

        float timeFrame;
        State state;
        public int snapshotIndex = -1;
        float specialTimer = 0;

        //private void Start()
        //{
        //    recordables = new List<IRecordable>(transform.GetComponents<IRecordable>());
        //    Debug.Log(recordables.Count);
        //}


        private void FixedUpdate()
        {
            //Debugging
            //Debug.Log(snapshotStorage.savedSnapshots.Count);

            switch (state)
            {
                case State.Recording:
                    snapshotIndex++;
                    TakeSnapshot();
                    break;

                case State.Replaying:
                    LoadNextSnapshot();
                    snapshotIndex++;

                    if (specialTimer < 0)
                    {
                        GoIdle();
                        EventManager.TriggerEvent("Record", null);
                    }
                    break;

                case State.Rewinding:
                    LoadNextSnapshot();
                    snapshotIndex--;

                    if (specialTimer < 0)
                    {
                        EventManager.TriggerEvent("AtTimeMachine", null);
                        GoIdle();
                    }
                    break;
            }

            specialTimer -= Time.fixedDeltaTime;
        }

        public void Register(IRecordable recordable)
        {
            recordables.Add(recordable);
        }

        public void Unregister(IRecordable recordable)
        {
            recordables.Remove(recordable);
        }


        public void StartRecording()
        {
            timeFrame = 0;
            state = State.Recording;
        }

        public void Replay(float seconds = 10)
        {
            snapshotIndex = 0;
            state = State.Replaying;
            specialTimer = seconds;

            // Liga o glitch
            //ShaderController.instance.TurnOn();
        }
        
        public void Rewind(float seconds = 10)
        {
            //snapshotIndex = snapshotStorage.savedSnapshots.Count - 1;
            state = State.Rewinding;
            specialTimer = seconds;

            // Liga o glitch
            //ShaderController.instance.TurnOn();
        }

        public void GoIdle()
        {
            state = State.Idle;
            EventManager.TriggerEvent("TimeBackToNormal", null);

            // Desliga o glitch
            //ShaderController.instance.TurnOff();
        }



        private void TakeSnapshot()
        {
            timeFrame += Time.fixedDeltaTime;
            FullGameSnapshot fullGameSnapshot = new FullGameSnapshot(timeFrame);

            for(int i=0; i < recordables.Count; i++) 
            {
                fullGameSnapshot.AddSnapshot(recordables[i].GetId(), recordables[i].SaveSnapshot());
            }

            snapshotStorage.AddSnapshot(fullGameSnapshot, snapshotIndex);
        }

        private void LoadNextSnapshot()
        {
            // Se houver frame para ser carregado, segue em frente
            if (snapshotStorage.GetSnapshot(snapshotIndex, out FullGameSnapshot fullGameSnapshot))
            {
                foreach (IRecordable recordable in recordables)
                {
                    // tenta achar o snapshot de cada objeto na lista
                    if (fullGameSnapshot.GetSnapshot(recordable.GetId(), out SnapshotInfo snapshotInfo))
                    {
                        recordable.LoadSnapshot(snapshotInfo);
                    }
                }
            }
            else
            {
                Debug.LogWarning("Snapshot Storage exhausted! Try to stop replaying/rewinding before this happens!");
                GoIdle();
            }
        }

        private void OnEnable()
        {
            EventManager.Subscribe("Replay", PrepareReplay);
            EventManager.Subscribe("Rewind", PrepareRewind);
            //EventManager.Subscribe("AtTimeMachine", PrepareIdle);
            EventManager.Subscribe("Record", PrepareRecord);
        }

        private void OnDisable()
        {
            EventManager.Unsubscribe("Replay", PrepareReplay);
            EventManager.Unsubscribe("Rewind", PrepareRewind);
            //EventManager.Unsubscribe("AtTimeMachine", PrepareIdle);
            EventManager.Unsubscribe("Record", PrepareRecord);
        }

        void PrepareReplay(object parameter)
        {
            Replay((float) parameter);
        }
        void PrepareRewind(object parameter)
        {
            //Debug.Log(parameter);
            Rewind((float)parameter);
        }
        void PrepareIdle(object parameter)
        {
            GoIdle();
        }
        void PrepareRecord(object parameter)
        {
            StartRecording();
        }
    }
}
