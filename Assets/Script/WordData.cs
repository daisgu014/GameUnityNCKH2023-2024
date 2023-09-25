using System;
using UnityEngine;



[CreateAssetMenu(fileName = "WordData", menuName = "Data/Round3", order = 1)]
public class WordData: ScriptableObject
{

    public string word;
    public Sprite img;
    public bool isCorrect = false;
}

