using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Round3Data", menuName = "Data/Round3DataList", order = 3)]
public class Round3Data : ScriptableObject
{
    [SerializeField] private CharacterData[] data;

   
    public CharacterData GetCharacter(int indexWord)
    {
        return data[indexWord];
    }
}