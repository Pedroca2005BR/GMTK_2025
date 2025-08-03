using UnityEngine;
using System.Collections.Generic;

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
        int snapshotIndex;
        float specialTimer = 0;


        private void FixedUpdate()
        {
            switch (state)
            {
                case State.Recording:
                    TakeSnapshot(); 
                    break;

                case State.Replaying:
                    LoadNextSnapshot();
                    snapshotIndex++;

                    if (specialTimer < 0)
                    {
                        GoIdle();
                    }
                    break;

                case State.Rewinding:
                    LoadNextSnapshot();
                    snapshotIndex--;

                    if (specialTimer < 0)
                    {
                        GoIdle();
                    }
                    break;
            }

            specialTimer -= Time.fixedDeltaTime;
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
            ShaderController.instance.TurnOn();
        }
        
        public void Rewind(float seconds = 10)
        {
            snapshotIndex = snapshotStorage.savedSnapshots.Count - 1;
            state = State.Rewinding;
            specialTimer = seconds;

            // Liga o glitch
            ShaderController.instance.TurnOn();
        }

        public void GoIdle()
        {
            state = State.Idle;

            // Desliga o glitch
            ShaderController.instance.TurnOff();
        }



        private void TakeSnapshot()
        {
            timeFrame += Time.fixedDeltaTime;
            FullGameSnapshot fullGameSnapshot = new FullGameSnapshot(timeFrame);

            for(int i=0; i < recordables.Count; i++) 
            {
                fullGameSnapshot.AddSnapshot(recordables[i].GetId(), recordables[i].SaveSnapshot());
            }

            snapshotStorage.AddSnapshot(fullGameSnapshot);
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
    }
}
