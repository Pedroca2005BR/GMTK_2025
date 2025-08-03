using UnityEngine;

namespace TimeSnapshot
{
    public interface IRecordable
    {
        public string GetId();
        public void LoadSnapshot(SnapshotInfo info);
        public SnapshotInfo SaveSnapshot();
    }
}
