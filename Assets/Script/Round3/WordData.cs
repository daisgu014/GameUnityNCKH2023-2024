using System;
using UnityEngine;



[CreateAssetMenu(fileName = "WordData", menuName = "Data/Round3Word", order = 1)]
public class WordData: ScriptableObject
{
    public string word;
    public Sprite img;
    public bool isCorrect = false;
    public AudioClip wordVoice;

    public float X = 1;
    public float Y = 1;
}

