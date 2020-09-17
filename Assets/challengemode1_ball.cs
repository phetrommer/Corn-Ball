using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class challengemode1_ball : MonoBehaviour
{
    private Transform player;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        gameObject.transform.LookAt(player);
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 8;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
