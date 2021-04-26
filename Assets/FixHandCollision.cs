using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixHandCollision : MonoBehaviour
{
    public void DisableCollider()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("button"), true );
        StartCoroutine(WaitForSec(1));
    }


    private IEnumerator WaitForSec(float sec)
    {
        yield return new WaitForSeconds(sec);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("button"), false );

    }
}
