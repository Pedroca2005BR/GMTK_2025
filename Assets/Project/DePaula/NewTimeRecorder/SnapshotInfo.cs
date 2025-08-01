using UnityEngine;
using System.Collections.Generic;

namespace TimeSnapshot
{
    public struct SnapshotInfo
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        public SnapshotInfo(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
    }
}
