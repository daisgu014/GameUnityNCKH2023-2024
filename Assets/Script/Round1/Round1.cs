﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Unity;
public class Round1 : MonoBehaviour
{
    [SerializeField] List<WordR1> words;
    public IndexWord index;
    public GameObject success;
    public GameObject fail;

    private Button next;
    private Button undo;
    private Button home;

    private Transform pn1; 
    private Transform pn2;
    private Transform pn3;
    private Transform pn4;
    private Transform pn5;

    private int page;
    void Start()
    {
        page = 1;
        setGameObject();
        btnNext();
        btnUndo();
        btnHome();
        MouthShape();
        discWord();
        findWord();
        findUppercase();
        findLowercase();
        /*findWord();*/
    }

    void Update()
    {
        btnPauseVideo();
        btnPlayVideo();
        
    }
    
    public Color GetRandomColor()
    {
        Color[] colors = {
            new Color(0.5f, 0.75f, 1.0f),
            new Color(0.0f, 1.0f, 0.0f), // xanh
            new Color(1.0f, 0.0f, 0.0f), // đỏ
            new Color(1.0f, 1.0f, 0.0f), // vàng
            new Color(1.0f, 0.5f, 0.0f), // cam
            new Color(0.0f, 1.0f, 0.0f), // lục
            new Color(1.0f, 0.0f, 1.0f),  // hồng
            new Color(0.0f, 0.5f, 1.0f)
        };

        return colors[UnityEngine.Random.Range(0, colors.Length)];
    }
    void setGameObject()
    {
        next = gameObject.transform.GetChild(0).GetComponent<Button>();
        undo = gameObject.transform.GetChild(1).GetComponent<Button>();
        home = gameObject.transform.GetChild(9).GetComponent<Button>();
        pn1 = gameObject.transform.GetChild(2);
        pn2 = gameObject.transform.GetChild(3);
        pn3 = gameObject.transform.GetChild(4);
        pn4 = gameObject.transform.GetChild(5);
        pn5 = gameObject.transform.GetChild(6);
    }
    void MouthShape()
    {
        int i = index.Value;
        Transform videoPn1 = pn1.GetChild(0);
        /*videoPn1.GetChild(1).GetComponent<VideoPlayer>().clip = words[i].clip;
        videoPn1.GetChild(1).GetComponent<VideoPlayer>().targetTexture = words[i].texture;
        videoPn1.GetChild(0).GetComponent<RawImage>().texture = words[i].texture;*/
        pn1.GetChild(2).GetComponent<TextMeshProUGUI>().text = words[i].word.ToUpper();
        pn1.GetChild(3).GetComponent<TextMeshProUGUI>().text = words[i].word.ToLower(); 
        pn1.GetChild(2).GetComponent<TextMeshProUGUI>().color = GetRandomColor();
        pn1.GetChild(3).GetComponent<TextMeshProUGUI>().color = GetRandomColor();
        pn1.GetChild(5).GetComponent<AudioSource>().clip = words[i].introWord;
        pn1.GetChild(5).GetComponent<AudioSource>().Play();
    }
    void discWord()
    {
        int i = index.Value;
        pn2.GetChild(0).GetComponent<TextMeshProUGUI>().text = words[i].word.ToUpper();
        pn2.GetChild(1).GetComponent<TextMeshProUGUI>().text = words[i].word.ToLower();
        pn2.GetChild(0).GetComponent<TextMeshProUGUI>().color = GetRandomColor();
        pn2.GetChild(1).GetComponent<TextMeshProUGUI>().color = GetRandomColor();
        Transform audioUp = pn2.GetChild(2);
        Transform audioLow = pn2.GetChild(3);
        audioUp.GetChild(2).GetComponent<AudioSource>().clip = words[i].uppercaseSound;
        audioLow.GetChild(2).GetComponent<AudioSource>().clip = words[i].lowercaseSound;
        audioUp.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            audioUp.GetChild(2).GetComponent<AudioSource>().Play();
        });
        audioLow.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            audioLow.GetChild(2).GetComponent<AudioSource>().Play();
        });
    }
    void findWord()
    {
        int i = index.Value;
        GameObject btn = pn3.GetChild(0).GetChild(0).GetComponent<Button>().gameObject;
        GameObject g;
        Vector3 startPosition = pn3.GetChild(0).GetChild(0).position;
        List<char> randomLetters = GenerateRandomLetters(5);
        System.Random random = new System.Random();
        int randomWord = random.Next(0, 4);
        for (int j = 0; j < 5; j++)
        {
            Vector3 position = startPosition + new Vector3(j * 300, 0, 0);

            g = Instantiate(btn, position, Quaternion.identity, pn3.GetChild(0).transform);
            if (j == randomWord)
            {
                g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = words[i].word.ToString();
            }
            else
            {
                g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = randomLetters[j].ToString();
            }
           
            Debug.Log(randomLetters[j]);
        }
        for (int j = 1; j <= 5; j++)
        {
            Button btnEvent = pn3.GetChild(0).GetChild(j).GetComponent<Button>();
            if (btnEvent != null)
            {
                btnEvent.onClick.AddListener(() =>
                {
                    if (btnEvent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == words[i].word.ToString())
                    {
                        Debug.Log("Đúng");
                        btnSuccess();
                    }
                    else
                    {
                        Debug.Log("Sai");
                        btnFail();
                    }
                    for (int k = 1; k <=5; k++)
                    {
                        pn3.GetChild(0).GetChild(k).transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 250;
                    }
                    btnEvent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 600;
                });
            }
        }
        pn3.GetChild(1).GetChild(2).GetComponent<AudioSource>().clip = words[i].chooseWordSound;
        pn3.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            pn3.GetChild(1).GetChild(2).GetComponent<AudioSource>().Play();
        });
    }
    void findUppercase()
    {
        int i = index.Value;
        System.Random random = new System.Random();
        int randomWord = random.Next(0,1);
        Debug.Log(randomWord);
        int optionRest = (randomWord == 0) ? 1 : 0;
        Debug.Log(optionRest);
        pn4.GetChild(randomWord).GetChild(0).GetComponent<TextMeshProUGUI>().text = words[i].word.ToUpper();
        pn4.GetChild(optionRest).GetChild(0).GetComponent<TextMeshProUGUI>().text = words[i].word.ToLower();
        pn4.GetChild(randomWord).GetChild(0).GetComponent<TextMeshProUGUI>().color = GetRandomColor(); 
        pn4.GetChild(optionRest).GetChild(0).GetComponent<TextMeshProUGUI>().color = GetRandomColor();
        pn4.GetChild(randomWord).GetComponent<Button>().onClick.AddListener(() =>
        {
            btnSuccess();
        });
         
        pn4.GetChild(optionRest).GetComponent<Button>().onClick.AddListener(() =>
        {
            btnFail();
            /*fail.gameObject.SetActive(true);*/
        });
        pn4.GetChild(2).GetChild(2).GetComponent<AudioSource>().clip = words[i].chooseWordUpper;
        pn4.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            pn4.GetChild(2).GetChild(2).GetComponent<AudioSource>().Play();
        });
    }
    void findLowercase()
    {
        int i = index.Value;
        System.Random random = new System.Random();
        int randomWord = random.Next(0, 1);
        Debug.Log(randomWord);
        int optionRest = (randomWord == 0) ? 1 : 0;
        Debug.Log(optionRest);
        pn5.GetChild(randomWord).GetChild(0).GetComponent<TextMeshProUGUI>().text = words[i].word.ToUpper();
        pn5.GetChild(optionRest).GetChild(0).GetComponent<TextMeshProUGUI>().text = words[i].word.ToLower();
        pn5.GetChild(randomWord).GetChild(0).GetComponent<TextMeshProUGUI>().color = GetRandomColor();
        pn5.GetChild(optionRest).GetChild(0).GetComponent<TextMeshProUGUI>().color = GetRandomColor();
        pn5.GetChild(optionRest).GetComponent<Button>().onClick.AddListener(() =>
        {
            btnSuccess();

        });

        pn5.GetChild(randomWord).GetComponent<Button>().onClick.AddListener(() =>
        {
            btnFail();
        });
        pn5.GetChild(2).GetChild(2).GetComponent<AudioSource>().clip = words[i].chooseWordLower;
        pn5.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            pn5.GetChild(2).GetChild(2).GetComponent<AudioSource>().Play();
        });
    }
    List<char> GenerateRandomLetters(int count)
    {
        List<char> alphabet = new List<char>
        {
            'A', 'B', 'C', 'D', 'E', 'G', 'H', 'I',
            'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y'
        };

        List<char> randomLetters = new List<char>();
        System.Random random = new System.Random();

        for (int i = 0; i < count; i++)
        {
            int randomIndex = random.Next(0, alphabet.Count);
            randomLetters.Add(alphabet[randomIndex]);
            alphabet.RemoveAt(randomIndex);
        }

        return randomLetters;
    }
    void btnPauseVideo()
    {
        Transform play = pn1.GetChild(1).GetChild(0);
        Transform pause = pn1.GetChild(1).GetChild(1);
        Transform video = pn1.GetChild(0).GetChild(1);
        Transform audio = pn1.GetChild(5);
        if (pause.GetComponent<Button>() != null)
        {
            pause.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    /*video.GetComponent<VideoPlayer>().Pause();*/
                    audio.GetComponent<AudioSource>().Pause();
                    pause.GetComponent<Button>().gameObject.SetActive(false);
                    play.GetComponent<Button>().gameObject.SetActive(true);
                }
            );
        }
    }
    void btnPlayVideo()
    {
        Transform play = pn1.GetChild(1).GetChild(0);
        Transform pause = pn1.GetChild(1).GetChild(1);
        Transform video = pn1.GetChild(0).GetChild(1);
        Transform audio = pn1.GetChild(5);
        if (play.GetComponent<Button>() != null)
        {
            play.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    /*video.GetComponent<VideoPlayer>().Play();*/
                    audio.GetComponent<AudioSource>().Play();
                    play.GetComponent<Button>().gameObject.SetActive(false);
                    pause.GetComponent<Button>().gameObject.SetActive(true);
                }
            );
        }
    }
    void setActiveFalse()
    {
        pn1.gameObject.SetActive(false);
        pn2.gameObject.SetActive(false);
        pn3.gameObject.SetActive(false);
        pn4.gameObject.SetActive(false);
        pn5.gameObject.SetActive(false);
    }
    void btnSuccess()
    {
        success.SetActive(true);

        if (success != null)
        {
            success.GetComponent<Button>().onClick.AddListener(() => {
                success.SetActive(false);
            });
        }
    }
    void btnFail()
    {
        fail.SetActive(true);

        if (fail != null)
        {
            fail.GetComponent<Button>().onClick.AddListener(() => {
                fail.SetActive(false);
            });
        }
    }
    void btnNext()
    {
        if (next != null)
        {
            next.onClick.AddListener(() =>
            {
                setActiveFalse();
                switch (page)
                {
                    /*case 1:
                        undo.gameObject.SetActive(true);
                        pn2.gameObject.SetActive(true);
                        page++;
                        break;
                    case 2:
                        pn3.gameObject.SetActive(true);
                        page++;
                        break;
                    case 3:
                        pn4.gameObject.SetActive(true);
                        page++;
                        break;
                    case 4:
                        pn5.gameObject.SetActive(true);
                        page++;
                        break;
                    case 5:
                        SceneManager.LoadScene("SampleScene");
                        break;*/
                    case 1:
                        undo.gameObject.SetActive(true);
                        pn2.gameObject.SetActive(true);
                        page++;
                        break;
                    case 2:
                        pn4.gameObject.SetActive(true);
                        page++;
                        break;
                    case 3:
                        pn5.gameObject.SetActive(true);
                        page++;
                        break;
                    case 4:
                        SceneManager.LoadScene("SampleScene");
                        break;
                    default:
                        
                        break;
                }
                
            });
        }
    }
    void btnUndo()
    {
        if (undo != null)
        {
            undo.onClick.AddListener(() =>
            {
                setActiveFalse();
                switch (page)
                {
                    /*case 2:
                        page--;
                        pn1.gameObject.SetActive(true);
                        undo.gameObject.SetActive(false);
                        break;
                    case 3:
                        page--;
                        pn2.gameObject.SetActive(true);
                        break;
                    case 4:
                        page--;
                        pn3.gameObject.SetActive(true);
                        break;
                    case 5:
                        page = 4;
                        pn4.gameObject.SetActive(true);
                        next.gameObject.SetActive(true);
                        break;*/
                    case 2:
                        page--;
                        pn1.gameObject.SetActive(true);
                        undo.gameObject.SetActive(false);
                        break;
                    case 3:
                        page--;
                        pn2.gameObject.SetActive(true);
                        break;
                    case 4:
                        page = 4;
                        pn4.gameObject.SetActive(true);
                        next.gameObject.SetActive(true);
                        break;
                    default:
                        break;
                }
            });
        }
    }
    void btnHome()
    {
        if (home != null) {
            home.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("StartGame");
            });       
        }
    }
}
