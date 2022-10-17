using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip[] audioClips = new AudioClip[13];

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playClipAtIndex(int index) {
        this.source.PlayOneShot(this.audioClips[index]);   
    }

}
