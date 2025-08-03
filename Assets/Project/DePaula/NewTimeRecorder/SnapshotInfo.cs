using UnityEngine;
using System.Collections.Generic;

namespace TimeSnapshot
{
    public class SnapshotInfo
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        //public SnapshotInfo(Vector3 position, Quaternion rotation, Vector3 scale)
        //{
        //    Position = position;
        //    Rotation = rotation;
        //    Scale = scale;
        //}
    }

    public class PhysicsSnapshotInfo : SnapshotInfo
    {
        public Rigidbody2D rb;
    }
}
