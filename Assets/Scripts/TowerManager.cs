using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private LayerMask _pathLayer;
    [SerializeField] private LayerMask _towerLayer;

    private TowerData _selectedTowerData;
    private TowerPreview _towerPreview;
    private Camera _mainCamera;
    private bool _isPlacing;

    public static TowerManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        _mainCamera = Camera.main;
    }

    public void SelectTower(TowerData towerData)
    {
        if (towerData == null) return;

        _selectedTowerData = towerData;
        StartPlacement();
    }

    private void StartPlacement()
    {
        CancelPlacement();
        GameObject previewObject = Instantiate(_selectedTowerData.PreviewPrefab);
        _towerPreview = previewObject.GetComponent<TowerPreview>();
        _towerPreview.SetRange(_selectedTowerData.Range);
        _isPlacing = true;
    }

    private void UpdatePreview()
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        _towerPreview.SetPosition(mouseWorldPosition);

        bool canPlace = CanPlace();
        _towerPreview.SetPlaceable(canPlace);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = Mathf.Abs(_mainCamera.transform.position.z);
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0f;
        return worldPosition;
    }

    public void CancelPlacement()
    {
        if (_towerPreview != null)
        {
            Destroy(_towerPreview.gameObject);
            _towerPreview = null;
        }

        _isPlacing = false;
    }

    private bool CanPlace()
    {
        CapsuleCollider2D collider = _towerPreview.PlacementCollider;

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(_pathLayer | _towerLayer);
        filter.useTriggers = true;

        Collider2D[] results = new Collider2D[10];
        int count = collider.Overlap(filter, results);
        
        return count == 0;
    }

    private void TryPlaceTower()
    {
        if (!CanPlace()) return;

        Vector3 position = _towerPreview.transform.position;
        Instantiate(_selectedTowerData.TowerPrefab, position, Quaternion.identity);
        CancelPlacement();
    }

    private void Update()
    {
        if (!_isPlacing) return;

        UpdatePreview();

        if (Input.GetMouseButtonDown(0))
        {
            TryPlaceTower();
        }
    }

}
