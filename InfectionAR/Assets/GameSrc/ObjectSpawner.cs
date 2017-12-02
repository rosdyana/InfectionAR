using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public Rigidbody rbEnemy;
    [SerializeField]
    private int num_of_enemy;
    [SerializeField]
    private int freqEnemy;


    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1.5f, freqEnemy);
    }

    void SpawnEnemy()
    {
        for (byte i = 0; i < num_of_enemy; i++)
        {
            Rigidbody cloneBottle;
            cloneBottle = Instantiate(rbEnemy, new Vector3(Random.Range(-1, 1), 0, Random.Range(1, 3)), Quaternion.identity) as Rigidbody;
        }
    }

}