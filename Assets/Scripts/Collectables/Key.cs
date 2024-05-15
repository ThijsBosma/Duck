using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        KeyCollector.Instance._Key += 1;       
        Destroy(gameObject);
    }
}
