using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;
using UnityEngine.SceneManagement;

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
        int musicSheetSize = musicNotesToPlay.Length;
        float randomY = this.bonusPositionsY[musicNotesToPlay[this.index]];
        this.index += 1;
        if (index == musicSheetSize) {
            SceneManager.LoadScene(3);
        }
        Vector2 spawnPosition = this.transform.position + new Vector3(0, randomY);
        Instantiate(bonus, spawnPosition, Quaternion.identity);
        StartCoroutine(SpawnBonus());
    }

    private void SetupLinePositions() {
        float basePosition = this.lines[0].transform.position.y;
        float step = 0.6f;
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
        float[] minThresh = new float[]{-2.30f, -1.7f, -1.1f, -0.6f, -0.12f, 0.35f, 0.76f, 1.1f, 1.68f, 2.1f, 2.65f, 3.2f, 3.78f};
        float[] maxThresh = new float[]{-2.0f, -1.5f, -0.9f, -0.5f, -0.05f, 0.4f, 0.82f, 1.25f, 1.75f, 2.22f, 2.75f, 3.3f, 3.82f};

        for (int i = 0; i < 13; i++) {
            this.noteBottomThreshold[i] = minThresh[i];
            this.noteTopThreshold[i] = maxThresh[i];
        }
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
