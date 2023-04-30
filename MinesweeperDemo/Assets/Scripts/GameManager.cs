using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public const int BOUNDARY_X = 13, BOUNDARY_Y = 21;

    public static GameManager Instance;

    public event System.Action OnWinEvent;
    public event System.Action OnGameOverEvent;

    [SerializeField] bool _canPlay = true;

    public Cell[,] cells = new Cell[BOUNDARY_X, BOUNDARY_Y];

    public bool CanPlay { get => _canPlay; private set => _canPlay = value; } 

    private void OnEnable()
    {
        OnWinEvent += CanPlayy;
        OnGameOverEvent += CanPlayy;
    }
    private void OnDisable()
    {
        OnWinEvent -= CanPlayy;
        OnGameOverEvent -= CanPlayy;
    }

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

    public void Win()
    {
        OnWinEvent?.Invoke();
    }
    public void GameOver()
    {
        OnGameOverEvent?.Invoke();
    }

    public void LoadLevelScene(int level = 0)
    {
        StartCoroutine(LoadSceneAsync(level));
    }

    public void CanPlayy()
    {
        _canPlay = false;
    }

    IEnumerator LoadSceneAsync(int level)
    {
        yield return null;
        _canPlay = true;

        SceneManager.LoadSceneAsync(level);

    }
}
