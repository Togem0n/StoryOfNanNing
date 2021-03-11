using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button loadButton;
    private void Start()
    {
        loadButton.onClick.AddListener(loadButtonClicked);
    }

    void loadButtonClicked()
    {
        //Debug.Log("MainMenuClicked");
        Loader.Load(Loader.Scene.Home);
    }
}
