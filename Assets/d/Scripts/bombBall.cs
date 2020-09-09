using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class bombBall : MonoBehaviour
{
    public float speed = 3f;
    private bool collided = false;
    private Transform target;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Boss").transform;
        Physics.IgnoreLayerCollision(13, 9);
    }

    void Update()
    {
        if (collided == false)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 7 * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collided = true;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
