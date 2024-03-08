using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public static CoinPool Instance;

    [SerializeField] private List<GameObject> _coinPool;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetCoin()
    {
        GameObject coin;

        do
        {
            coin = _coinPool.First();
        } while (!coin);

        _coinPool.Remove(coin);
        return coin;
    }

    public void ReturnCoin(GameObject coin)
    {
        if (coin != null)
        {
            _coinPool.Add(coin);
            coin.transform.SetParent(transform, false);
            coin.SetActive(false);
        }
    }
}
