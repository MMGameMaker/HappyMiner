using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] Text timeText;
    [SerializeField] Text moneyText;
    [SerializeField] Text result;
    [SerializeField] Text levelTarget;

    [SerializeField] int minMoney;

    public GameObject stopGamePanel;
    public Button[] stopPaneButtons;

    public int CurMoney { get; private set; }
    public int CurLevel { get; private set; }

    public int TimeLimit;

    public bool endGame = false;

    [SerializeField] bool lastLevel;
    [SerializeField] string nextLevel;
    

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        stopGamePanel.SetActive(false);
        CurLevel = 1;
        CurMoney = DataSerializer.LoadScore("Score");
        StartCoroutine("CountDown");
        result.text = "";
        levelTarget.text = minMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        this.timeText.text = TimeLimit.ToString();
        this.moneyText.text = CurMoney.ToString();
        this.endGame = false;
    }


    public IEnumerator CountDown()
    {
        bool add = true;
        while (add)
        {
            yield return new WaitForSeconds(1);
            if (TimeLimit > 0)
            {
                TimeLimit--;
            }
            if(TimeLimit <= 0)
            {
                EndGame();
                if(CurMoney >= minMoney && endGame)
                {
                    CurLevel++;
                    Debug.Log(CurLevel);
                    stopGamePanel.SetActive(true);
                    ResetStopPaneButtons();
                    stopPaneButtons[0].interactable = false;
                    result.text = "To Next Level?";
                }
                else if( CurMoney < minMoney && endGame)
                {
                    stopGamePanel.SetActive(true);
                    ResetStopPaneButtons();
                    stopPaneButtons[1].interactable = false;
                    result.text = "Restart?";
                }
                if(CurMoney> DataSerializer.LoadHightScore("HightScore"))
                {
                    DataSerializer.SaveHightScore("HightScore", CurMoney);
                    Debug.Log("hight score:" + DataSerializer.LoadHightScore("HightScore").ToString());
                }
                
                StopCoroutine("CountDown");

            }    
        }
    }

    private void EndGame()
    {
        this.endGame = true;
        Time.timeScale = 0;
    }

    public void AddMoney(int amout)
    {
        this.CurMoney += amout;
        Debug.Log(this.CurMoney);
    }

    public void ResetStopPaneButtons()
    {
        foreach(Button buton in stopPaneButtons)
        {
            buton.interactable = true;
        }
    }

    public void BacktoMenu()
    {
        DataSerializer.SaveScore("Score", 0);
        SceneManager.LoadScene("Menu");
    }

    public void ToNextLevel()
    {
        if (!lastLevel)
        {
            DataSerializer.SaveScore("Score", CurMoney);
            SceneManager.LoadScene(nextLevel);
        }
        else if (lastLevel)
        {
            DataSerializer.SaveScore("Score", 0);
            SceneManager.LoadScene("HightScore");
        }

    }

    public void RestartGame()
    {
        DataSerializer.SaveScore("Score", 0);
        SceneManager.LoadScene("Level1");
    }
}
