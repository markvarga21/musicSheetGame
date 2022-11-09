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
            this.audioManager.playGameOverSound();
            PlayerPrefs.SetInt("score", this.score);
            this.scoreText.text = score.ToString();
            SceneManager.LoadScene(2);
        }
    }

}
