using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other);
        }
    }
}
