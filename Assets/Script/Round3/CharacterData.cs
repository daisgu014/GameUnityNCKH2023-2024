using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Data/Round3Character", order = 2)]
public class CharacterData: ScriptableObject
{
    [SerializeField] private List<WordData> words;
    [SerializeField] private Sprite bgImage;
    [SerializeField] private AudioClip introVoice;
    [SerializeField] private int required = 1;

    public AudioClip IntroVoice { get => introVoice; set => introVoice = value; }
    public int Required { get => required; set => required = value; }

    public List<WordData> GetWords()
    {
        return this.words;
    }

    public Sprite getBackgroundImage()
    {
        return this.bgImage;
    }
}

