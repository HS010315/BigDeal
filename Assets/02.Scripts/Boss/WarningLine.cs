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
        int numberOfPoints = 100;
        lineRenderer.positionCount = numberOfPoints;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i / (float)(numberOfPoints - 1);
            Vector3 pointPosition = Vector3.Lerp(dropPosition, new Vector3(dropPosition.x, 0, dropPosition.z), t) - Vector3.up * Mathf.Lerp(0, 10, t);
            lineRenderer.SetPosition(i, pointPosition);
        }

        yield return new WaitForSeconds(warningDuration);

        lineRenderer.enabled = false;
    }
}