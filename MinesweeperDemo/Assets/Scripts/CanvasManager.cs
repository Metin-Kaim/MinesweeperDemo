using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _totalCellsText;
    [SerializeField] TextMeshProUGUI _regularCellsText;
    [SerializeField] TextMeshProUGUI _bombCellsText;
    [SerializeField] GameObject _winPanel;
    [SerializeField] GameObject _gameOverPanel;

    private void Awake()
    {
        _winPanel.SetActive(false);
        _gameOverPanel.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnWinEvent += WinPanel;
        GameManager.Instance.OnGameOverEvent += GameOverPanel;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnWinEvent -= WinPanel;
        GameManager.Instance.OnGameOverEvent -= GameOverPanel;
    }

    public void UpdateAllCounts(int totalcellCount, int regularCellCount, int bombCount)
    {
        _totalCellsText.text = totalcellCount.ToString();
        UpdateRegularCellsCount(regularCellCount);
        _bombCellsText.text = bombCount.ToString();
    }

    public void UpdateRegularCellsCount(int regularCellCount)
    {
        _regularCellsText.text = regularCellCount.ToString();
    }

    public void LoadLevelScene(int level = 0)
    {
        GameManager.Instance.LoadLevelScene(level);
    }

    private void GameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }
    private void WinPanel()
    {
        _winPanel.SetActive(true);
    }
}
