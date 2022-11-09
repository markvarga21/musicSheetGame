using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallEventManager : MonoBehaviour
{
    [SerializeField]
    private int score;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private GameObject audioManagerObject;
    AudioManager audioManager;
    [SerializeField]
    private GameObject obstacleSpawnerObject;
    ObstacleSpawner obstacleSpawner;

    void Start()
    {
        this.obstacleSpawner = this.obstacleSpawnerObject.GetComponent<ObstacleSpawner>();
    }

    private void Awake() {
        this.score = 0;
        this.audioManager = this.audioManagerObject.GetComponent<AudioManager>();
        DontDestroyOnLoad(this.audioManager);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Bonus")) {
            int index = this.obstacleSpawner.getLineIndexForY(collision.transform.position.y);
            Debug.Log("Collision Y at: " + collision.transform.position.y.ToString() + " with line index: " + index.ToString());
            this.audioManager.playClipAtIndex(index);
            this.score++;
            this.scoreText.text = score.ToString();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacle")) {
            FreezeObstacles();
            FreezeNotes();
            FreezeCircle();
            this.audioManager.playGameOverSound();
            PlayerPrefs.SetInt("score", this.score);
            this.scoreText.text = score.ToString();
            collision.GetComponent<ParticleSystem>().Play();
            collision.GetComponent<SpriteRenderer>().enabled = false; 
            StartCoroutine(LoadSceneDelayed());
        }
    }

    private IEnumerator LoadSceneDelayed() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }

    private void FreezeObstacles() {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < gameObjects.Length; i++) {
            gameObjects[i].GetComponent<ObstacleMover>().setObstacleMoveSpeed(0f);
        }
    }

    private void FreezeNotes() {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Bonus");
        for (int i = 0; i < gameObjects.Length; i++) {
            gameObjects[i].GetComponent<ObstacleMover>().setObstacleMoveSpeed(0f);
        }
    }

    private void FreezeCircle() {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Circle");
        gameObjects[0].GetComponent<BallRotator>().SetBallSpeed(0f);
    }

}
