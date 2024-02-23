using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Jumping,
    Leaning
}

public enum PlayerSide
{
    Left,
    Center,
    Right
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSideLocation;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _animationDuration;

    private PlayerSide _playerSide;
    private PlayerState _playerState;
    private bool _canVulnerable;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _canVulnerable = true;
        _playerSide = PlayerSide.Center;
        _playerState = PlayerState.Idle;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_playerSide == PlayerSide.Center)
            {
                transform.position = new Vector3(-_playerSideLocation, transform.position.y, transform.position.z);
                _playerSide = PlayerSide.Left;
            }
            else if (_playerSide == PlayerSide.Right)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
                _playerSide = PlayerSide.Center;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_playerSide == PlayerSide.Center)
            {
                transform.position = new Vector3(_playerSideLocation, transform.position.y, transform.position.z);
                _playerSide = PlayerSide.Right;
            }
            else if (_playerSide == PlayerSide.Left)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
                _playerSide = PlayerSide.Center;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            // The Player is leaning...
            _playerState = PlayerState.Leaning;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            if (_playerState != PlayerState.Jumping)
            {
                // The Player is jumping... 
                _playerState = PlayerState.Jumping;
                _animator.SetTrigger("Jumped");
                GetComponent<Rigidbody>().AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            _playerState = PlayerState.Idle;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") && _canVulnerable)
        {
            // play animation...
            _animator.SetBool("IsFlashing", true);
            _canVulnerable = false;

            // call a function to stop the animation after a given duration...
            Invoke(nameof(StopAnimation), _animationDuration);

            // if _canVulnerable then health -= 1;
        }
    }

    // function to stop the animation after a given duration...
    void StopAnimation()
    {
        _animator.SetBool("IsFlashing", false);
        _canVulnerable = true;
    }
}
