using DG.Tweening;
using UnityEngine;

public class ActiveRing : MonoBehaviour
{
    private RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    public void PlayActiveAnimation()
    {
        _rect.DOKill();
        _rect.gameObject.SetActive(true);

        _rect.DOLocalRotate(
                new Vector3(0.0f, 0.0f, -360f),
                2.0f,
                RotateMode.FastBeyond360
            )
            .SetEase(Ease.Linear)
            .SetLoops(-1)
            .SetUpdate(true);
    }

    public void StopActiveAnimation()
    {
        if (_rect == null) _rect = GetComponent<RectTransform>();

        _rect.DOKill();
        _rect.localRotation = Quaternion.identity;
        _rect.gameObject.SetActive(false);
    }

}
