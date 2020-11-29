using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName="_BallVariant", menuName="Custom/Ball Variant")]
public class BallVariant : ScriptableObject
{
    [SerializeField] private int m_Id;
    [SerializeField] private Sprite m_Sprite;

    public int GetId => m_Id;
    public Sprite GetBallSprite_F() => m_Sprite;
}
