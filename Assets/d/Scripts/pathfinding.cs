using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class pathfinding : MonoBehaviour
{
    public float speed = 3f;
    private void Start()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
