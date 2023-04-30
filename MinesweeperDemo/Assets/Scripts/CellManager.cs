using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public void ApenAllCells()
    {
        for (int y = 0; y < GameManager.BOUNDARY_Y; y++)
        {
            for (int x = 0; x < GameManager.BOUNDARY_X; x++)
            {
                Cell cell = GameManager.Instance.cells[x,y];
                if(cell.IsBomb)
                {
                    cell.Bomb(cell);
                }
                else
                {
                    cell.RegularCell(cell);
                }
            }
        }
    }
}
