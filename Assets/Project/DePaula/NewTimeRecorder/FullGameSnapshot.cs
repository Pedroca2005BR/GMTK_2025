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
    }
}
