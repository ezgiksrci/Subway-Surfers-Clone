using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    [SerializeField] List<GameObject> allPlatforms;
    [SerializeField] List<GameObject> movingPlatforms;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            AddPlatformToMovingList(SelectRandomPlatform());
            RemovePlatformFromMovingList(other.gameObject);
        }
    }

    private void AddPlatformToMovingList(GameObject newPlatform)
    {
        movingPlatforms.Add(newPlatform);
        newPlatform.transform.position = new Vector3(0, 0, 207);
        newPlatform.SetActive(true);
    }
    private void RemovePlatformFromMovingList(GameObject oldPlatform)
    {
        oldPlatform.SetActive(false);
        movingPlatforms.Remove(oldPlatform);
    }

    private GameObject SelectRandomPlatform()
    {
        GameObject newPlatform;

        do
        {
            newPlatform = allPlatforms[UnityEngine.Random.Range(0, allPlatforms.Count)];
        } while (movingPlatforms.Contains(newPlatform));

        return newPlatform;
    }
}
