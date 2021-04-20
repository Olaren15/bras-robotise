using UnityEngine;

namespace Robot_arm.Scripts
{
    public class KeyPoint
    {
        public Vector3 targetPosition { get;}
        public Vector3 targetRotationPosition { get;}

        public KeyPoint(Vector3 targetPosition, Vector3 targetRotationPosition)
        {
            this.targetPosition = targetPosition;
            this.targetRotationPosition = targetRotationPosition;
        }

    }
}