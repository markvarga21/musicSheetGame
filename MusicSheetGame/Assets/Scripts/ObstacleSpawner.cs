using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2 spawnRange;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject bonus;
    [SerializeField]
    private GameObject[] lines = new GameObject[5];
    private float[] bonusPositionsY = new float[13];
    private float noteOffsetY = 0.7f;
    private float[] noteTopThreshold = new float[13];
    private float[] noteBottomThreshold = new float[13];

    private IEnumerator SpawnObstacle() {
        yield return new WaitForSeconds(1.5F);
        Vector2 spawnPosition = this.transform.position + new Vector3(0, Random.Range(-this.spawnRange.y, this.spawnRange.y));
        Instantiate(enemy, spawnPosition, Quaternion.identity);
        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnBonus() {
        yield return new WaitForSeconds(1.5F);
        int randomIndex = Random.Range(0, this.bonusPositionsY.Length);
        float randomY = this.bonusPositionsY[randomIndex];
        Vector2 spawnPosition = this.transform.position + new Vector3(0, randomY);
        Instantiate(bonus, spawnPosition, Quaternion.identity);
        StartCoroutine(SpawnBonus());
    }

    private void SetupLinePositions() {
        this.bonusPositionsY[0] = this.lines[0].transform.position.y - 0.2f;
        this.bonusPositionsY[1] = this.lines[0].transform.position.y + 0.4f;
        this.bonusPositionsY[2] = (this.lines[0].transform.position.y + this.lines[1].transform.position.y + 0.2f)/2; 
        this.bonusPositionsY[3] = this.lines[1].transform.position.y;
        this.bonusPositionsY[4] = (this.lines[1].transform.position.y + this.lines[2].transform.position.y + 0.1f)/2; 
        this.bonusPositionsY[5] = this.lines[2].transform.position.y;
        this.bonusPositionsY[6] = (this.lines[2].transform.position.y + this.lines[3].transform.position.y + 0.1f)/2; 
        this.bonusPositionsY[7] = this.lines[3].transform.position.y;
        this.bonusPositionsY[8] = (this.lines[3].transform.position.y + this.lines[4].transform.position.y + 0.1f)/2; 
        this.bonusPositionsY[9] = this.lines[4].transform.position.y;
        this.bonusPositionsY[10] = this.lines[4].transform.position.y + 0.6f;
        this.bonusPositionsY[11] = this.lines[4].transform.position.y + 0.9f;
        this.bonusPositionsY[12] = this.lines[4].transform.position.y + 1.5f;
        OffsetNotes();
    }

    private void OffsetNotes() {
        for (int i = 0; i < this.bonusPositionsY.Length; i++) {
            this.bonusPositionsY[i] += this.noteOffsetY;
        }
    }

    void Awake() {
        SetupLinePositions();
        FillCollisionBonusPositions();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObstacle());
        StartCoroutine(SpawnBonus());   
    }

    private void FillCollisionBonusPositions()
    {
        this.noteBottomThreshold[0] = -2.0f;
        this.noteTopThreshold[0] = -1.95f;

        this.noteBottomThreshold[1] = -1.4f;
        this.noteTopThreshold[1] = -1.3f;

        this.noteBottomThreshold[2] = -1.25f;
        this.noteTopThreshold[2] = -1.2f;

        this.noteBottomThreshold[3] = -0.9f;
        this.noteTopThreshold[3] = -0.85f;

        this.noteBottomThreshold[4] = -0.35f;
        this.noteTopThreshold[4] = -0.3f;  

        this.noteBottomThreshold[5] = 0.1f;
        this.noteTopThreshold[5] = 0.14f;  

        this.noteBottomThreshold[6] = 0.60f;
        this.noteTopThreshold[6] = 0.70f;  

        this.noteBottomThreshold[7] = 0.9f;
        this.noteTopThreshold[7] = 1.12f; 

        this.noteBottomThreshold[8] = 1.60f;
        this.noteTopThreshold[8] = 1.70f; 

        this.noteBottomThreshold[9] = 2.0f;
        this.noteTopThreshold[9] = 2.12f; 

        this.noteBottomThreshold[10] = 2.65f;
        this.noteTopThreshold[10] = 2.75f; 

        this.noteBottomThreshold[11] = 2.9f;
        this.noteTopThreshold[11] = 3.1f; 

        this.noteBottomThreshold[12] = 3.55f;
        this.noteTopThreshold[12] = 3.65f; 
    }

    public int getLineIndexForY(float yPos) {
        for (int i = 0; i < this.bonusPositionsY.Length; i++) {
            if (this.noteBottomThreshold[i] <= yPos && this.noteTopThreshold[i] >= yPos) {
                Debug.Log("index for " + yPos.ToString() + " is " + i.ToString());
                return i;
            }
        }
        return 0;
    }

}
