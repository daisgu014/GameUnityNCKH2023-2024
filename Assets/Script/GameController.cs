using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Button startBtn;
    
    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(() => loadScene("CharBoard"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
