using System.Collections;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float delayBeforeDisappearing = 1f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DisappearAfterDelay());
        }
    }

    private IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeDisappearing); 

        Destroy(gameObject);
    }
}