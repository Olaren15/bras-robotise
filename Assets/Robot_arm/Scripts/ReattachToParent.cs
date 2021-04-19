using System;
using UnityEngine;

namespace Robot_arm.Scripts
{
    public class ReattachToParent : MonoBehaviour
    {
        private GameObject parent;

        private void Start()
        {
            parent = transform.parent.gameObject;
        }

        public void Reattach()
        {
            transform.SetParent(parent.transform);
        }
    }
}