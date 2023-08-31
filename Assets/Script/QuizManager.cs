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
    private List<Category> categories;
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
    public List<Category> Categories
    {
        get { return categories; }
    }
    public string CurrentCategory
    {
        get { return currentCategory; }
    }
    public List<Category> StartChoose(int optionIndex, string option)
    {
        CurrentOption = option;
        currentInt = optionIndex;
        categories = new List<Category>();
        dataScriptable = quizDataList[optionIndex];
        categories.AddRange(dataScriptable.categoriesList);
        gameStatus = GameStatus.PLAYING;
        return categories;

    }
    public void StartGame(int categoryIndex, string category)
    {
       
        currentCategory = category;
        Debug.Log(currentInt);
        correctAnswerCount = 0;
        gameScore = 0;
        quesitons = new List<Question>();
        dataScriptable = quizDataList[currentInt];
        for(int i = 0; i < dataScriptable.questionsList.Count; i++)
            if (dataScriptable.questionsList[i].category.categoryName.Equals(currentCategory))
            {
                quesitons.Add(dataScriptable.questionsList[i]);
            }
       Debug.Log(quesitons.Count);
        if (quesitons.Count > 0)
        {
            SelectQuestion();
        }
       
        gameStatus = GameStatus.PLAYING;


    }
    void SelectQuestion()
    {
        int val = UnityEngine.Random.Range(0, quesitons.Count);
        selectedQuestion = quesitons[val];
        quizUI.setQuestion(selectedQuestion);
        quesitons.RemoveAt(val);

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

        }
        else
        {
            //No, Ans is wrong
            //Reduce Life
            lifeRemaining--;
           // quizUI.ReduceLife(lifeRemaining);

            if (lifeRemaining == 0)
            {
                GameEnd();
            }
        }

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
    public Category category;
    public List<string> options;
    public string correctAns;

}
[System.Serializable]
public class Category
{
    public string categoryName;
    public Sprite imgPath;
}

[SerializeField]
public enum GameStatus
{
    PLAYING,
    NEXT
}
