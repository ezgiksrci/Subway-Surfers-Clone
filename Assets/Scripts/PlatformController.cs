using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private int _movingSpeed;

    private void Start()
    {
        GameManager.Instance.OnGameSpeedChanged += OnGameSpeedChanged;
        _movingSpeed = GameManager.Instance.GameSpeed;
    }

    private void OnGameSpeedChanged(object sender, GameManager.OnGameSpeedChangedEventArgs e)
    {
        _movingSpeed = e.gameSpeed;
    }

    void Update()
    {
        transform.position += new Vector3(0, 0, -_movingSpeed) * Time.deltaTime;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameSpeedChanged -= OnGameSpeedChanged;
    }
}
