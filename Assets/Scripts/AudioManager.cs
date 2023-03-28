using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource bgMusic, winMusic;

    public bool isBgMusicOn, isSfxOn;

    void Awake(){
        DontDestroyOnLoad(gameObject);
        
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start(){
        bgMusic.loop = true;
        bgMusic.playOnAwake = true;

        winMusic.loop = false;
        winMusic.playOnAwake = false;
    }
    public void ToggleBgMusic(bool value){
        isBgMusicOn = value;
        bgMusic.mute = !isBgMusicOn;
    }
    public void ToggleSfx(bool value){
        isSfxOn = value;
    }
    public void PlayWinSound(){
        if (isSfxOn) winMusic.Play();
    }
}
