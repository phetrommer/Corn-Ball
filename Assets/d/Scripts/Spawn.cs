using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Rigidbody enemy;
    public Transform[] spawnPoints;

    void Start()
    {
        InvokeRepeating("spawnEnemy", 1f, 2f);
    }

    public void spawnEnemy()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint.name.Equals("spawnBottom"))
            {
                Instantiate(enemy, RandomPointInBounds(spawnPoint.GetComponent<Collider>().bounds), Quaternion.Euler(0, -180, 0));
            }
            else if (spawnPoint.name.Equals("spawnTop"))
            {
                Instantiate(enemy, RandomPointInBounds(spawnPoint.GetComponent<Collider>().bounds), Quaternion.Euler(0, 0, 0));
            }
            else if (spawnPoint.name.Equals("spawnLeft"))
            {
                Instantiate(enemy, RandomPointInBounds(spawnPoint.GetComponent<Collider>().bounds), Quaternion.Euler(0, -90, 0));
            }
            else if (spawnPoint.name.Equals("spawnRight"))
            {
                Instantiate(enemy, RandomPointInBounds(spawnPoint.GetComponent<Collider>().bounds), Quaternion.Euler(0, 90, 0));
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
