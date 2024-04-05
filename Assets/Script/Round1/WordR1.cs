using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu]
public class WordR1 : ScriptableObject
{
    public string word;
    public VideoClip clip;
    /*public VideoPlayer videoPlayer;*/
    public RenderTexture texture;
    public AudioClip uppercaseSound;
    public AudioClip chooseWordSound;
    public AudioClip lowercaseSound;
    public AudioClip chooseWordUpper;
    public AudioClip chooseWordLower;
}
