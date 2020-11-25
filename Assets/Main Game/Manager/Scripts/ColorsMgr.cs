using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGameMgrStuff
{
    public class ColorsMgr : MonoBehaviour
    {
        #region Variables
        private MainGameMgr m_MainGameMgr;

        [SerializeField] private Color[] m_Colors;
        //private int m_ColorCurIndex;
        //public int ColorCurIndex => m_ColorCurIndex;
        #endregion

        private void Awake()
        {
            m_MainGameMgr = GetComponent<MainGameMgr>();
            //ColorCurIndexSetRandom_F();
        }

        public Color GetRandomColor_F()
        {
            int index = Random.Range(0, m_Colors.Length);
            return m_Colors[index];
        }

        //public int RandomColorExcludingCurGet_F(out Color color)
        //{
        //    int r;
        //    do
        //    {
        //        r = GetRandomColor_F(out color);
        //    } while (r == m_ColorCurIndex);

        //    return r;
        //}

        //public int ColorCurGet_F(out Color color)
        //{
        //    int r = m_ColorCurIndex;
        //    color = GetColorFromIndex_F(ColorCurIndex);
        //    return r;
        //}

        public Color GetColorFromIndex_F(int colorIndex) => m_Colors[colorIndex];

        //public int ColorCurIndexSetRandom_F()
        //{
        //    m_ColorCurIndex = GetRandomColor_F(out _);
        //    return m_ColorCurIndex;
        //}
    }
}
