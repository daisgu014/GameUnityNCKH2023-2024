using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    public Button startBtn;
    public Button exitBtn;
    
    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(() => loadScene("CharBoard"));
        exitBtn.onClick.AddListener(() => exitGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    void exitGame()
    {
        Application.Quit();
    }
}
