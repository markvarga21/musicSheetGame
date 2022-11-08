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

    private FileManager fileManager;
    private SheetConverter sheetConverter;
    private int[] musicNotesToPlay;

    private int index = 0;

    private IEnumerator SpawnObstacle() {
        yield return new WaitForSeconds(1F);
        Vector2 spawnPosition = this.transform.position + new Vector3(0, Random.Range(-this.spawnRange.y, this.spawnRange.y));
        Instantiate(enemy, spawnPosition, Quaternion.identity);
        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnBonus() {
        yield return new WaitForSeconds(1.5F);
        Debug.Log("Index is: " + this.index.ToString());
        //int randomIndex = Random.Range(0, this.bonusPositionsY.Length); //musicNotesToPlay[this.index]
        float randomY = this.bonusPositionsY[6];
        this.index += 1;
        Vector2 spawnPosition = this.transform.position + new Vector3(0, randomY);
        Instantiate(bonus, spawnPosition, Quaternion.identity);
        StartCoroutine(SpawnBonus());
    }

    private void SetupLinePositions() {
        float basePosition = this.lines[0].transform.position.y;
        float step = 0.6f;
        /*
        this.bonusPositionsY[0] = basePosition;
        this.bonusPositionsY[1] = basePosition + 1f*step;
        this.bonusPositionsY[2] = basePosition + 2f*step;
        this.bonusPositionsY[3] = basePosition + 2.7f*step;
        this.bonusPositionsY[4] = basePosition + 3.5f*step; 
        this.bonusPositionsY[5] = basePosition + 4.3f*step;
        this.bonusPositionsY[6] = basePosition + 5f*step; 
        this.bonusPositionsY[7] = basePosition + 5.7f*step;
        this.bonusPositionsY[8] = basePosition + 6.5f*step; 
        this.bonusPositionsY[9] = basePosition + 7.3f*step;
        this.bonusPositionsY[10] = basePosition + 8.2f*step;
        this.bonusPositionsY[11] = basePosition + 9.1f*step;
        this.bonusPositionsY[12] = basePosition + 10f*step;
        */

        float[] offsetValues = new float[]{0f, 1f, 2f, 2.7f, 3.5f, 4.3f, 5f, 5.7f, 6.5f, 7.3f, 8.2f, 9.1f, 10f};

        for (int i = 0; i < 13; i++) {
            this.bonusPositionsY[i] = basePosition + offsetValues[i]*step;
        }

        OffsetNotes();
    }

    private void OffsetNotes() {
        for (int i = 0; i < this.bonusPositionsY.Length; i++) {
            this.bonusPositionsY[i] += this.noteOffsetY;
        }
    }

    void Awake() {
        this.fileManager = this.GetComponent<FileManager>();
        this.sheetConverter = this.GetComponent<SheetConverter>();
        string notesFromFile = this.fileManager.getNotesFromFile();
        Debug.Log("Notes from file: " + notesFromFile);
        this.musicNotesToPlay = this.sheetConverter.getLineIndexArrayForString(notesFromFile);
        Debug.Log("musicNotesToPlay size: " + this.musicNotesToPlay.Length);
        SetupLinePositions();
        FillCollisionBonusPositions();
    }

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
