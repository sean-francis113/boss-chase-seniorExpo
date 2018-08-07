using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class soundPack : MonoBehaviour {

    /*
     * Sound Source ID
     */
    public SOUNDSOURCE sourceID;

    /*
     * Music
     */
    [Header("Music Files")]
    public AudioClip genBG;
    public AudioClip bossBG;
    public AudioClip bossNearDeathBG;
    public AudioClip bossDeadBG;
    public AudioClip playerNearDeathBG;
    public AudioClip playerDeadBG;

    /*
     * Sounds
     */
    [Header("Sound Files")]
    public AudioClip footstep;
    public AudioClip attack;
    public AudioClip damaged;
    public AudioClip death;
    public AudioClip attackHit;
    public AudioClip blocked;
    public List<AudioClip> ambient;

    private AudioClip soundSource;

    public void SetSoundIntoSource(SOUND type)
    {

        switch (type)
        {

            case SOUND.FOOTSTEP:
                audioManager.instance.SetSoundIntoSource(
                    footstep, 
                    SOUND.FOOTSTEP
                    );
                break;
            case SOUND.ATTACK:
                audioManager.instance.SetSoundIntoSource(
                    attack, 
                    SOUND.ATTACK
                    );
                break;
            case SOUND.DAMAGED:
                audioManager.instance.SetSoundIntoSource(
                    damaged, 
                    SOUND.DAMAGED
                    );
                break;
            case SOUND.DEATH:
                audioManager.instance.SetSoundIntoSource(
                    death, 
                    SOUND.DEATH
                    );
                break;
            case SOUND.ATTACKHIT:
                audioManager.instance.SetSoundIntoSource(
                    attackHit, 
                    SOUND.ATTACKHIT
                    );
                break;
            case SOUND.BLOCKED:
                audioManager.instance.SetSoundIntoSource(
                    blocked,
                    SOUND.BLOCKED
                    );
                break;
            case SOUND.AMBIENT:
                audioManager.instance.SetSoundIntoSource(
                    ambient[Random.Range(0, ambient.Count - 1)], 
                    SOUND.AMBIENT
                    );
                break;

        }

    }

    public void SetMusicIntoSource(MUSIC type)
    {

        switch (type)
        {

            case MUSIC.GEN_BG:
                audioManager.instance.SetMusicIntoSource(
                    genBG,
                    MUSIC.GEN_BG
                    );
                break;
            case MUSIC.BOSS_BG:
                audioManager.instance.SetMusicIntoSource(
                    bossBG,
                    MUSIC.BOSS_BG
                    );
                break;
            case MUSIC.BOSS_NEARDEATH:
                audioManager.instance.SetMusicIntoSource(
                    bossNearDeathBG,
                    MUSIC.BOSS_NEARDEATH
                    );
                break;
            case MUSIC.BOSS_DEATH:
                audioManager.instance.SetMusicIntoSource(
                    bossDeadBG,
                    MUSIC.BOSS_DEATH
                    );
                break;
            case MUSIC.PLAYER_DEATH:
                audioManager.instance.SetMusicIntoSource(
                    playerDeadBG,
                    MUSIC.PLAYER_DEATH
                    );
                break;
            case MUSIC.PLAYER_NEARDEATH:
                audioManager.instance.SetMusicIntoSource(
                    playerNearDeathBG,
                    MUSIC.PLAYER_NEARDEATH
                    );
                break;

        }

    }

    public void SetSound(AudioClip newSound, SOUND type)
    {

        switch(type)
        {

            case SOUND.FOOTSTEP:
                footstep = newSound;
                break;
            case SOUND.ATTACK:
                attack = newSound;
                break;
            case SOUND.DAMAGED:
                damaged = newSound;
                break;
            case SOUND.DEATH:
                death = newSound;
                break;
            case SOUND.ATTACKHIT:
                attackHit = newSound;
                break;
            case SOUND.AMBIENT:
                ambient.Add(newSound);
                break;
            case SOUND.BLOCKED:
                blocked = newSound;
                break;

        }

    }

    public void SetMusic(AudioClip newMusic, MUSIC type)
    {

        switch (type)
        {

            case MUSIC.GEN_BG:
                genBG = newMusic;
                break;
            case MUSIC.BOSS_BG:
                bossBG = newMusic;
                break;
            case MUSIC.BOSS_DEATH:
                bossDeadBG = newMusic;
                break;
            case MUSIC.BOSS_NEARDEATH:
                bossNearDeathBG = newMusic;
                break;
            case MUSIC.PLAYER_DEATH:
                playerDeadBG = newMusic;
                break;
            case MUSIC.PLAYER_NEARDEATH:
                playerNearDeathBG = newMusic;
                break;

        }

    }

    public void SetAmbient(List<AudioClip> list)
    {

        if(list.Count <= 0)
        {

            return;

        }

        if(ambient.Count <= 0)
        {

            ambient = new List<AudioClip>();

        }

        for(int i = 0; i < list.Count; i++)
        {

            ambient.Add(list[i]);

        }

    }

    public void SetAmbient(AudioClip[] list)
    {

        if (list.Length <= 0)
        {

            return;

        }

        if (ambient.Count <= 0)
        {

            ambient = new List<AudioClip>();

        }

        for (int i = 0; i < list.Length; i++)
        {

            ambient.Add(list[i]);

        }

    }

    public void PlaySound(SOUND type)
    {

        switch (type)
        {

            case SOUND.FOOTSTEP:
                audioManager.instance.PlaySound(
                    footstep, 
                    sourceID
                    );
                break;
            case SOUND.ATTACK:
                audioManager.instance.PlaySound(
                    attack, 
                    sourceID
                    );
                break;
            case SOUND.DAMAGED:
                audioManager.instance.PlaySound(
                    damaged, 
                    sourceID
                    );
                break;
            case SOUND.DEATH:
                audioManager.instance.PlaySound(
                    death, 
                    sourceID
                    );
                break;
            case SOUND.ATTACKHIT:
                audioManager.instance.PlaySound(
                    attackHit, 
                    sourceID
                    );
                break;
            case SOUND.AMBIENT:
                audioManager.instance.PlaySound(
                    ambient[Random.Range(0, ambient.Count - 1)], 
                    sourceID
                    );
                break;
            case SOUND.BLOCKED:
                audioManager.instance.PlaySound(
                    blocked,
                    sourceID
                    );
                break;

        }

    }

}
