using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Surmountable,
    NonSurmountable
}

public class ObstacleCreator : MonoBehaviour
{
    [SerializeField] private List<Transform> _obstaclePool;

    private void OnEnable()
    {
        DisableAllObstacles();
        EnableRandomObstacle();
    }

    private void DisableAllObstacles()
    {
        foreach (var obstacle in _obstaclePool)
        {
            if (obstacle.gameObject.activeSelf)
            {
                obstacle.gameObject.SetActive(false);
            }
        }
    }

    private void EnableRandomObstacle()
    {
        Transform randomObstacle = _obstaclePool[UnityEngine.Random.Range(0, _obstaclePool.Count)];
        randomObstacle.gameObject.SetActive(true);
    }
}
