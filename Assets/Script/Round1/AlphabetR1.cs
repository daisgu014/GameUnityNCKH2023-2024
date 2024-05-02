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

        string vietnameseAlphabet = "AĂÂBCDĐEÊGHIKLMNOÔƠPQRSTUƯVXY";
        int i = 0;
        List<Color> colors = GenerateRandomColors(vietnameseAlphabet.Length);
        foreach (char character in vietnameseAlphabet)
        {
            g = Instantiate(button, transform);
            g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = character.ToString();
            g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colors[i];
            g.GetComponent<Button>().AddEventListener(i, setActive);
            i++;
        }

        /*Destroy(button);*/
    }
    List<Color> GenerateRandomColors(int count)
    {
        List<Color> randomColors = new List<Color>();
        for (int i = 0; i < count; i++)
        {
            randomColors.Add(new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value));
        }
        return randomColors;
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
