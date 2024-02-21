using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] int movingSpeed;
    void Update()
    {
        transform.position += new Vector3(0, 0, -movingSpeed) * Time.deltaTime;
    }
}
