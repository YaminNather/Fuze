using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public void SaveNew_F()
    {
        m_SaveData = new SaveData();
        Save_F();
    }

    public void Save_F()
    {
        if (m_SaveData == null)
            throw new Exception("Save Data is null");
        
        File.WriteAllText(m_SAVEPATH, JsonUtility.ToJson(m_SaveData));
    }

    public void Load_F()
    {
        if (!SaveFileExists_F() || !SaveFileIsValid_F())
            SaveNew_F();

        m_SaveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(m_SAVEPATH));
    }
    
    private bool SaveFileExists_F() => File.Exists(m_SAVEPATH);

    private bool SaveFileIsValid_F() => JsonUtility.FromJson<SaveData>(File.ReadAllText(m_SAVEPATH)) != null;



    private readonly string m_SAVEPATH = Application.persistentDataPath + "/Saves/Save0.txt";
    private SaveData m_SaveData;

    [SerializeField] private BallVariant[] m_BallVariants;
    private Dictionary<int, BalVariant> m_BallVariants;

    public int GetHighScore_F() => m_SaveData.GetHighScore_F();
    public void SetHighScore_F(int value) => m_SaveData.SetHighScore_F(value);

    public int GetCurrency_F() => m_SaveData.GetCurrency_F();
    public void SetCurrency_F(int value) => m_SaveData.SetCurrency_F(value);

    public BallVariant[] GetAllBallVariants_F() => m_BallVariants.Clone() as BallVariant[];
    public BallVariant GetBallVariantWithId_F() => m_BallVariants.

    public int[] GetListOfOwnedBalls_F() => m_SaveData.GetListOfOwnedBalls_F();
    public void AddToListOFOwnedBalls_F(int id) => m_SaveData.AddToListOfOwnedBalls_F(id);
    public void RemoveFromListOFOwnedBalls_F(int id) => m_SaveData.RemoveFromListOfOwnedBalls_F(id);
}
