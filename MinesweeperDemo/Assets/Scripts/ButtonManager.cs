using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int _x, _y;
    [SerializeField] bool _isBomb;
    [SerializeField] int _bombCount;
    [SerializeField] bool _isChecked;
    [SerializeField] bool _canLeftClick = true;
    [SerializeField] bool _canRightClick = true;
    [SerializeField] CellCounter _cellCounter;

    public int X { get => _x; set => _x = value; }
    public int Y { get => _y; set => _y = value; }
    public bool IsBomb { get => _isBomb; set => _isBomb = value; }

    private void Start()
    {
        _cellCounter = transform.parent.GetComponent<CellCounter>();

        if (!_isBomb)
            FindNeighborsBombsCount();
    }

    private void FindNeighborsBombsCount()
    {
        ButtonManager tempObject;

        for (int y = _y - 1; y <= _y + 1; y++)
        {
            for (int x = _x - 1; x <= _x + 1; x++)
            {
                #region Boundary Check
                if (x < 0 || x >= GameManager.BOUNDARY_X || y < 0 || y >= GameManager.BOUNDARY_Y) continue;
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
                GetComponent<Image>().color = Color.white;
                _canLeftClick = true;
            }
        }

    }

    private void CheckSides(int _x, int _y)
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

        ButtonManager currentCell = GameManager.Instance.cells[x, y];

        if(currentCell.IsBomb)//gameOver
        {
            print("Game Over");
            currentCell.GetComponent<Image>().color = Color.black;
            return;
        }

        if (currentCell._isChecked) return;

        _cellCounter.DecreaseRegularCells();

        currentCell._isChecked = true;
        currentCell._canRightClick = false;
        currentCell._canLeftClick = false;

        currentCell.GetComponent<Button>().interactable = false;

        Image currentImage = currentCell.GetComponent<Image>();

        Color tempColor = currentImage.color;
        tempColor.a = .5f;
        tempColor.r = 0;
        tempColor.g = 255;
        tempColor.b = 100;
        currentImage.color = tempColor;

        if (currentCell._bombCount > 0)
        {
            currentCell.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentCell._bombCount.ToString();
            return;
        }

        currentCell.CheckSides(x, y);

    }
}
