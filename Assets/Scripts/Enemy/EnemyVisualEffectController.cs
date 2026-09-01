using DG.Tweening;
using UnityEngine;

public class EnemyVisualEffectController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private bool _isSlowed;
    private Tween _colorTween;
    private Tween _hitTween;

    private readonly Color _normalColor = Color.white;
    private readonly Color _hitColor = Color.red;
    private readonly Color _slowColor = new Color(0.5f, 0.8f, 1.0f);

    public void Init(SpriteRenderer spriteRenderer)
    {
        _spriteRenderer = spriteRenderer;
    }

    public void SetSlow(bool active)
    {
        _isSlowed = active;

        // 피격 연출 중이라면 상태 색 변경은
        // 피격 연출이 끝난 뒤 적용된다.
        if (_hitTween != null && _hitTween.IsActive())
        {
            return;
        }

        if (active)
        {
            PlaySlow();
        }
        else
        {
            StopSlow();
        }
    }

    public void PlayHitEffect()
    {
        _hitTween?.Kill();

        _colorTween?.Kill();
        _colorTween = null;

        _hitTween = DOTween.Sequence()
            .Append(_spriteRenderer.DOColor(_hitColor, 0.05f))
            .Append(_spriteRenderer.DOColor(GetStatusColor(), 0.05f))
            .OnComplete(() =>
            {
                _hitTween = null;

                if (_isSlowed)
                {
                    PlaySlow();
                }
            });
    }

    private void PlaySlow()
    {
        _colorTween?.Kill();
        _spriteRenderer.color = _slowColor;
        _colorTween = null;
    }

    private void StopSlow()
    {
        _colorTween?.Kill();
        _colorTween = null;
        _spriteRenderer.color = _normalColor;
    }

    private Color GetStatusColor()
    {
        return _isSlowed ? _slowColor : _normalColor;
    }

    private void OnDestroy()
    {
        _colorTween?.Kill();
        _hitTween?.Kill();
    }

}
