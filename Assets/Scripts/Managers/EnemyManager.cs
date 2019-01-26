using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject[] enemy;
    public float spawnTimeBunny = 3f;
    public float spawnTimeBear = 5f;
    public float spawnTimeHellephant = 10f;
    public Transform[] spawnPoints;


    float timerBunny, timerBear, timerHellephant;
     
    void Start ()
    {
        
    }

    private void Update()
    {
        timerBunny += Time.deltaTime;
        timerBear += Time.deltaTime;
        timerHellephant += Time.deltaTime;

        if (spawnTimeBunny <= timerBunny)
        {
            Spawn(2);
            timerBunny = 0;
        }
        if (spawnTimeBear <= timerBear)
        {
            Spawn(1);
            timerBear = 0;
        }
        if (spawnTimeHellephant <= timerHellephant)
        {
            Spawn(0);
            timerHellephant = 0;
        }
    }


    void Spawn (int enemyIndex)
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
