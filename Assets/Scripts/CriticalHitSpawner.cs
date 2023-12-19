using System.Collections;
using UnityEngine;

public class CriticalHitSpawner : MonoBehaviour
{
    private const float timeToKill = 0.5f;

    private void Start()
    {
        StartCoroutine(nameof(KillCriticalHitObject));
    }

    private IEnumerator KillCriticalHitObject()
    {
        yield return new WaitForSeconds(timeToKill);
        Destroy(this.gameObject);
    }
}
