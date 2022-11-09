using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip[] audioClips = new AudioClip[13];
    [SerializeField]
    private AudioClip gameOverSound;
    [SerializeField]
    private AudioClip obstacleHit;

    public void playClipAtIndex(int index) {
        this.source.PlayOneShot(this.audioClips[index]);   
    }

    public void playGameOverSound() {
        this.source.PlayOneShot(this.obstacleHit);
        this.source.PlayOneShot(this.gameOverSound);
    }

}
