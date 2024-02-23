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

    public int GameSpeed { get => _gameSpeed; }
    [SerializeField] private int _gameSpeed;

    private void Awake()
    {
        Instance = this;
    }

    /********** 
    ***********
    *
    Invoke OnGameSpeedChanged when speed is changed...
    *
    ***********
    ***********/
}
