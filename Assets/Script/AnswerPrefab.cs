using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerPrefab : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] private Image image;
    [SerializeField] Button btn;
    public Button Btn { get => btn; }
    public void setOptionBtn(string title, Sprite imagePath)
    {
        text.text = title;
        if(imagePath != null)
        {
            image.sprite = imagePath;
        }
        
    }

}
