using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadGame : MonoBehaviour {

    private void Start()
    {

        InitializeGame();

    }

    void InitializeGame()
    {

        pauseState.instance.SetCanPause(false);
        menuManager.instance.ShowLoadingScreen();
        trackConstructor.instance.Initialize();
        spawnEnemies.instance.Initialize();

        menuManager.instance.ShowMainMenu();

    }

}
