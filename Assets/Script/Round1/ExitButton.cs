using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject game = GameObject.Find("Exit");
        if (game != null)
        {
            game.GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene("StartGame");
            });
        }
        else
        {
            Debug.LogError("Không thể tìm thấy đối tượng trong MainCanvas!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
