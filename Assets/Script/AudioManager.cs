using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip clickBtnAudio;
    public AudioClip CorrectAudio;
    public AudioClip wrongAudio;
    QuizManager quizManager;
    // Update is called once per frame
    void Start()
    {

    }
    public void playBtnAudio()
    {
        audioSource.PlayOneShot(clickBtnAudio);
    }

    public void playAudio(bool answer)
    {
        audioSource.Stop();
        if(answer)
        {
            audioSource.PlayOneShot(CorrectAudio);
        }
        else
        {
            audioSource.PlayOneShot(wrongAudio);
        }
    }
    public void playQuestionAu(AudioClip questionAu)
    {
        audioSource.PlayOneShot(questionAu);
    }
    public void stopAudito()
    {
      audioSource.Stop();
    }
}
