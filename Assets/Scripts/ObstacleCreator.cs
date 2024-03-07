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
    [SerializeField] private ObstaclePool _obstaclePool;
    [SerializeField] private ObstacleType _obstacleType;

    private GameObject _randomObstacle;

    private void OnEnable()
    {
        if (_randomObstacle == null)
        {
            _randomObstacle = _obstaclePool.GetRandomObstacle(_obstacleType);
            _randomObstacle.transform.SetParent(transform, false);
            _randomObstacle.SetActive(true);
        }
    }

    private void OnDisable()
    {
        ObstaclePool.Instance.AddObstacleToList(_randomObstacle, _obstacleType);
        _randomObstacle = null;
    }
}
