using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class PlatformCreator : MonoBehaviour
{
    [SerializeField] private List<PlatformController> _allPlatforms;
    [SerializeField] private List<PlatformController> _movingPlatforms;
    [SerializeField] private int _passedPlatformNumber;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            RemovePlatformFromMovingList(other.gameObject.GetComponent<PlatformController>());
            AddPlatformToMovingList(SelectRandomPlatform());
            _passedPlatformNumber++;

            if (_passedPlatformNumber % 2 == 0)
            {
                GameManager.Instance.GameSpeed += 2;
            }
        }
    }

    private void AddPlatformToMovingList(PlatformController newPlatform)
    {
        _movingPlatforms.Add(newPlatform);
        newPlatform.transform.position = new Vector3(0, 0, 207);
        newPlatform.gameObject.SetActive(true);
    }
    private void RemovePlatformFromMovingList(PlatformController oldPlatform)
    {
        oldPlatform.gameObject.SetActive(false);
        _movingPlatforms.Remove(oldPlatform);
    }

    private PlatformController SelectRandomPlatform()
    {
        PlatformController newPlatform;

        do
        {
            newPlatform = _allPlatforms[UnityEngine.Random.Range(0, _allPlatforms.Count)];
        } while (_movingPlatforms.Contains(newPlatform));

        return newPlatform;
    }
}
