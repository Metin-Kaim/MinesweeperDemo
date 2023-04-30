using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int _x, _y;
    [SerializeField] bool _isBomb;
    [SerializeField] int _bombCount;
    [SerializeField] bool _isChecked;
    [SerializeField] bool _canLeftClick = true;
    [SerializeField] bool _canRightClick = true;
    [SerializeField] CellCounter _cellCounter;
    [SerializeField] Color _currentColor;
    [SerializeField] Sprite _knob;

    public int X { get => _x; set => _x = value; }
    public int Y { get => _y; set => _y = value; }
    public bool IsBomb { get => _isBomb; set => _isBomb = value; }

    private void Start()
    {
        _currentColor = GetComponent<Image>().color;
        _cellCounter = GetComponentInParent<CellCounter>();

        if (!_isBomb)
            FindNeighborsBombsCount();
    }

    private void FindNeighborsBombsCount()
    {
        Cell tempObject;

        for (int y = _y - 1; y <= _y + 1; y++)
        {
            for (int x = _x - 1; x <= _x + 1; x++)
            {
                #region Boundary Check
                if (!IsInside(x, y)) continue;
                #endregion

                tempObject = GameManager.Instance.cells[x, y];
                if (tempObject.IsBomb)
                {
                    _bombCount++;
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.Instance.CanPlay) return;

        if (_canLeftClick && eventData.button == PointerEventData.InputButton.Left)
        {
            _canRightClick = false;

            CheckCell(_x, _y);
        }
        else if (_canRightClick && eventData.button == PointerEventData.InputButton.Right)
        {
            if (_canLeftClick)
            {
                GetComponent<Image>().color = Color.red;
                _canLeftClick = false;
            }
            else
            {
                GetComponent<Image>().color = _currentColor;
                _canLeftClick = true;
            }
        }

    }

    private void CheckAround(int _x, int _y)
    {
        for (int y = _y - 1; y <= _y + 1; y++)
        {
            for (int x = _x - 1; x <= _x + 1; x++)
            {
                #region Boundary Check
                if (x < 0 || x >= GameManager.BOUNDARY_X || y < 0 || y >= GameManager.BOUNDARY_Y) continue;
                #endregion

                if (x == _x && y == _y) continue; //kendine uðrama

                CheckCell(x, y);
            }
        }
    }

    private void CheckCell(int x, int y)
    {
        #region Boundary Check
        if (x < 0 || x >= GameManager.BOUNDARY_X || y < 0 || y >= GameManager.BOUNDARY_Y) return;
        #endregion

        Cell currentCell = GameManager.Instance.cells[x, y];

        if (!currentCell.IsBomb)//gameOver
        {
            RegularCell(currentCell, x, y);
        }
        else
        {
            Bomb(currentCell);
            GameManager.Instance.GameOver();
            return;
        }

    }

    public void Bomb(Cell currentCell)
    {
        currentCell.GetComponent<Image>().color = Color.black;
        currentCell.transform.localScale = currentCell.transform.localScale / 2;
        currentCell.GetComponent<Image>().sprite = _knob;
    }
    private void RegularCell(Cell currentCell, int x, int y)
    {
        if (currentCell._isChecked || !currentCell._canLeftClick) return; //cell is marked as bomb

        _cellCounter.DecreaseRegularCells();

        currentCell._isChecked = true;
        currentCell._canRightClick = false;
        currentCell._canLeftClick = false;

        currentCell.GetComponent<Button>().interactable = false;

        Image currentImage = currentCell.GetComponent<Image>();

        Color tempColor = currentImage.color;
        tempColor.r = 0;
        tempColor.g = 255;
        tempColor.b = 100;
        currentImage.color = tempColor;

        if (currentCell._bombCount > 0)
        {
            currentCell.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentCell._bombCount.ToString();
            return;
        }

        currentCell.CheckAround(x, y);

    }
    public void RegularCell(Cell currentCell)
    {
        if (currentCell._isChecked || !currentCell._canLeftClick) return; //cell is marked as bomb

        _cellCounter.DecreaseRegularCells();

        currentCell._isChecked = true;
        currentCell._canRightClick = false;
        currentCell._canLeftClick = false;

        currentCell.GetComponent<Button>().interactable = false;

        Image currentImage = currentCell.GetComponent<Image>();

        Color tempColor = currentImage.color;
        tempColor.r = 0;
        tempColor.g = 255;
        tempColor.b = 100;
        currentImage.color = tempColor;

        if (currentCell._bombCount > 0)
        {
            currentCell.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentCell._bombCount.ToString();
            return;
        }

    }

    private bool IsInside(int x, int y)
    {
        return !(x < 0 || x >= GameManager.BOUNDARY_X || y < 0 || y >= GameManager.BOUNDARY_Y);
    }
}
