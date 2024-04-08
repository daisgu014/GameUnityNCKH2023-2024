using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Word : MonoBehaviour
{

    private string word;
    public IndexWord current;
    private Sprite img;
    private bool isCorrect = false;

    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private TextMeshProUGUI textRender;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip correctAudio;
    [SerializeField] private AudioClip incorrectAudio;


    // Start is called before the first frame update
    void Start()
    {
        renderer.sprite = img;
        textRender.text = word;
    }

    public void setData(WordData data)
    {
        this.word = data.word;
        this.img = data.img;
        this.isCorrect = data.isCorrect;
        this.correctAudio = data.wordVoice;
    }

    public void setCharIndex(IndexWord indexWord)
    {
        current = indexWord;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log(current.Value);
        if (isCorrect) {
            Debug.Log("Hay quá pé ơi");
            audioSource.PlayOneShot(correctAudio);
        }
        else
        {
            Debug.Log("Sai rồi pé ơi");
            audioSource.PlayOneShot(incorrectAudio);
        }
        
        
    }
}
