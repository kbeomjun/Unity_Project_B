using UnityEngine;
using UnityEngine.Splines;

public class SplineMover : MonoBehaviour
{
    [SerializeField] private SplineContainer spline;
    [SerializeField] private float speed = 0.2f;

    private float progress = 0f;

    private void Update()
    {
        progress += speed * Time.deltaTime;
        progress = Mathf.Clamp01(progress);
        transform.position = spline.EvaluatePosition(progress);
    }

}
