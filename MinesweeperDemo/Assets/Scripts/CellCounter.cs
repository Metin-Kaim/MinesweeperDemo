using Unity.Collections;
using UnityEngine;

public class CellCounter : MonoBehaviour
{
    [Header("Don't Touch")]
    [SerializeField] int _totalCells;
    [SerializeField] int _regularCells;

    CanvasManager _canvasManager;
    CellSpawner _cellSpawner;
    bool _isWin;

    public int TotalCells => _totalCells;
    public int RegularCells => _regularCells;

    private void Awake()
    {
        _canvasManager = GetComponentInParent<CanvasManager>();
        _cellSpawner = GetComponent<CellSpawner>();
    }

    private void Start()
    {
        _totalCells = GameManager.BOUNDARY_X * GameManager.BOUNDARY_Y;
        _regularCells = _totalCells - _cellSpawner.BombAmount;

        _canvasManager.UpdateAllCounts(TotalCells, RegularCells, _cellSpawner.BombAmount);
    }

    public void DecreaseRegularCells(int amount = 1)
    {
        _regularCells -= amount;
        _canvasManager.UpdateRegularCellsCount(RegularCells);

        if (_regularCells <= 0)
        {
            GameManager.Instance.Win();
        }
    }
}
