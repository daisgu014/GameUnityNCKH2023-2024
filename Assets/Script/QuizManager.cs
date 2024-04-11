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
    public IndexWord indexWord;
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
        Debug.Log("Thứ tự câu hỏi: " + indexWord.Value);
        correctAnswerCount = 0;
        gameScore = 0;
        quesitons = new List<Question>();
        dataScriptable = quizDataList[indexWord.Value];
        for (int i = 0; i < dataScriptable.questionsList.Count; i++)
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
    public QuizDataScriptable dataIndex(){
            return quizDataList[indexWord.Value];
        }

Question SelectQuestion()
    {
        
        int val = UnityEngine.Random.Range(0, quesitons.Count);
        selectedQuestion = quesitons[val];
        currentQues = selectedQuestion;
  
        quesitons.RemoveAt(val);
        quizUI.AudioManager.playQuestionAu(selectedQuestion.questionAudio);
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
        if (selectedQuestion.correctAns.ToLower() == answerd.ToLower())
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
                    Invoke("SelectQuestion", 2);

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
    
    public string questionInfo;
    public AudioClip questionAudio;// câu hỏi
    public List<string> options; // danh sách câu trả lời
    public string correctAns;// đáp án đúng
    public AudioClip correctAnsAudio;

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
