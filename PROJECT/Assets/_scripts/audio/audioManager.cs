using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audioManager : MonoBehaviour {

    public static audioManager instance;

    [Header("Mixers")]
    public AudioMixer master;
    public AudioMixer BGM;
    public AudioMixer SFX;

    [Header("Snapshots")]
    public AudioMixerSnapshot changingBiomeSnap;
    public AudioMixerSnapshot menuBGSnap;
    public AudioMixerSnapshot genBGSnap;
    public AudioMixerSnapshot bossBGSnap;
    public AudioMixerSnapshot bossNearDeathBGSnap;
    public AudioMixerSnapshot bossDeadBGSnap;
    public AudioMixerSnapshot playerNearDeathBGSnap;
    public AudioMixerSnapshot playerDeadBGSnap;

    [Header("Audio Sources")]
    public AudioSource menuBG;
    public AudioSource genBG;
    public AudioSource bossBG;
    public AudioSource bossNearDeathBG;
    public AudioSource bossDeadBG;
    public AudioSource playerNearDeathBG;
    public AudioSource playerDeadBG;
    public AudioSource playerSFX;
    public AudioSource enemySFX;
    public AudioSource ambientSFX;
    public AudioSource menuSFX;

    [Header("Default Values")]
    public float defaultTransitionTime;
    public float defaultMasterVolume;
    public float defaultBGMVolume;
    public float defaultSFXVolume;

    [Header("Current Values")]
    public float masterVolume;
    public float BGMVolume;
    public float SFXVolume;

    private MUSIC currentMusicSnapshot = MUSIC.MENU_BG;

    // Use this for initialization
    void Awake () {

        if(!instance)
        {

            instance = this;

        }
        else
        {

            Destroy(this);

        }

        masterVolume = defaultMasterVolume;
        BGMVolume = defaultBGMVolume;
        SFXVolume = defaultSFXVolume;
		
	}

    public void CrossFade(MUSIC transitionTo)
    {
        if (CheckPriority(currentMusicSnapshot, transitionTo))
        {

            CrossFade(transitionTo, defaultTransitionTime);

        }

    }

    public void CrossFade(MUSIC transitionTo, float transitionTime)
    {

       switch (transitionTo)
            {

                case MUSIC.CHANGING_BIOME:
                    //nextSnap = changingBiomeSnap;
                    changingBiomeSnap.TransitionTo(transitionTime);
                currentMusicSnapshot = MUSIC.CHANGING_BIOME;
                    break;
                case MUSIC.MENU_BG:
                    menuBGSnap.TransitionTo(transitionTime);
                currentMusicSnapshot = MUSIC.MENU_BG;
                    break;
                case MUSIC.GEN_BG:
                    //nextSnap = genBGSnap;
                    genBGSnap.TransitionTo(transitionTime);
                currentMusicSnapshot = MUSIC.GEN_BG;
                    break;
                case MUSIC.BOSS_BG:
                    //nextSnap = bossBGSnap;
                    bossBGSnap.TransitionTo(transitionTime);
                currentMusicSnapshot = MUSIC.BOSS_BG;
                    break;
                case MUSIC.BOSS_NEARDEATH:
                    //nextSnap = bossNearDeathBGSnap;
                    bossNearDeathBGSnap.TransitionTo(transitionTime);
                currentMusicSnapshot = MUSIC.BOSS_NEARDEATH;
                    break;
                case MUSIC.BOSS_DEATH:
                    //nextSnap = bossNearDeathBGSnap;
                    bossDeadBGSnap.TransitionTo(transitionTime);
                currentMusicSnapshot = MUSIC.BOSS_DEATH;
                    break;
                case MUSIC.PLAYER_NEARDEATH:
                    //nextSnap = playerNearDeathBGSnap;
                    playerNearDeathBGSnap.TransitionTo(transitionTime);
                currentMusicSnapshot = MUSIC.PLAYER_NEARDEATH;
                    break;
                case MUSIC.PLAYER_DEATH:
                    //nextSnap = playerDeadBGSnap;
                    playerDeadBGSnap.TransitionTo(transitionTime);
                currentMusicSnapshot = MUSIC.PLAYER_DEATH;
                    break;

       }

    }

    public void PlaySound(AudioClip sound, SOUNDSOURCE source)
    {

        switch(source)
        {

            case SOUNDSOURCE.PLAYER:
                playerSFX.PlayOneShot(sound);
                break;
            case SOUNDSOURCE.ENEMY:
                enemySFX.PlayOneShot(sound);
                break;
            case SOUNDSOURCE.AMBIENCE:
                ambientSFX.PlayOneShot(sound);
                break;
            case SOUNDSOURCE.MENU:
                menuSFX.PlayOneShot(sound);
                break;
            case SOUNDSOURCE.NULL:
                Debug.LogWarning("Cannot Play Sound: No Source");
                break;

        }

    }

    public bool CheckPriority(MUSIC currentState, MUSIC toCheck)
    {

        /*
         * Check Special Cases
         * (Menu -> GenBG, Changing Biome -> Menu)
         */

        if(currentState == MUSIC.MENU_BG && toCheck == MUSIC.GEN_BG)
        {

            return true;

        }
        else if(currentState == MUSIC.CHANGING_BIOME && toCheck == MUSIC.MENU_BG)
        {

            return true;

        }
        else if(currentState == MUSIC.CHANGING_BIOME && toCheck == MUSIC.GEN_BG)
        {

            return true;

        }

        /*
         * Special Cases Checked
         * Move to Normal Cases
         */

        if(toCheck < currentState)
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    public void SetSoundIntoSource(AudioClip sound, SOUND type)
    {



    }

    public void SetMusicIntoSource(AudioClip music, MUSIC type)
    {

        switch (type)
        {
            case MUSIC.MENU_BG:
                menuBG.clip = music;
                break;
            case MUSIC.GEN_BG:
                genBG.clip = music;
                break;
            case MUSIC.BOSS_BG:
                bossBG.clip = music;
                break;
            case MUSIC.BOSS_NEARDEATH:
                bossNearDeathBG.clip = music;
                break;
            case MUSIC.BOSS_DEATH:
                bossDeadBG.clip = music;
                break;
            case MUSIC.PLAYER_DEATH:
                playerDeadBG.clip = music;
                break;
            case MUSIC.PLAYER_NEARDEATH:
                playerNearDeathBG.clip = music;
                break;

        }

    }

    public void SetMasterVolume(float value)
    {

        master.SetFloat("masterVolume", value);
        masterVolume = value;
        menuManager.instance.SetAudioSettings();

    }

    public void SetBGMVolume(float value)
    {

        master.SetFloat("BGMVolume", value);
        BGMVolume = value;
        menuManager.instance.SetAudioSettings();

    }

    public void SetSFXVolume(float value)
    {

        master.SetFloat("SFXVolume", value);
        SFXVolume = value;
        menuManager.instance.SetAudioSettings();

    }

}

public enum SOUND
{

    NULL,
    FOOTSTEP,
    ATTACK,
    DAMAGED,
    DEATH,
    ATTACKHIT,
    AMBIENT,
    BLOCKED,
    MENU

}

public enum MUSIC
{

    NULL = 0,
    CHANGING_BIOME = 2,
    MENU_BG = 1,
    GEN_BG = 8,
    BOSS_BG = 7,
    BOSS_NEARDEATH = 6,
    BOSS_DEATH = 5,
    PLAYER_NEARDEATH = 4,
    PLAYER_DEATH = 3

}

public enum SOUNDSOURCE
{

    NULL,
    PLAYER,
    ENEMY,
    AMBIENCE,
    MENU

}
