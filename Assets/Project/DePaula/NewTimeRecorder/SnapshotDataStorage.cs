using UnityEngine;
using System.Collections.Generic;

namespace TimeSnapshot
{
    [CreateAssetMenu(fileName = "SnapshotData", menuName = "Scriptable Objects/SnapshotDataStorage")]
    public class SnapshotDataStorage : ScriptableObject
    {
        public List<FullGameSnapshot> savedSnapshots = new List<FullGameSnapshot>();

        public void AddSnapshot(FullGameSnapshot snapshot, int index)
        {
            if (index > savedSnapshots.Count)
            {
                savedSnapshots.Add(snapshot);
            }
            else
            {
                savedSnapshots.Insert(index, snapshot);
            }
        }

        public bool GetSnapshot(int index,  out FullGameSnapshot snapshot)
        {
            if (index >= savedSnapshots.Count || index < 0)
            {
                snapshot = new FullGameSnapshot(-1);
                return false;
            }

            snapshot = savedSnapshots[index];
            return true;
        }
    }
}
