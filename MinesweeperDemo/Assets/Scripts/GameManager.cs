using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public const int BOUNDARY_X = 9, BOUNDARY_Y = 15;

    public static GameManager Instance;

    public ButtonManager[,] cells = new ButtonManager[BOUNDARY_X, BOUNDARY_Y];

    [SerializeField] int _bombAmount;

    public int BombAmount => _bombAmount;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
