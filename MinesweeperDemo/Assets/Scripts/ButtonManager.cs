using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] int _x, _y;
    [SerializeField] bool _isBomb;
    [SerializeField] int _bombCount;
    [SerializeField] bool isChecked;

    public int X { get => _x; set => _x = value; }
    public int Y { get => _y; set => _y = value; }
    public bool IsBomb { get => _isBomb; set => _isBomb = value; }

    private void Start()
    {
        if (!_isBomb)
            FindNeighborBombsCount();
    }

    private void FindNeighborBombsCount()
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
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _bombCount.ToString();
    }

    public void ButtonClicked()
    {
        if (InputReader.MouseClick == 0) //left click
        {
            CheckCell(_x, _y);

            //Debug.Log($"Etkileþim saðlandý x:{X} y:{Y}");

        }
    }

    private void CheckSides(int x, int y)
    {
        CheckCell(x - 1, y);
        CheckCell(x + 1, y);
        CheckCell(x, y - 1);
        CheckCell(x, y + 1);
    }


    private void CheckCell(int x, int y)
    {
        //fix!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        #region Boundary Check
        if (x < 0 || x >= GameManager.BOUNDARY_X || y < 0 || y >= GameManager.BOUNDARY_Y) return;
        #endregion
        if (GameManager.Instance.cells[x, y].IsBomb || GameManager.Instance.cells[x, y].isChecked || GameManager.Instance.cells[x, y]._bombCount > 0) return;

        GameManager.Instance.cells[x, y].isChecked = true;
        
        GameManager.Instance.cells[x, y].GetComponent<Button>().interactable = false;
        Color tempColor = GameManager.Instance.cells[x, y].GetComponent<Image>().color;
        tempColor.a = .5f;
        tempColor.r = 0;
        tempColor.g = 255;
        tempColor.b = 100;
        GameManager.Instance.cells[x, y].GetComponent<Image>().color = tempColor;
        
        GameManager.Instance.cells[x, y].CheckSides(x, y);
    }
}
