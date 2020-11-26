using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMgr : MonoBehaviour
{
    static private GlobalMgr s_Instance;

    [SerializeField] private BallVariant[] m_BallVariants;


    static public GlobalMgr GetInstance_F() => s_Instance;

    public BallVariant GetBallVariant_F(int index) => m_BallVariants[index];
    public int GetBallVariantsCount_F() => m_BallVariants.Length;
}
