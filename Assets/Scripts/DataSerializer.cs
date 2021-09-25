using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSerializer : MonoBehaviour
{
    public static void SaveHightScore(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public static int LoadHightScore(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public static void SaveScore(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public static int LoadScore(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

}
