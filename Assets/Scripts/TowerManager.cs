using UnityEngine;

public enum TowerManagerState
{
    None,
    Placing,
    Selecting,
}

public class TowerManager : MonoBehaviour
{
    [SerializeField] private LayerMask _pathLayer;
    [SerializeField] private LayerMask _towerLayer;

    private TowerData _selectedTowerData;
    private TowerPreview _towerPreview;
    private Camera _mainCamera;
    private TowerManagerState _state = TowerManagerState.None;

    public static TowerManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
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
        if (_selectedTowerData == null) return;

        DestroyPreview();
        GameObject previewObject = Instantiate(_selectedTowerData.PreviewPrefab);
        _towerPreview = previewObject.GetComponent<TowerPreview>();
        _towerPreview.Init(_selectedTowerData);
        ChangeState(TowerManagerState.Placing); 
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
        Vector3 mousePosition = InputManager.Instance.GamePlay.Point.ReadValue<Vector2>();
        Vector3 screenPosition = new Vector3(mousePosition.x, mousePosition.y, Mathf.Abs(_mainCamera.transform.position.z));
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0.0f;
        return worldPosition;
    }

    public void CancelPlacement()
    {
        if (_state != TowerManagerState.Placing) return;

        ChangeState(TowerManagerState.None);
    }

    private void DestroyPreview()
    {
        if (_towerPreview == null) return;

        Destroy(_towerPreview.gameObject);
        _towerPreview = null;
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
        GameObject tower = Instantiate(_selectedTowerData.TowerPrefab, position, Quaternion.identity);
        tower.GetComponent<TowerController>().Init(_selectedTowerData);
        CancelPlacement();
    }

    private void ChangeState(TowerManagerState newState)
    {
        if (_state == newState) return;

        ExitState(_state);
        _state = newState;
        EnterState(_state);
    }

    private void EnterState(TowerManagerState state)
    {
        switch (state)
        {
            case TowerManagerState.None:
                break;

            case TowerManagerState.Placing:
                break;

            case TowerManagerState.Selecting:
                break;
        }
    }

    private void ExitState(TowerManagerState state)
    {
        switch (state)
        {
            case TowerManagerState.None:
                break;

            case TowerManagerState.Placing:
                DestroyPreview();
                break;

            case TowerManagerState.Selecting:
                break;
        }
    }

    private void Update()
    {
        switch (_state)
        {
            case TowerManagerState.None:
                break;

            case TowerManagerState.Placing:
                UpdatePlacing();
                break;

            case TowerManagerState.Selecting:
                break;
        }
    }

    private void UpdatePlacing()
    {
        UpdatePreview();

        if (InputManager.Instance.GamePlay.Click.WasPressedThisFrame())
        {
            TryPlaceTower();
        }

        if (InputManager.Instance.GamePlay.Cancel.WasPressedThisFrame())
        {
            CancelPlacement();
        }
    }

}
