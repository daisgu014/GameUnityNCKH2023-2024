using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Word : MonoBehaviour
{

    private string word;
    public IndexWord current;
    private Sprite img;
    private Image image;
    private bool isCorrect = false;
    private Round3 controller;
    private bool clicked = false;

    private Vector3 minScale;
    private Vector3 maxScale;
    private bool repeatFlag = true;
    private float scalingDuration = 1;

    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private TextMeshProUGUI textRender;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip correctAudio;
    [SerializeField] private AudioClip incorrectAudio;
    [SerializeField] private Button button;
    [SerializeField] private RectTransform transformer;

    public bool IsCorrect { get => isCorrect; set => isCorrect = value; }
    public Round3 Controller { get => controller; set => controller = value; }


    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() =>
            OnMouseDown()
        );
        renderer.sprite = img;
        textRender.text = word;
       
    }

    public void setData(WordData data)
    {
        this.word = data.word;
        this.img = data.img;
        this.IsCorrect = data.isCorrect;
        this.correctAudio = data.wordVoice;
        minScale = new Vector2(0.2302357f, 0.5153641f);
        maxScale = minScale * new Vector2(1.5f, 1.5f);
    }

    public void setCharIndex(IndexWord indexWord)
    {
        current = indexWord;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnMouseDown()
    {
        if (IsCorrect) {
            repeatFlag = true;
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(correctAudio);
            }
            while (repeatFlag)
            {
                yield return RepeatLerping(minScale, maxScale, scalingDuration);
                yield return RepeatLerping(maxScale, minScale, scalingDuration);
            }
            controller.CurrentChoice.Add(word);
        }
        else
        {
            if (!clicked)
            {
                
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(incorrectAudio);
                    while (repeatFlag)
                    {
                        yield return RepeatLerping(minScale, new Vector2(0f,0f), scalingDuration);
                    }
                    button.gameObject.SetActive(false);
                    renderer.enabled = false;
                }
                clicked = true;
            }
            
        }

        controller.CheckEndGame(audioSource);
    }

    IEnumerator RepeatLerping(Vector3 startScale, Vector3 endScale, float time)
    {
        float t = 0.0f;
        float rate = (1f / time) * 3;
        while (t < 1f)
        {
            t += Time.deltaTime * rate;
            transformer.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
        repeatFlag = false;
    }

}
