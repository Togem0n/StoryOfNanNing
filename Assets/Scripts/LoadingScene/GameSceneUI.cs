using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    public Button mainMenuButton;
    private void Start()
    {
        mainMenuButton.onClick.AddListener(mainMenuClicked);
    }

    void mainMenuClicked()
    {
        //Debug.Log("MainMenuClicked");
        Loader.Load(Loader.Scene.MainMenu);
    }
}
