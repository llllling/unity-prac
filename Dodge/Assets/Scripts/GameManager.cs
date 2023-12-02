using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI recordText;

    private float survivalTime;
    private bool isGameover;

    void Start()
    {
        survivalTime = 0f;
        isGameover = false;
    }

    void Update()
    {
        if (isGameover && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene"); 
            return;
        }

        survivalTime += Time.deltaTime;
        timeText.text = "Time : " + (int)survivalTime;
    }

    public void EndGame()
    {
        isGameover = true;
        gameoverText.SetActive(true);


        if (survivalTime > PlayerPrefs.GetFloat("BestTime"))
        {

            PlayerPrefs.SetFloat("BestTime", survivalTime);
            recordText.text = "Best Time : " + (int) survivalTime;
        }
    }
}
