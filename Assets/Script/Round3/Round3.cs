
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
    private List<WordData> words;
    private CharacterData currentCharacter;
    public IndexWord indexWord;
    private Sprite bgImage;
    private HashSet<string> currentChoice;

    public HashSet<string> CurrentChoice { get => currentChoice; set => currentChoice = value; }


    // Start is called before the first frame update
    void Start()
    {
        CurrentChoice = new HashSet<string>();
        currentCharacter = data.GetCharacter(indexWord.Value);
        voiceControl.PlayDelayed((float)0.5);
        voiceControl.PlayOneShot(clip: currentCharacter.IntroVoice);
        StartCoroutine(WaitForSound(currentCharacter.IntroVoice));
        Debug.Log(indexWord.Value);
        words = currentCharacter.GetWords();
        bgImage = currentCharacter.getBackgroundImage();
        renderBackground();

    }

    private IEnumerator WaitForSound(AudioClip Sound)
    {
        yield return new WaitUntil(() => voiceControl.isPlaying == false);
        // or yield return new WaitWhile(() => audiosource.isPlaying == true);
        play();
    }



    private void play()
    {
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
        if(currentChoice.Count >= currentCharacter.Required)
        {
            SceneManager.LoadScene("CharBoard");
        }
    }
}
