using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2 spawnRange;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject bonus;


    private IEnumerator SpawnObstacle() {
        yield return new WaitForSeconds(1.5F);
        Vector2 spawnPosition = this.transform.position + new Vector3(0, Random.Range(-this.spawnRange.y, this.spawnRange.y));
        Instantiate(enemy, spawnPosition, Quaternion.identity);
        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnBonus() {
        yield return new WaitForSeconds(1.5F);
        Vector2 spawnPosition = this.transform.position + new Vector3(0, Random.Range(-this.spawnRange.y, this.spawnRange.y));
        Instantiate(bonus, spawnPosition, Quaternion.identity);
        StartCoroutine(SpawnBonus());
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObstacle());
        StartCoroutine(SpawnBonus());   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
