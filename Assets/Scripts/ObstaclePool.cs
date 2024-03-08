using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    public static ObstaclePool Instance;

    [SerializeField] private List<GameObject> _surmountableObstacles;
    [SerializeField] private List<GameObject> _nonSurmountableObstacles;

    [SerializeField] private GameObject _surmountableObstaclesParent;
    [SerializeField] private GameObject _nonSurmountableObstaclesParent;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetRandomObstacle(ObstacleType obstacleType)
    {
        GameObject randomObstacle = null;

        if (obstacleType == ObstacleType.Surmountable)
        {
            while (randomObstacle == null || randomObstacle.activeInHierarchy)
            {
                randomObstacle = _surmountableObstacles[Random.Range(0, _surmountableObstacles.Count)];
                if (randomObstacle != null && !randomObstacle.activeInHierarchy)
                {
                    _surmountableObstacles.Remove(randomObstacle);
                    return randomObstacle;
                }
            }
        }
        else if (obstacleType == ObstacleType.NonSurmountable)
        {
            while (randomObstacle == null || randomObstacle.activeInHierarchy)
            {
                randomObstacle = _nonSurmountableObstacles[Random.Range(0, _nonSurmountableObstacles.Count)];
                if (randomObstacle != null && !randomObstacle.activeInHierarchy)
                {
                    _nonSurmountableObstacles.Remove(randomObstacle);
                    return randomObstacle;
                }
            }
        }
        return null;
    }

    public void AddObstacleToList(GameObject obstacle, ObstacleType obstacleType)
    {
        if (obstacleType == ObstacleType.Surmountable)
        {
            _surmountableObstacles.Add(obstacle);
            obstacle.SetActive(false);
        }
        else
        {
            _nonSurmountableObstacles.Add(obstacle);
            obstacle.SetActive(false);
        }
    }
}
