using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reverseBall : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        Physics.IgnoreLayerCollision(9, 9);
        Invoke("setSpeed", Random.Range(4f, 6f));
    }

    void setSpeed()
    {
        speed = -speed - 3;
        gameObject.GetComponent<Renderer>().material.color = Color.black;
        
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
