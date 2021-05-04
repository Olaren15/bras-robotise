using System;
using UnityEngine;

namespace Robot_arm.Scripts
{
    [Serializable]
    public class KeyPoint
    {
        // target position
        private float t1x, t1y, t1z;

        // target rotation position
        private float t2x, t2y, t2z;

        public bool toolActivated;

        public Vector3 TargetPosition => new Vector3(t1x, t1y, t1z);
        public Vector3 TargetRotationPosition => new Vector3(t2x, t2y, t2z);

        public KeyPoint(Vector3 targetPosition, Vector3 targetRotationPosition, bool toolActivated)
        {
            t1x = targetPosition.x;
            t1y = targetPosition.y;
            t1z = targetPosition.z;

            t2x = targetRotationPosition.x;
            t2y = targetRotationPosition.y;
            t2z = targetRotationPosition.z;

            this.toolActivated = toolActivated;
        }
    }
}