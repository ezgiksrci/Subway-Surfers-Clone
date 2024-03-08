using UnityEngine;

public class CoinController : MonoBehaviour
{
    private GameObject _coin;

    [SerializeField] private float _spawnThreshold = 120f;
    [SerializeField] private float _despawnThreshold = -20f;
    private bool _isCollected;

    private void OnEnable()
    {
        _isCollected = false;
    }

    private void Update()
    {
        float currentZPosition = transform.position.z;

        // Check if coin should be spawned..
        if (currentZPosition <= _spawnThreshold && currentZPosition > _despawnThreshold && !_coin && !_isCollected)
        {
            // Get coin from the pool...
            _coin = CoinPool.Instance.GetCoin();

            _coin.transform.SetParent(transform, false);
            _coin.transform.localPosition = Vector3.zero;
            _coin.SetActive(true);
        }
        // Check if coin should be despawned...
        else if (currentZPosition <= _despawnThreshold && _coin != null)
        {
            // Return coin to the pool...
            CoinPool.Instance.ReturnCoin(_coin);
            _coin = null;
        }
    }

    public void ResetCoin()
    {
        if (_coin != null)
        {
            CoinPool.Instance.ReturnCoin(_coin);
            _isCollected = true;
            _coin = null;
        }
    }
}