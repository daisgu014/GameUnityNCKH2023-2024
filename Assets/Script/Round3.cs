
using System.Collections.Generic;
using UnityEngine;

public class Round3 : MonoBehaviour
{
    public Round3Data data;
    private List<WordData> words;
    [SerializeField] private GameObject wordGO;
    [SerializeField] private GameObject wordHolder;
    private Sprite bgImage;
    // Start is called before the first frame update
    void Start()
    {
        this.words = data.GetWords();
        this.bgImage = data.getBackgroundImage();
        renderBackground();
        renderWordList();

    }

    private void renderBackground()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = bgImage;
        Debug.Log(bgImage.rect.size);
    }

    private void renderWordList()
    {

        Vector3 rootPosition = new Vector3(-.6f, .3f);
        rootPosition *= 15;
        for (int i = 0; i < words.Count; i++)
        {
            GameObject tmp = Instantiate(wordGO);
            tmp.name = "word" + i;
            tmp.GetComponent<Word>().setData(words[i]);
            tmp.transform.position = rootPosition + new Vector3(words[i].X*5,words[i].Y*(-1f)*5);
            tmp.transform.SetParent(wordHolder.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
