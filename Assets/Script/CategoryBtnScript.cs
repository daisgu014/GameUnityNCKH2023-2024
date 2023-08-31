using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryBtnScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI categoryTitleText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button btn;
    [SerializeField] private Image image;

    public Button Btn { get => btn; }

    public void SetButton(string title, Sprite imgPath)
    {
        categoryTitleText.text = title;
      //  scoreText.text = PlayerPrefs.GetInt(title, 0) + "/" + totalQuestion; //we get the score save for this category
        image.sprite = imgPath;
    }
}
