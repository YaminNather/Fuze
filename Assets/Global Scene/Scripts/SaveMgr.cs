using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class SaveMgr : MonoBehaviour
{
    private void Save_F()
    {

    }

    private void Load_F()
    {

    }
}

[System.Serializable]
public class SaveData
{
    private readonly string m_SAVEPATH = Application.persistentDataPath + "/Saves/Save0.txt";

    public SaveData()
    {
        SetValuesToDefault_F();
    }

    public void SetValuesToDefault_F()
    {
        m_Currency = 0;
        m_OwnedBalls = new List<int>(){0};
        m_HighScore = 0;
    }



    private int m_Currency;
    private List<int> m_OwnedBalls;
    private int m_HighScore;

    public int GetCurrency_F() => m_Currency;
    public int SetCurrency_F(int value) => m_Currency = Mathf.Clamp(value, 0, 9999999);

    public int[] GetListOfOwnedBalls_F() => m_OwnedBalls.ToArray() as int[];
    public void AddToListOfOwnedBalls_F(int id)
    {
        if (m_OwnedBalls.Contains(id)) return;
        
        m_OwnedBalls.Add(id);
    }
    public void RemoveFromListOfOwnedBalls_F(int id)
    {
        if (!m_OwnedBalls.Contains(id)) return;

        m_OwnedBalls.Remove(id);
    }

    public int GetHighScore_F() => m_HighScore;
    public int SetHighScore_F(int value) => m_HighScore = Mathf.Clamp(value, 0, 999999);
}
