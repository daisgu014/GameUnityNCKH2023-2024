
using System.Collections.Generic;
using UnityEngine;

public class Round3 : MonoBehaviour
{

    [SerializeField] private List<WordData> words;
    [SerializeField] private GameObject wordGO;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<words.Count; i++)
        {
            GameObject tmp = Instantiate(wordGO);
            tmp.name = "word" + i;
            tmp.GetComponent<Word>().setData(words[i]);
            tmp.transform.position = wordGO.transform.position + new Vector3(5 * (i%3), -5 * ((int)i / 3));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
