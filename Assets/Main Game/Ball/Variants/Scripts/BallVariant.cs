using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="_BallVariant", menuName="Custom/Ball Variant")]
public class BallVariant : ScriptableObject
{
    [SerializeField] private Texture2D m_BallT2D;

    public Texture2D GetBallTexture2D_F() => m_BallT2D;
}
