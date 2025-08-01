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

        List<IRecordable> recordables = new List<IRecordable>();
        public SnapshotDataStorage snapshotStorage;

        float timeFrame;



        private void FixedUpdate()
        {
            TakeSnapshot();
        }


        public void StartRecording()
        {
            timeFrame = 0;
        }

        public void Replay(float seconds)
        {

        }
        
        public void Rewind(float seconds)
        {

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
    }
}
