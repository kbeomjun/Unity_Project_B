using UnityEngine;

public class TowerPreview : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _rangeCircle;
    [SerializeField] private CapsuleCollider2D _placementCollider;

    public CapsuleCollider2D PlacementCollider => _placementCollider;

    public void Init(TowerData data)
    {
        _rangeCircle.localScale = Vector3.one * data.Range;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetPlaceable(bool canPlace)
    {
        _spriteRenderer.color = canPlace ? Color.white : Color.red;
    }

}
