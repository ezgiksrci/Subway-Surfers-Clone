using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler<OnGameSpeedChangedEventArgs> OnGameSpeedChanged;
    public class OnGameSpeedChangedEventArgs : EventArgs
    {
        public int gameSpeed;
    }

    [SerializeField] private PlayerController _player;

    public int GameSpeed { get => _gameSpeed; }
    [SerializeField] private int _gameSpeed;

    public int PlayerHealth { get => _playerHealth; }
    [SerializeField] private int _playerHealth = 3;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _player.OnPlayerGetHurt += Player_OnPlayerGetHurt;
    }

    private void Player_OnPlayerGetHurt()
    {
        _playerHealth--;
    }

    /********** 
    ***********
    *
    Invoke OnGameSpeedChanged when speed is changed...
    *
    ***********
    ***********/
}
