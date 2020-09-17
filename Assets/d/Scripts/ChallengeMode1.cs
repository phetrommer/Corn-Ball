using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeMode1 : MonoBehaviour
{
    public Transform cube;
    public Transform[] currentPoint;
    public float speed;
    int targetIndex = 0;

    public Transform ball;

    private void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint[targetIndex].position, Time.deltaTime * speed);

        if (transform.position == currentPoint[targetIndex].position)
        {
            targetIndex = (targetIndex + 1) % currentPoint.Length;
        }
    }

    IEnumerator spawnEnemy()
    {
        while (true)
        {
            Instantiate(ball, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(1f);
        }
    }
}
