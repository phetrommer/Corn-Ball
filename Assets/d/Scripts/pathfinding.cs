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
        Physics.IgnoreLayerCollision(9, 9);
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        //switch (GameManager.Instance.currentRound)
        //{
        //    case 2:
        //        InvokeRepeating("Push", 1f, 2f);
        //        break;
        //    case 3:
        //        InvokeRepeating("Pull", 5f, 0f);
        //        break;
        //    default:
        //        break;
        //}
    }

    //void Push()
    //{
    //    this.GetComponent<Rigidbody>().AddForce(-transform.right * 5f);
    //}

    //void Pull()
    //{
    //    this.GetComponent<Rigidbody>().AddForce(transform.right * 5f);
    //}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
