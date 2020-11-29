using System.Collections;
using System.Collections.Generic;
using BallPawnStuff;
using UnityEngine;

namespace MainGameMgrStuff
{
    public class BallsSpawner : MonoBehaviour
    {
        #region Variables
        [Header("Prefabs")]
        [SerializeField] private BallPawn m_NormalBallPawnPrefab;
        [SerializeField] private BlackBallPawn m_BlackBallPawnPrefab;
        [SerializeField] private AnyColorBallPawn m_AnyColorBallPawnPrefab;
        
        [Header("Materials Stuff")]
        [SerializeField] private Material[] m_BallMaterials;
        #endregion

        public void SetBallTexture_F(Texture2D texture)
        {
            if (m_BallMaterials == null || m_BallMaterials.Length == 0) return;

            foreach(Material material in m_BallMaterials) 
                material.SetTexture("_EmissionMap", texture);
        }

        public NormalBallPawn SpawnNormalBall_F(Vector3 pos, Color color)
        {
            GameObject spawnedGObj = Instantiate(m_NormalBallPawnPrefab.gameObject,
                pos,
                Quaternion.identity);

            NormalBallPawn normalBallPawn = spawnedGObj.GetComponent<NormalBallPawn>();
            normalBallPawn.SetColor_F(color);

            return normalBallPawn;
        }

        public BlackBallPawn SpawnBlackBall_F(Vector3 pos)
        {
            GameObject spawnedGObj = Instantiate(m_BlackBallPawnPrefab.gameObject,
                pos,
                Quaternion.identity);

            return spawnedGObj.GetComponent<BlackBallPawn>();
        }

        public AnyColorBallPawn SpawnAnyColorBallPawn_F(Vector3 pos)
        {
            GameObject spawnedGObj = Instantiate(m_AnyColorBallPawnPrefab.gameObject,
                pos,
                Quaternion.identity);

            return spawnedGObj.GetComponent<AnyColorBallPawn>();
        }
    }
}
