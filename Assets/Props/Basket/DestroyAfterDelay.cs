using System.Collections;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitFoSecs(3));
    }

    private IEnumerator WaitFoSecs(float sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);
    }
}