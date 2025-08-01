using UnityEngine;
using System.Collections.Generic;

namespace TimeSnapshot
{
    [CreateAssetMenu(fileName = "SnapshotData", menuName = "Scriptable Objects/SnapshotDataStorage")]
    public class SnapshotDataStorage : ScriptableObject
    {
        public List<FullGameSnapshot> savedSnapshots = new List<FullGameSnapshot>();

        public void AddSnapshot(FullGameSnapshot snapshot)
        {
            savedSnapshots.Add(snapshot);
        }

        
    }
}
