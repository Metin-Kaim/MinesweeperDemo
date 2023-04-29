using UnityEngine;
using UnityEngine.UI;

public class CellSpawner : MonoBehaviour
{
    [SerializeField] Button _cellPrefab;

    private void Awake()
    {
        Button currentCell;

        for (int y = 0; y < 15; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                currentCell = Instantiate(_cellPrefab, transform);
                GameManager.Instance.cells[x, y] = currentCell;
                currentCell.GetComponent<ButtonManager>().X = x;
                currentCell.GetComponent<ButtonManager>().Y = y;
            }
        }
    }
}
