using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RoundManager : MonoBehaviour
{
    public enum RoundState { SPAWNING, WAITING, COUNTING };
    public int nextRound = 0;

    [System.Serializable]
    public class enemyTypes
    {
        public GameObject enemy;
        public int spawnCount;
        public float spawnInterval;
    }

    [System.Serializable]
    public class Round
    {
        public enemyTypes[] enemies;
    }

    public float timeBetweenRounds = 5f;
    public Transform[] spawnPoints;

    public int round;
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
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
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

        for (int i = 0; i < _round.enemies.Length; i++)
        {
            for (int k = 0; k < _round.enemies[i].spawnCount; k++)
            {
                spawnEnemy(_round.enemies[i].enemy);
                yield return new WaitForSeconds(_round.enemies[i].spawnInterval);
            }
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

    public void spawnEnemy(GameObject _enemy)
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
