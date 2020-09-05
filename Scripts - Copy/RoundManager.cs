using System.Collections;
using UnityEngine;


public class RoundManager : MonoBehaviour
{
    public enum RoundState { SPAWNING, WAITING, COUNTING };
    
    [System.Serializable]
    public class Round
    {
        public int round;
        public Transform enemy;
        public int spawnCount;
        public float spawnInterval;
    }
    public float timeBetweenRounds = 5f;
    public Transform[] spawnPoints;

    public Round[] rounds;
    private int nextRound = 0;

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
        GameManager.Instance.currentRound += 1;
        Debug.Log("starting round: " + _round.round);
        state = RoundState.SPAWNING;

        for (int i = 0; i < _round.spawnCount; i++)
        {
            spawnEnemy(_round.enemy);
            yield return new WaitForSeconds(_round.spawnInterval);
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
