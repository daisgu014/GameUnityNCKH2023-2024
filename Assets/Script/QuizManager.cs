using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class QuizManager : MonoBehaviour

{
    [SerializeField]
    private QuizUI quizUI;
    [SerializeField]
    private List<QuizDataScriptable> quizDataList;
    private Question selectedQuestion = new Question();
    private string CurrentOption = "";
    private int currentInt;
    private Question currentQues = new Question();
    //private List<Category> categories;
    private string currentCategory = "";
    private List<Question> quesitons;
    private int correctAnswerCount = 0;
    private int gameScore;
    private int lifeRemaining;
    private QuizDataScriptable dataScriptable;
    private GameStatus gameStatus = GameStatus.NEXT;
    public GameStatus GameStatus
    {
        get { return gameStatus; }
    }
    public List<QuizDataScriptable> QuizData
    {
        get
        {
            return quizDataList;
        }
    }
    //public List<Category> Categories
    //{
    //    get { return categories; }
    //}
    public string CurrentCategory
    {
        get { return currentCategory; }
    }
    //public List<Category> StartChoose(int optionIndex, string option)
    //{
    //    CurrentOption = option;
    //    currentInt = optionIndex;
    //    categories = new List<Category>();
    //    dataScriptable = quizDataList[optionIndex];
    //    categories.AddRange(dataScriptable.categoriesList);
    //    gameStatus = GameStatus.PLAYING;
    //    return categories;

    //}
    public void StartGame()
    {
       
        //currentCategory = category;
       // Debug.Log(currentInt);
        correctAnswerCount = 0;
        gameScore = 0;
        quesitons = new List<Question>();
        dataScriptable = quizDataList[currentInt];
        for(int i = 0; i < dataScriptable.questionsList.Count; i++)
            {
                quesitons.Add(dataScriptable.questionsList[i]);
            }
      // Debug.Log(quesitons.Count);
        if (quesitons.Count > 0)
        {
           SelectQuestion();
        }
       
        gameStatus = GameStatus.PLAYING;


    }
    Question SelectQuestion()
    {
        int val = UnityEngine.Random.Range(0, quesitons.Count);
        selectedQuestion = quesitons[val];
        currentQues = selectedQuestion;
        quesitons.RemoveAt(val);
        quizUI.setQuestion(selectedQuestion);
        return selectedQuestion;

    }
    void selectQuestionAgian(Question val)
    {
        quizUI.setQuestion(val);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
 
    public bool Answer(string answerd)

    {
        bool correct = false;
        //if selected answer is similar to the correctAns
        if (selectedQuestion.correctAns == answerd)
        {
            //Yes, Ans is correct
            correctAnswerCount++;
            correct = true;
            gameScore += 5;
            quizUI.ScoreText.text = "Điểm:" + gameScore;
            if (gameStatus == GameStatus.PLAYING)
            {
                if (quesitons.Count > 0)
                {
                    //call SelectQuestion method again after 1s
                    Invoke("SelectQuestion", 0.4f);

                }
                else
                {
                    GameEnd();
                }
            }
        }
        else
        {
            //No, Ans is wrong
            //Reduce Life
            lifeRemaining--;
            // quizUI.ReduceLife(lifeRemaining);

            selectQuestionAgian(currentQues);
            if (lifeRemaining == 0)
            {
                GameEnd();
            }
        }

       
        //return the value of correct bool
        return correct;
    }

    private void GameEnd()
    {
        gameStatus = GameStatus.NEXT;
        quizUI.GameOverPanel.SetActive(true);
        PlayerPrefs.SetInt(currentCategory, correctAnswerCount); //save the score for this category
    }
}
[System.Serializable]
public class Question
{
    
    public string questionInfo; // câu hỏi
    public List<string> options; // danh sách câu trả lời
    public string correctAns; // đáp án đúng

}
[System.Serializable]

//------------------------------------BỎ-----------------------------------------
//public class Category
//{
//    public string categoryName;
//    public Sprite imgPath;
//}
//----------------------------------------------------------------------------------

[SerializeField]
//-------------------------------------Trạng thái trò chơi-----------------------------
public enum GameStatus
{
    PLAYING,
    NEXT
}
