using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PlayerController _player;
    [SerializeField] private int _score = 0;
    [SerializeField] private float _scoreMultiplier = 1f;

    public event Action OnGameOver;
    public event Action<int> OnHealthChanged;
    public event Action<int, int> OnCoinChanged;
    public event Action<int> OnGameSpeedChanged;

    public int GameSpeed
    {
        get
        {
            return _gameSpeed;
        }
        set
        {
            _gameSpeed = value;
            OnGameSpeedChanged?.Invoke(_gameSpeed);
            _scoreMultiplier *= 1.2f;
            Debug.Log("The game is speed up!");
        }
    }
    [SerializeField] private int _gameSpeed;

    public int PlayerHealth { get => _playerHealth; }
    [SerializeField] private int _playerHealth = 3;

    public int CoinNumber { get => _coinNumber; }
    [SerializeField] private int _coinNumber;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _player.OnPlayerGetHurt += Player_OnPlayerGetHurt;
        _player.OnCoinCollected += Player_OnCoinCollected;
    }

    private void Player_OnCoinCollected()
    {
        _coinNumber++;
        _score = (int)((_coinNumber * _scoreMultiplier) * 10f);
        OnCoinChanged?.Invoke(_coinNumber, _score);
    }

    private void Player_OnPlayerGetHurt()
    {
        _playerHealth--;
        OnHealthChanged?.Invoke(_playerHealth);

        if (_playerHealth <= 0)
        {
            Debug.Log("The Player is dead!");
            OnGameOver?.Invoke();
            return;
        }
    }

    private void OnDestroy()
    {
        _player.OnPlayerGetHurt -= Player_OnPlayerGetHurt;
        _player.OnCoinCollected -= Player_OnCoinCollected;
    }
}
