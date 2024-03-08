using UnityEngine;

public class CoinController : MonoBehaviour
{
    private GameObject _coin;

    [SerializeField] private float spawnThreshold = 130f;
    [SerializeField] private float despawnThreshold = -80f;
    private bool _isCollected;

    private void OnEnable()
    {
        _isCollected = false;
    }

    private void Update()
    {
        float currentZPosition = transform.position.z;

        // Check if coin should be spawned..
        if (currentZPosition <= spawnThreshold && currentZPosition > despawnThreshold && !_coin && !_isCollected)
        {
            // Get coin from the pool...
            _coin = CoinPool.Instance.GetCoin();

            _coin.transform.SetParent(transform, false);
            _coin.transform.localPosition = Vector3.zero;
            _coin.SetActive(true);
        }
        // Check if coin should be despawned...
        else if (currentZPosition <= despawnThreshold && _coin != null)
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