using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class fastBall : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        Physics.IgnoreLayerCollision(9, 9);
        Invoke("setSpeed", 1.5f);
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public GameObject trailprefab;
    public Transform fastboi;
    void setSpeed()
    {
        speed = 15f;
        Instantiate(trailprefab, transform.position, transform.rotation, fastboi);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
