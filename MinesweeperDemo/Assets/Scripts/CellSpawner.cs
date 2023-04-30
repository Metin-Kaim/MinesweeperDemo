using UnityEngine;
using UnityEngine.UI;

public class CellSpawner : MonoBehaviour
{
    [SerializeField] Button _cellPrefab;

    [SerializeField] int _bombAmount;

    public int BombAmount => _bombAmount;

    private void Start()
    {
        Cell buttonManager;

        for (int y = 0; y < GameManager.BOUNDARY_Y; y++)
        {
            for (int x = 0; x < GameManager.BOUNDARY_X; x++)
            {
                buttonManager = Instantiate(_cellPrefab, transform).GetComponent<Cell>();
                GameManager.Instance.cells[x, y] = buttonManager;

                buttonManager.IsBomb = false;
                buttonManager.X = x;
                buttonManager.Y = y;
            }
        }

        for (int i = 0; i < _bombAmount; i++)
        {
            do
            {
                int x = Random.Range(0, GameManager.BOUNDARY_X);
                int y = Random.Range(0, GameManager.BOUNDARY_Y);

                buttonManager = GameManager.Instance.cells[x, y].GetComponent<Cell>();

            } while (buttonManager.IsBomb);

            buttonManager.IsBomb = true;

        }
    }
}
