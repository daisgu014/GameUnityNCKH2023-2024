using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBtn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Button btn;
    public Button Btn { get => btn; }
    public void setOptionBtn(string title, Color color)
    {
        text.text = title;
        text.color = color;
    }

}
