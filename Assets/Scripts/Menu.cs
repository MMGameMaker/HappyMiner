using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void ToHightScoreBoard()
    {
        SceneManager.LoadScene("HightScore");
    }

    public void StartPlay()
    {
        SceneManager.LoadScene("Level1");
    }
}
