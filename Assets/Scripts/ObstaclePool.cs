using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    public static ObstaclePool Instance;

    [SerializeField] private List<GameObject> SurmountableObstacles;
    [SerializeField] private List<GameObject> NonSurmountableObstacles;

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
                randomObstacle = SurmountableObstacles[Random.Range(0, SurmountableObstacles.Count)];
                if (randomObstacle != null && !randomObstacle.activeInHierarchy)
                {
                    SurmountableObstacles.Remove(randomObstacle);
                    return randomObstacle;
                }
            }
        }
        else if (obstacleType == ObstacleType.NonSurmountable)
        {
            while (randomObstacle == null || randomObstacle.activeInHierarchy)
            {
                randomObstacle = NonSurmountableObstacles[Random.Range(0, NonSurmountableObstacles.Count)];
                if (randomObstacle != null && !randomObstacle.activeInHierarchy)
                {
                    NonSurmountableObstacles.Remove(randomObstacle);
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
            SurmountableObstacles.Add(obstacle);
            obstacle.SetActive(false);
        }
        else
        {
            NonSurmountableObstacles.Add(obstacle);
            obstacle.SetActive(false);
        }
    }
}
