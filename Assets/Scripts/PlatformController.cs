using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private int _movingSpeed;
    private bool _isGameOver;
    [SerializeField] private List<Transform> _coinList;

    private void OnEnable()
    {
        foreach (var coin in _coinList)
        {
            coin.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        GameManager.Instance.OnGameSpeedChanged += GameManager_OnGameSpeedChanged;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        _movingSpeed = GameManager.Instance.GameSpeed;
        _isGameOver = false;
    }

    private void GameManager_OnGameOver()
    {
        _isGameOver = true;
    }

    private void GameManager_OnGameSpeedChanged(object sender, GameManager.OnGameSpeedChangedEventArgs e)
    {
        _movingSpeed = e.gameSpeed;
    }

    void Update()
    {
        if (!_isGameOver)
        {
            transform.position += new Vector3(0, 0, -_movingSpeed) * Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameSpeedChanged -= GameManager_OnGameSpeedChanged;
    }
}
