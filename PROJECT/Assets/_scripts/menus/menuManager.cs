using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuManager : MonoBehaviour {

    public static menuManager instance;
    public camMove cMove;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject characterMenu;
    public GameObject optionsMenu;
    public GameObject inGameOptionsMenu;
    public GameObject gameUI;
    public GameObject pauseUI;
    public GameObject deathUI;
    public GameObject aboutMenu;

    [Header("Loading Screen")]
    public GameObject loadingScreen;

    [Header("Quitting Screen")]
    public GameObject quittingScreen;

    [Header("Player")]
    public GameObject player;

    [Header("Volume Sliders")]
    public Slider masterVolSlider;
    public Slider masterVolSliderInGame;
    public Slider BGMVolSlider;
    public Slider BGMVolSliderInGame;
    public Slider SFXVolSlider;
    public Slider SFXVolSliderInGame;

    [Header("Boss Health Slider")]
    public Slider bossHealthSlider;

    [Header("Death Stats")]
    public Text runTimeMins;
    public Text runTimeSecs;
    public Text minionKills;
    public Text bossKills;

    [Header("Menu Sounds")]
    public AudioClip buttonSound;

    private trackConstructor constructor;
    private spawnEnemies spawn;

    private void Awake()
    {

        if(!instance)
        {

            instance = this;

        }
        else
        {

            Destroy(this);

        }

    }

    public void ClearAll()
    {

        mainMenu.SetActive(false);
        //characterMenu.SetActive(false);
        quittingScreen.SetActive(false);
        optionsMenu.SetActive(false);
        inGameOptionsMenu.SetActive(false);
        loadingScreen.SetActive(false);
        gameUI.SetActive(false);
        pauseUI.SetActive(false);
        deathUI.SetActive(false);
        aboutMenu.SetActive(false);

    }

    public void ShowMainMenu()
    {

        ResetGame();
        pauseState.instance.SetCanPause(false);
        audioManager.instance.CrossFade(MUSIC.MENU_BG);
        ClearAll();
        mainMenu.SetActive(true);

    }

    public void ResumeGame()
    {

        ShowGameUI();
        spawnEnemies.instance.SetCanSpawnMinions(true);
        trackConstructor.instance.SetCanMove(true);

    }

    public void ShowLoadingScreen()
    {

        ClearAll();
        loadingScreen.SetActive(true);

    }

    public void ShowCharacterMenu()
    {

        ClearAll();
        //characterMenu.SetActive(true);

    }

    public void ShowOptionsMenu()
    {

        ClearAll();
        optionsMenu.SetActive(true);

    }

    public void ShowInGameOptions()
    {

        ClearAll();
        inGameOptionsMenu.SetActive(true);

    }

    public void ShowGameUI()
    {

        ClearAll();

        if(pauseState.instance.GetPauseState() != PAUSESTATE.UNPAUSED)
        {

            pauseState.instance.SetPauseState(PAUSESTATE.UNPAUSED);

        }

        gameUI.SetActive(true);

    }

    public void ShowPauseMenu()
    {

        ClearAll();

        if(pauseState.instance.GetPauseState() != PAUSESTATE.PAUSED)
        {

            pauseState.instance.SetPauseState(PAUSESTATE.PAUSED);

        }

        pauseUI.SetActive(true);

    }

    public void ShowDeathMenu()
    {

        ClearAll();
        pauseState.instance.SetCanPause(false);
        deathUI.SetActive(true);

    }

    public void ShowAboutMenu()
    {

        ClearAll();
        aboutMenu.SetActive(true);

    }

    public void PlayButtonSound()
    {

        audioManager.instance.PlaySound(buttonSound, SOUNDSOURCE.MENU);

    }

    public void PlayGame()
    {

        //camMove cam = GameObject.Find("Main Camera").GetComponent<camMove>();

        ClearAll();
        
        loadingScreen.SetActive(true);
        trackConstructor.instance.ChangeBiome();
        spawnEnemies.instance.ChangeBiome();

        for(int i = 0; i < 2; i++)
        {

            trackConstructor.instance.SpawnRandomSegment();

        }

        loadingScreen.SetActive(false);
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        //cam.GetPlayer();
        VCamManager.instance.GetPlayer();
        trackConstructor.instance.SetCanMove(true);
        spawnEnemies.instance.SetCanSpawnMinions(true);
        gameUI.SetActive(true);
        pauseState.instance.SetCanPause(true);

    }

    public void ResetGame()
    {

        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
        {

            if (obj.tag != "core" && 
                obj.tag != "MainCamera" && 
                obj.tag != "Canvas" &&
                obj.layer != 5)
            {

                obj.SetActive(false);

                if(obj.GetComponent<minion>())
                {

                    var m = obj.GetComponent<minion>();

                    m.ResetSelf();

                }
                else if(obj.GetComponent<boss>())
                {

                    var b = obj.GetComponent<boss>();

                    b.ResetSelf();

                }

            }

            //Is Below Even Needed??
            //Wouldn't the Children Count as GameObjects for the foreach loop?
            if (obj.transform.childCount > 0)
            {

                for (int i = 0; i < obj.transform.childCount; i++)
                {

                    GameObject child = obj.transform.GetChild(i).gameObject;
                    
                    if (child.tag != "defaultHidden")
                    {

                        child.SetActive(true);

                    }
                    else
                    {

                        child.SetActive(false);

                    }

                }

            }

            if (obj.GetComponent<trackPiece>())
            {

                obj.GetComponent<trackPiece>().Nullify();

            }

            if (obj.tag == "Player")
            {

                Destroy(obj.gameObject);

            }

        }

        trackConstructor.instance.ResetTracks();
        spawnEnemies.instance.ResetEnemies();
        pauseState.instance.SetPauseState(PAUSESTATE.UNPAUSED);
        RemoveBossHealthUI();

        trackConstructor.instance.Nullify();
        spawnEnemies.instance.Nullify();

        Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
        pauseState.instance.SetCanPause(true);

    }

    public void SetAudioSettings()
    {

        if (masterVolSlider)
        {

            masterVolSlider.value = audioManager.instance.masterVolume;
            masterVolSliderInGame.value = audioManager.instance.masterVolume;

        }

        if (BGMVolSlider)
        {

            BGMVolSlider.value = audioManager.instance.BGMVolume;
            BGMVolSliderInGame.value = audioManager.instance.BGMVolume;
            
        }

        if(SFXVolSlider)
        {

            SFXVolSlider.value = audioManager.instance.SFXVolume;
            SFXVolSliderInGame.value = audioManager.instance.SFXVolume;

        }

    }

    public void ShowBossHealthUI()
    {

        bossHealthSlider.gameObject.SetActive(true);

    }

    public void RemoveBossHealthUI()
    {

        bossHealthSlider.gameObject.SetActive(false);
        
    }

    public void SetStats(int minionKills, int bossKills, float runTime)
    {

        this.minionKills.text = "" + minionKills.ToString() + " Minions";
        this.bossKills.text = "" + bossKills.ToString() + " Bosses";

        var minutes = runTime / 60;
        var seconds = runTime % 60;

        runTimeMins.text = string.Format("{0:00} Minutes", minutes);
        runTimeSecs.text = string.Format("{0:00} Seconds", seconds);

        ShowDeathMenu();

    }

    public void QuitGame()
    {

        Application.Quit();

    }

}
