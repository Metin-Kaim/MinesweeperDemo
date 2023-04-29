using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCounter : MonoBehaviour
{
    [SerializeField] int _totalCells;
    [SerializeField] int _regularCells;

    public int TotalCells => _totalCells;
    public int RegularCells => _regularCells;

    private void Start()
    {
        _totalCells = GameManager.BOUNDARY_X * GameManager.BOUNDARY_Y;
        _regularCells = _totalCells - GameManager.Instance.BombAmount;
    }

    private void Update()
    {
        if (_regularCells <= 0)
        {
            print("WIN");
        }
    }

    public void DecreaseRegularCells(int amount = 1)
    {
        _regularCells -= amount;
    }
}
