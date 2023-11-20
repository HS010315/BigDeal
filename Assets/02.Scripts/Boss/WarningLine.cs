using UnityEngine;
using System.Collections;

public class WarningLine : MonoBehaviour
{
    public float warningDuration = 1f;
    public LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    public void ShowWarningBeforeDrop(Vector3 dropPosition)
    {
        StartCoroutine(ShowWarningLine(dropPosition));
    }

    IEnumerator ShowWarningLine(Vector3 dropPosition)
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, dropPosition);
        lineRenderer.SetPosition(1, new Vector3(dropPosition.x, 0, dropPosition.z));

        yield return new WaitForSeconds(warningDuration);

        lineRenderer.enabled = false;
    }
}