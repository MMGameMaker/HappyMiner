using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HightSCoreBoard : MonoBehaviour
{
    public Text scoreText;


    private void Start()
    {
        scoreText.text = DataSerializer.LoadHightScore("HightScore").ToString();
    }

    public void UpdateHightScore()
    {

    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ResetHightScore()
    {
        DataSerializer.SaveHightScore("HightScore", 0);
        scoreText.text = DataSerializer.LoadHightScore("HightScore").ToString();
    }

}
