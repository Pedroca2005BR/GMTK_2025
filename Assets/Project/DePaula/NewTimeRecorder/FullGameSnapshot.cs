using UnityEngine;
using System.Collections.Generic;

namespace TimeSnapshot
{
    public struct FullGameSnapshot
    {
        float frameTime;
        Dictionary<string, SnapshotInfo> snapshots;

        public FullGameSnapshot(float frame)
        {
            this.frameTime = frame;
            snapshots = new Dictionary<string, SnapshotInfo>();
        }

        public void AddSnapshot(string key, SnapshotInfo value)
        {
            snapshots[key] = value;
        }

        public bool GetSnapshot(string key, out SnapshotInfo value)
        {
            return snapshots.TryGetValue(key, out value);
        }
    }
}
