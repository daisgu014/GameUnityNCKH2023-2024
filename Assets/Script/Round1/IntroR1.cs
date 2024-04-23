using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroR1 : MonoBehaviour
{
    public AudioClip audioClip;
    public GameObject game;
    public GameObject round1;
    private Button next;
    private Button exit;
    // Start is called before the first frame update
    void Start()
    {
        next = gameObject.transform.GetChild(0).GetComponent<Button>();
        exit = gameObject.transform.GetChild(1).GetComponent<Button>();
        next.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            game.SetActive(true);
            round1.SetActive(true);
        });
        exit.onClick.AddListener(() =>
        {
            ChangeScene();
        });
    }
     void ChangeScene()
    {
        SceneManager.LoadScene("StartGame");
    }
    void Update()
    {
        
    }
}
