using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(()=>doAction("hello from the outside"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void doAction(string aaa)
    {
        Debug.Log(aaa);
    }
}
