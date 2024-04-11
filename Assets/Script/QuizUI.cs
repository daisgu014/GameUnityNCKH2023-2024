using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{

    [SerializeField] QuizManager manager;
    [SerializeField] AudioManager audioManager;
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] private RectTransform scrollHolder, chooseScreenContent, scrollHoderEndR2, chooseScreenContentR2;
    [SerializeField] private ChooseBtn chooseBtnPrefab;
    [SerializeField] private BtnAnswerAudio btnAnswerAudioPrefab;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel, gamePanel,chooseScreen, StartR2, R2End;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private List<Button> options;
    [SerializeField] private Color correctCol, wrongCol;
    [SerializeField] public AudioClip questionAudio,endAudio;

    private Question question;
    private bool answered;
    private float audioLength;
    public TextMeshProUGUI ScoreText { get => scoreText; }
    public BtnAnswerAudio BtnAnswerAudioPrefab { get => btnAnswerAudioPrefab; }
    public RectTransform ScrollHoderEndR2 { get => chooseScreenContentR2; }
    public AudioManager AudioManager { get => audioManager; }
    public GameObject GmeOverPanel { get => gameOverPanel; }
    public GameObject GamePanel { get => gamePanel; }
    public GameObject R2EndObbject { get => R2End; }
    public AudioClip EndAudio { get => endAudio; }
    private void Start()
    {

        playAudioInfo();

    }
    public void playAudioInfo()
    {
        AudioManager.playQuestionAu(questionAudio);
    }
    public void playEnd2()
    {
        AudioManager.playQuestionAu(endAudio);
    }
    public void loadSenceR1()
    {
        SceneManager.LoadScene("Round1");
    }
    public void changeGameChooice()
    {
        AudioManager.stopAudito();
        StartR2.SetActive(false);
        gamePanel.SetActive(true);
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => {
                OnClick(localBtn);
            });
        }
        manager.StartGame();
    }

    public void setQuestion(Question question)
    {
        //questionCounter.text = $"0/{questions.Count.ToString()}";
        this.question = question;
        questionText.text = question.questionInfo;
        List<string> ansOptions = SuffleList.ShuffleListItems<string>(question.options);

        if (question.options.Count > 0)
        {
            for (int i = 0; i < options.Count; i++)
            {
                options[i].GetComponentInChildren<TextMeshProUGUI>().text = ansOptions[i];
                options[i].name = ansOptions[i];
            }
        }
        answered = false;


    }
    public void playAgianQuestionAu()
    {
        audioManager.playQuestionAu(this.question.questionAudio);
    }
    public void ReduceLife(int remainingLife)
    {
        lifeImageList[remainingLife].color = Color.red;
    }
    private void OnClick(Button btn)
    {

        if (manager.GameStatus == GameStatus.PLAYING)
        {
            //if answered is false
            if (!answered)
            {
                //set answered true
                answered = true;
                //get the bool value
                bool val = manager.Answer(btn.name);
                

                //if its true
                if (val)
                {
                    //set color to correct
                   
                    StartCoroutine(BlinkImg(btn.image));
                    audioManager.playAudio(true);
                    btn.image.color = correctCol;
                }
                else
                {
                    //else set it to wrong color
                  
                    StartCoroutine(BlinkImg(btn.image));
                    audioManager.playAudio(false);
                    btn.image.color = Color.red;
                }
            }
        }
    }
    void CreateOptionButtons()
    {
        for(int i=0; i< manager.QuizData.Count; i++)
        {
            ChooseBtn categoryBtn = Instantiate(chooseBtnPrefab, chooseScreenContent);
            categoryBtn.setOptionBtn(manager.QuizData[i].chooseOption, manager.QuizData[i].color);
            int index = i;
            categoryBtn.Btn.onClick.AddListener(()=>ChooseBtn(index, manager.QuizData[index].chooseOption));

        }
    }
    public void loadSence3()
    {
        SceneManager.LoadScene("Round3");
    }
    public void loadSenceStart()
    {
        SceneManager.LoadScene("StartGame");
    }
    //void CreateCategoryButtons()
    //{

    //    //we loop through all the available catgories in our QuizManager
    //        for (int i = 0; i < manager.Categories.Count; i++)
    //        { 
    //        //Create new CategoryBtn
    //            CategoryBtnScript categoryBtn = Instantiate(categoryBtnPrefab, scrollHolder.transform);
    //        //Set the button default values
    //        categoryBtn.SetButton(manager.Categories[i].categoryName, manager.Categories[i].imgPath);
    //            int index = i;
    //            //Add listner to button which calls CategoryBtn method
    //            categoryBtn.Btn.onClick.AddListener(() => CategoryBtn(index, manager.Categories[index].categoryName));

    //           //scrollHolder.sizeDelta = new Vector2(scrollHolder.sizeDelta.x, 20 * -i); 
    //    }

    //}\

    public void EndScreen()
    {
        gamePanel.SetActive(false);
        R2EndObbject.SetActive(true);
        QuizDataScriptable dataScriptable = manager.dataIndex();
        for (int i=0; i< dataScriptable.questionsList.Count; i++)
        {
            BtnAnswerAudio btnAnswer = Instantiate(btnAnswerAudioPrefab, scrollHoderEndR2.transform);
            btnAnswer.setOptionBtn(dataScriptable.questionsList[i].correctAns);
            btnAnswer.Btn.onClick.AddListener(()=>{ AudioManager.playQuestionAu(dataScriptable.questionsList[i].correctAnsAudio); });
            scrollHolder.sizeDelta = new Vector2(scrollHolder.sizeDelta.x, 20 * -i);
        }
    }
    private void ChooseBtn(int index, string option)
    {

        audioManager.playBtnAudio();
        manager.StartGame();
        chooseScreen.SetActive(false);
        gamePanel.SetActive(true);


    }
    //private void CategoryBtn(int index, string category)
    //{
    //    audioManager.playBtnAudio();
    //    manager.StartGame(index, category);
    //    mainMenu.SetActive(false);
    //    gamePanel.SetActive(true);
    //}
    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator BlinkImg(Image img)
    {
        for (int i = 0; i < 2; i++)
        {
            img.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void RestryButton()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        audioManager.playBtnAudio();
    }
    public void BackToChooseMenu()
    {

        SceneManager.LoadScene(chooseScreen.scene.buildIndex);
        audioManager.playBtnAudio();
    }
    public void StopAudio()
    {
        audioManager.stopAudito();
    }
}
