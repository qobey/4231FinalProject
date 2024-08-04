using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class AudioManagerScript : MonoBehaviour
{
    [Header("Soundtrack")]
    public AudioSource musicSource;
    public AudioClip level1;

    public AudioClip level2;
    public AudioClip mainMenu;

    [Header ("Sound Effects")]
    public AudioSource sfxSource;

    public AudioClip dashSnd;
    public AudioClip playerShot;
    public AudioClip explosion;
    public AudioClip playerDeath;
    public AudioClip enemyDeath;

    
    void Start()
    {
        //musicSource.clip = mainTheme;
        SetSong();
        musicSource.Play();
        
    }

    //method for other scripts to use when they require a sound effect to be played
public void PlaySFX(AudioClip clip) 
{
    sfxSource.PlayOneShot(clip);
}

public void PlayMusic() 
{
    musicSource.Play();
}

public void SetSong()
{
    if(SceneManager.GetActiveScene().buildIndex == 0) 
    {
        // set music source to main menu
        musicSource.clip = mainMenu;
    }
    if (SceneManager.GetActiveScene().buildIndex == 1)
    {
        // set music source to main theme(level 1)
        musicSource.clip = level1;

    }
    if (SceneManager.GetActiveScene().buildIndex == 2) 
    {
        // set music source to level 2
        musicSource.clip = level2;
    }
}



}
