using UnityEngine;
using UnityEngine.UI;

public class CellSpawner : MonoBehaviour
{
    [SerializeField] Button _cellPrefab;

    private void Start()
    {
        ButtonManager buttonManager;

        for (int y = 0; y < 15; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                buttonManager = Instantiate(_cellPrefab, transform).GetComponent<ButtonManager>();
                GameManager.Instance.cells[x, y] = buttonManager;

                buttonManager.IsBomb = false;
                buttonManager.X = x;
                buttonManager.Y = y;
            }
        }

        for (int i = 0; i < GameManager.Instance.BombAmount; i++)
        {
            do
            {
                int x = Random.Range(0, GameManager.BOUNDARY_X);
                int y = Random.Range(0, GameManager.BOUNDARY_Y);

                buttonManager = GameManager.Instance.cells[x, y].GetComponent<ButtonManager>();

            } while (buttonManager.IsBomb);

            buttonManager.IsBomb = true;

        }
    }
}
