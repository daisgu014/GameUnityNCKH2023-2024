
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Round3 : MonoBehaviour
{
    [SerializeField] private Round3Data data;
    [SerializeField] private GameObject wordGO;
    [SerializeField] private GameObject wordHolder;
    [SerializeField] private AudioSource voiceControl;
    [SerializeField] private AudioClip startVoice;
    [SerializeField] private AudioClip endVoice;
    [SerializeField] private Sprite endImage;
    private List<WordData> words;
    private CharacterData currentCharacter;
    public IndexWord indexWord;
    private Sprite bgImage;
    private HashSet<string> currentChoice;

    public HashSet<string> CurrentChoice { get => currentChoice; set => currentChoice = value; }
    public AudioSource VoiceControl { get => voiceControl; set => voiceControl = value; }


    // Start is called before the first frame update
    void Start()
    {
        CurrentChoice = new HashSet<string>();
        currentCharacter = data.GetCharacter(indexWord.Value);
        VoiceControl.PlayDelayed((float)0.5);
        VoiceControl.PlayOneShot(clip: startVoice);
        StartCoroutine(WaitForStart(startVoice));

    }

    private IEnumerator WaitForStart(AudioClip Sound)
    {
        yield return new WaitUntil(() => VoiceControl.isPlaying == false);
        start();
    }

    private void start()
    {
        bgImage = currentCharacter.getBackgroundImage();
        renderBackground();
        VoiceControl.PlayDelayed((float)0.5);
        VoiceControl.PlayOneShot(clip: currentCharacter.IntroVoice);
        StartCoroutine(WaitForIntro(currentCharacter.IntroVoice));
    }

    private IEnumerator WaitForIntro(AudioClip Sound)
    {
        yield return new WaitUntil(() => VoiceControl.isPlaying == false);
        play();
    }


    private void play()
    {
        
        words = currentCharacter.GetWords();
        wordHolder.SetActive(true);
        renderWordList();
    }

    private void renderBackground()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = bgImage;
        Debug.Log(bgImage.rect.size);
    }

    private void renderWordList()
    {
        for (int i = 0; i < words.Count; i++)
        {
            GameObject tmp = Instantiate(wordGO);
            tmp.name = "word" + i;
            tmp.GetComponent<Word>().setData(words[i]);
            tmp.GetComponent<Word>().setCharIndex(indexWord);
            tmp.GetComponent<Word>().Controller = wordHolder.transform.parent.GetComponent<Round3>();
            tmp.transform.position = new Vector3(
                words[i].X,
                words[i].Y*(-1f)
                );
            tmp.transform.SetParent(wordHolder.transform);
            
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckEndGame(AudioSource source)
    {
        if (currentChoice.Count >= currentCharacter.Required)
        {
            StartCoroutine(WaitForEnd(source));
        }
    }

    private IEnumerator WaitForEnd(AudioSource source)
    {
        yield return new WaitUntil(() => source.isPlaying == false);
        wordHolder.SetActive(false);
        VoiceControl.PlayDelayed((float)0.5);
        VoiceControl.PlayOneShot(clip: endVoice);
        gameObject.GetComponent<SpriteRenderer>().sprite = endImage;
        yield return new WaitUntil(() => VoiceControl.isPlaying == false);
        SceneManager.LoadScene("CharBoard");
    }
}
