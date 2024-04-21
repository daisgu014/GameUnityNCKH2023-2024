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
   
    public string CurrentCategory
    {
        get { return currentCategory; }
    }
    public void StartGame()
    {
        correctAnswerCount = 0;
        gameScore = 0;
        quesitons = new List<Question>();
        dataScriptable = quizDataList[indexWord.Value];
        for (int i = 0; i < dataScriptable.questionsList.Count; i++)
        {
            quesitons.Add(dataScriptable.questionsList[i]);
        }
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
        if (selectedQuestion.correctAns.ToLower() == answerd.ToLower())
        {
            correctAnswerCount++;
            correct = true;
            gameScore += 5;
            quizUI.ScoreText.text = "Điểm:" + gameScore;
            if (gameStatus == GameStatus.PLAYING)
            {
                if (quesitons.Count > 0)
                {
                    Invoke("SelectQuestion", 2);

                }
                else
                {
                    Invoke("GameEnd", 2f);
                }
            }
        }
        else
        {

            lifeRemaining--;
            selectQuestionAgian(currentQues);
            if (lifeRemaining == 0)
            {
                Invoke("GameEnd", 2.5f);
            }
        }

        return correct;
    }

    private void GameEnd()
    {
        gameStatus = GameStatus.NEXT;
        quizUI.GamePanel.SetActive(false);
        quizUI.R2EndObbject.SetActive(true);
        quizUI.AudioManager.playQuestionAu(quizUI.EndAudio);
        QuizDataScriptable dataScriptable = dataIndex();
        for (int i = 0; i < dataScriptable.questionsList.Count; i++)
        {
            BtnAnswerAudio btnAnswer = Instantiate(quizUI.BtnAnswerAudioPrefab, quizUI.ScrollHoderEndR2.transform);
            btnAnswer.setOptionBtn(dataScriptable.questionsList[i].correctAns);
            int index = i;
            btnAnswer.Btn.onClick.AddListener(() => { quizUI.AudioManager.playQuestionAu(dataScriptable.questionsList[index].correctAnsAudio); });
            quizUI.ScrollHoderEndR2.sizeDelta = new Vector2(quizUI.ScrollHoderEndR2.sizeDelta.x, 20 * -i);
        }
        PlayerPrefs.SetInt(currentCategory, correctAnswerCount); //save the score for this category
    }
}
[System.Serializable]
public class Question
{
    
    public string questionInfo;
    public AudioClip questionAudio;// câu hỏi
    public List<Answer> options; // danh sách câu trả lời
    public string correctAns;// đáp án đúng
    public AudioClip correctAnsAudio;

}
[System.Serializable]

public class Answer
{
    public string text;
    public Sprite imgPath;
}

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
