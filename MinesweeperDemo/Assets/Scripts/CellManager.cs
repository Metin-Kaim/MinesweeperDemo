using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.OnGameOverEvent += OpenAllCells;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameOverEvent -= OpenAllCells;
    }

    public void OpenAllCells()
    {
        StartCoroutine(OpenAll());
    }

    public IEnumerator OpenAll()
    {
        for (int y = 0; y < GameManager.BOUNDARY_Y; y++)
        {
            for (int x = 0; x < GameManager.BOUNDARY_X; x++)
            {
                yield return new WaitForSeconds(.005f);
                Cell cell = GameManager.Instance.cells[x, y];
                if (cell.IsBomb)
                {
                    cell.Bomb(cell);
                }
                else
                {
                    if (!cell.IsChecked)
                        cell.RegularCell(cell);
                }
            }
        }
    }
}
