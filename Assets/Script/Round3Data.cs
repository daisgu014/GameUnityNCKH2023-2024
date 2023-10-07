using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WordData", menuName = "Data/Round3", order = 2)]
public class Round3Data: ScriptableObject
{
    [SerializeField] private List<WordData> words;
    [SerializeField] private Sprite bgImage;

    public List<WordData> GetWords()
    {
        return this.words;
    }

    public Sprite getBackgroundImage()
    {
        return this.bgImage;
    }
}

