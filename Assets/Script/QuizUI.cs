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
    [SerializeField] private RectTransform scrollHolder, chooseScreenContent, chooseScreenContentR2, chooseScreenOptions;
    [SerializeField] private ChooseBtn chooseBtnPrefab;
    [SerializeField] private AnswerPrefab answerPrefab;
    [SerializeField] private BtnAnswerAudio btnAnswerAudioPrefab;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel, gamePanel,chooseScreen, StartR2, R2End;
    [SerializeField] private TextMeshProUGUI questionText;
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
    public RectTransform ChooseScreenOptions { get => chooseScreenOptions; }
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
        manager.StartGame();
    }

    public void setQuestion(Question question)
    {
        this.question = question;
        questionText.text = question.questionInfo;
        List<Answer> ansOptions = SuffleList.ShuffleListItems<Answer>(question.options);
        setAnswerOption(ansOptions);
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
            if (!answered)
            {
                answered = true;
                bool val = manager.Answer(btn.name);

                if (val)
                {
                    StartCoroutine(BlinkImg(btn.image));
                    audioManager.playAudio(true);
                    btn.image.color = correctCol;
                }
                else
                {
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
    public void setAnswerOption(List<Answer> options)
    {
       
            foreach (Transform child in chooseScreenOptions.transform)
            {
                Destroy(child.gameObject);
            }
        
        for (int i =0; i< options.Count; i++)
        {
            AnswerPrefab answerOp = Instantiate(answerPrefab, chooseScreenOptions.transform);
            answerOp.setOptionBtn(options[i].text, options[i].imgPath);
            answerOp.Btn.name = options[i].text;
            int index = i;
            answerOp.Btn.onClick.AddListener(() => {
                OnClick(answerOp.Btn);
            });
            chooseScreenOptions.sizeDelta = new Vector2(chooseScreenOptions.sizeDelta.x, 20 * -i);
        }
    }
    private void ChooseBtn(int index, string option)
    {

        audioManager.playBtnAudio();
        manager.StartGame();
        chooseScreen.SetActive(false);
        gamePanel.SetActive(true);


    }
    void Update()
    {

    }
    IEnumerator BlinkImg(Image img)
    {
        if (img != null && img.gameObject != null)
        {
            for (int i = 0; i < 2; i++)
            {
                if (img != null) // Kiểm tra lại một lần nữa trước khi thay đổi màu sắc
                {
                    img.color = Color.white;
                    yield return new WaitForSeconds(0.05f);
                    if (img != null) // Kiểm tra lại một lần nữa trước khi thay đổi màu sắc
                    {
                        yield return new WaitForSeconds(0.05f);
                    }
                }
            }
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
