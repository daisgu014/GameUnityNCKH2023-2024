using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionsData", menuName = "QuestionsData", order = 1)]
public class QuizDataScriptable : ScriptableObject
{
    public string chooseOption; // chữ cái
    public Color color; //màu của chữ cái
    public List<Question> questionsList; //danh sách câu hỏi của chữ cái mà bạn chon
}
