using System.Collections;
using System.Threading;
using UnityEngine;

public class ChallengeMode2 : MonoBehaviour
{
    private Animator animator;
    private bool isAnimating = false;
    public GameObject ball;
    public float shootRate = 0.1f;
    int count = 0;
    private RoundManager.RoundState state;

    private void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("RaiseTurret", 1f, 5f);
        InvokeRepeating("spawnBall", 0f, shootRate);        
    }

    void RaiseTurret()
    {
        isAnimating = !isAnimating;
        animator.SetBool("isAnimating", isAnimating);
    }

    void spawnBall()
    {
        if (isAnimating == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle2") && state == RoundManager.RoundState.SPAWNING)
        {
            Instantiate(ball, new Vector3(transform.position.x, transform.position.y+0.8f, transform.position.z), Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            //if (count == 360)
            //{
            //    count = 0;
            //}
            //else
            //{
            //    count += 10;
            //}
        }
    }
}
