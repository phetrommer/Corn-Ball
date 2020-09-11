using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RoundManager : MonoBehaviour
{
    public enum RoundState { SPAWNING, WAITING, COUNTING };
    public int nextRound = 0;

    [System.Serializable]
    public class Wave
    {
        public GameObject enemy;
        [Tooltip("Amount of times this wave will spawn")]
        public int spawnCount;
        [Tooltip("Amount of time between these wave spawns")]
        public float spawnInterval;
        public float timeAfterWave = 0.2f;
        [Header("Spawns")]
        public int topSpawn;
        public int bottomSpawn;
        public int leftSpawn;
        public int rightSpawn;
    }

    [System.Serializable]
    public class Round
    {
        public Wave[] waves;
    }

    public float timeBetweenRounds = 5f;
    public Transform[] spawnPoints;

    private int round;
    public Round[] rounds;

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
                GameManager.Instance.currentPoints += 200f;
                RoundCompleted();
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
            if (GameObject.FindGameObjectWithTag("Enemy") == null && GameObject.FindGameObjectWithTag("Boss") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator StartRound(Round _round)
    {
        GameManager.Instance.currentRound = ++round;
        Debug.Log("starting round: " + round);
        state = RoundState.SPAWNING;

        for (int i = 0; i < _round.waves.Length; i++)
        {
            for (int k = 0; k < _round.waves[i].spawnCount; k++)
            {
                spawnEnemy(_round.waves[i]);
                yield return new WaitForSeconds(_round.waves[i].spawnInterval);
            }
            yield return new WaitForSeconds(_round.waves[i].timeAfterWave);
        }

        state = RoundState.WAITING;
    }

    void RoundCompleted()
    {
        Debug.Log("round completed");

        state = RoundState.COUNTING;
        roundCountdown = timeBetweenRounds;

        if (nextRound + 1 > rounds.Length - 1)
        {
            nextRound = 0;
            Debug.Log("all rounds completed");
        }
        else
        {
            nextRound++;
        }
    }

    public void spawnEnemy(Wave _enemy)
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint.name.Equals("spawnBottom"))
            {
                for (int k = 0; k < _enemy.bottomSpawn; k++)
                {
                    Instantiate(_enemy.enemy, RandomPointInBounds(spawnPoint.GetComponent<Collider>().bounds), Quaternion.Euler(0, -180, 0));
                }
            }
            else if (spawnPoint.name.Equals("spawnTop"))
            {
                for (int k = 0; k < _enemy.topSpawn; k++)
                {
                    Instantiate(_enemy.enemy, RandomPointInBounds(spawnPoint.GetComponent<Collider>().bounds), Quaternion.Euler(0, 0, 0));
                }
            }
            else if (spawnPoint.name.Equals("spawnLeft"))
            {
                for (int k = 0; k < _enemy.leftSpawn; k++)
                {
                    Instantiate(_enemy.enemy, RandomPointInBoundsLR(spawnPoint.GetComponent<Collider>().bounds), Quaternion.Euler(0, -90, 0));
                }
            }
            else if (spawnPoint.name.Equals("spawnRight"))
            {
                for (int k = 0; k < _enemy.rightSpawn; k++)
                {
                    Instantiate(_enemy.enemy, RandomPointInBoundsLR(spawnPoint.GetComponent<Collider>().bounds), Quaternion.Euler(0, 90, 0));
                }
            }
        }
    }
    
    //refactor me into 1 method, convert world vector to local? 
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(bounds.center.x, 0.5f, bounds.center.z + (int)Random.Range(-10, 10));
    }
    public static Vector3 RandomPointInBoundsLR(Bounds bounds)
    {
        return new Vector3(bounds.center.x + (int)Random.Range(-10, 10), 0.5f, bounds.center.z);
    }
}
