using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] int _x, _y;
    [SerializeField] bool _isBomb;

    public int X { get => _x; set => _x = value; }
    public int Y { get => _y; set => _y = value; }
    public bool IsBomb { get => _isBomb; set => _isBomb = value; }

    public void ButtonClicked()
    {
        if (InputReader.MouseClick == 0) //left click
        {
            //Debug.Log($"Etkileþim saðlandý x:{X} y:{Y}");
            GetComponent<Button>().interactable = false;
            Color tempColor = GetComponent<Image>().color;
            tempColor.a = .5f;
            tempColor.r = 0;
            tempColor.g = 255;
            tempColor.b = 100;
            GetComponent<Image>().color = tempColor;
        }
    }
}
