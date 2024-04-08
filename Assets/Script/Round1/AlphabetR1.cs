using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> Onlick)
    {
        button.onClick.AddListener(delegate () {
            Onlick(param);
        });
    }
}
public class AlphabetR1 : MonoBehaviour
{
    [SerializeField] private GameObject word;
    [SerializeField] private GameObject sceneAlphabet;
    [SerializeField] IndexWord Index;
    // Start is called before the first frame update
    void Start()
    {
        GameObject button = word.transform.gameObject;
        GameObject g;

        string vietnameseAlphabet = "AĂÂBCDĐEÊGHIKLMNOÔƠPQRSTUƯVWXY";
        int i = 0;
        foreach (char character in vietnameseAlphabet)
        {
            g = Instantiate(button, transform);
            g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = character.ToString();
            g.GetComponent<Button>().AddEventListener(i, setActive);
            i++;
        }

        /*Destroy(button);*/
    }

    // Update is called once per frame
    void Update()
    {

    }
    void setActive(int i)
    {
        Index.Value = i;
        SceneManager.LoadScene("Round1");
    }
}
