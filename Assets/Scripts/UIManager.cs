using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private GameObject _tryAgainUI;

    private void Start()
    {
        GameManager.Instance.OnCoinChanged += GameManager_OnCoinChanged;
        GameManager.Instance.OnHealthChanged += GameManager_OnHealthChanged;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }

    private void GameManager_OnGameOver()
    {
        _tryAgainUI.SetActive(true);
    }

    private void GameManager_OnHealthChanged(int health)
    {
        _healthText.text = health.ToString();
    }

    private void GameManager_OnCoinChanged(int coin)
    {
        _coinText.text = coin.ToString();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCoinChanged -= GameManager_OnCoinChanged;
        GameManager.Instance.OnHealthChanged -= GameManager_OnHealthChanged;
    }

    public void TryAgain()
    {
        Time.timeScale = 1;
        _tryAgainUI.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
