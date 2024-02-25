using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public enum PlayerState
{
    Running,
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
    public event Action OnPlayerGetHurt;

    [SerializeField] private float _playerSideLocation;
    [SerializeField] private float _sideSlideForce;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _slideDuration;

    private PlayerSide _playerSide;
    private PlayerState _playerState;
    private bool _canVulnerable;
    private bool _isSliding;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _isSliding = false;
        _canVulnerable = true;
        _playerSide = PlayerSide.Center;
        _playerState = PlayerState.Running;
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
        ResetPosition();
    }

    private void HandlePlayerMovement()
    {
        if (!_isSliding)
        {
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _playerState == PlayerState.Running)
            {
                if (_playerSide == PlayerSide.Center)
                {
                    _playerSide = PlayerSide.Left;
                }
                else if (_playerSide == PlayerSide.Right)
                {
                    _playerSide = PlayerSide.Center;
                }
                else if (_playerSide == PlayerSide.Left)
                {
                    return;
                }
                _isSliding = true;
                StartCoroutine(ResetMovement());
                _rigidbody.AddForce(Vector3.left * _sideSlideForce, ForceMode.Impulse);
            }
            else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _playerState == PlayerState.Running)
            {
                if (_playerSide == PlayerSide.Center)
                {
                    _playerSide = PlayerSide.Right;
                }
                else if (_playerSide == PlayerSide.Left)
                {
                    _playerSide = PlayerSide.Center;
                }
                else if (_playerSide == PlayerSide.Right)
                {
                    return;
                }
                _isSliding = true;
                StartCoroutine(ResetMovement());
                _rigidbody.AddForce(Vector3.right * _sideSlideForce, ForceMode.Impulse);
            }
            else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && _playerState == PlayerState.Running)
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
                    _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                }
            }
        }
    }

    private IEnumerator ResetMovement()
    {
        yield return new WaitForSeconds(_slideDuration); // wait some secs for make sure sliding end....

        _isSliding = false;
    }

    private void ResetPosition()
    {
        if (!_isSliding)
        {
            switch (_playerSide)
            {
                case PlayerSide.Left:
                    transform.position = new Vector3(-_playerSideLocation, transform.position.y, transform.position.z);
                    break;
                case PlayerSide.Center:
                    transform.position = new Vector3(0, transform.position.y, transform.position.z);
                    break;
                case PlayerSide.Right:
                    transform.position = new Vector3(_playerSideLocation, transform.position.y, transform.position.z);
                    break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            _playerState = PlayerState.Running;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") && _canVulnerable)
        {
            OnPlayerGetHurt?.Invoke();

            // play animation...
            _animator.SetBool("IsFlashing", true);
            _canVulnerable = false;

            // call a function to stop the animation after a given duration...
            Invoke(nameof(StopAnimation), _animationDuration);
        }
    }

    // function to stop the animation after a given duration...
    private void StopAnimation()
    {
        _animator.SetBool("IsFlashing", false);
        _canVulnerable = true;
    }
}

