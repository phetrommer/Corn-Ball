using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.VFX;

public class RoundManager : MonoBehaviour
{
    public enum RoundState { SPAWNING, WAITING, COUNTING };
    
    [System.Serializable]
    public class Round
    {
        public int round;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Round[] rounds;
    private int nextRound = 0;

    public float timeBetweenRounds = 5f;
    private float roundCountdown;

    private float searchCountdown = 1f;

    private RoundState state = RoundState.COUNTING;

    private void Start()
    {
        roundCountdown = timeBetweenRounds;

    }

    private void Update()
    {
        if (state == RoundState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                Debug.Log("round compelted!");
            }
            else
            {
                return;
            }
        }
        
        if (roundCountdown <= 0)
        {
            if (state != RoundState.SPAWNING)
            {
                StartCoroutine(StartRound(rounds[nextRound]));
            }
        }
        else
        {
            roundCountdown -= Time.deltaTime;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator StartRound(Round _round)
    {
        Debug.Log("starting round: " + _round.round);
        state = RoundState.SPAWNING;

        for (int k = 0; k < _round.round; k++)
        {
            spawnEnemy(_round.enemy);
            yield return new WaitForSeconds(1f / _round.rate);
        }

        state = RoundState.WAITING;

        yield break;
    }

    public Transform[] spawnPoints;

    public void spawnEnemy(Transform _enemy)
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint.name.Equals("spawnBottom"))
            {
                Instantiate(_enemy, RandomPointInBounds(spawnPoint.GetComponent<Collider>().bounds), Quaternion.Euler(0, -180, 0));
            }
            else if (spawnPoint.name.Equals("spawnTop"))
            {
                Instantiate(_enemy, RandomPointInBounds(spawnPoint.GetComponent<Collider>().bounds), Quaternion.Euler(0, 0, 0));
            }
            else if (spawnPoint.name.Equals("spawnLeft"))
            {
                Instantiate(_enemy, RandomPointInBounds(spawnPoint.GetComponent<Collider>().bounds), Quaternion.Euler(0, -90, 0));
            }
            else if (spawnPoint.name.Equals("spawnRight"))
            {
                Instantiate(_enemy, RandomPointInBounds(spawnPoint.GetComponent<Collider>().bounds), Quaternion.Euler(0, 90, 0));
            }
        }
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            0.5f,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
