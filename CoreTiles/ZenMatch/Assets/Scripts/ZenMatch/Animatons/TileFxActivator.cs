using System;
using System.Linq;
using UnityEngine;
using ZenMatch.Models.ScriptableObjects;
using ZenMatch.Views;

namespace ZenMatch.Animatons
{
    public class TileFxActivator : MonoBehaviour
    {
        [Serializable]
        public class TileFxData
        {
            public TileModel tileModel;
            public GameObject fxPrefab;
        }

        public TileFxData[] tilesFxData;
        public GameObject defaultFxPrefab;
        public float fxScale = 2.0f;
        
        /// <summary>
        /// Старт эффекта уничтожения партикла.
        /// Нужно визуально синкать с анимацией уничтожения. 
        /// </summary>
        public void ActivateDestroyFx(TileView tileView, GameObject fxHolder)
        {
            var fxPrefab = GetFxForModel(tileView.TileModel);
            var destroyFx = Instantiate(fxPrefab, tileView.TargetPosition, Quaternion.identity, fxHolder.transform);
            destroyFx.transform.position = tileView.TargetPosition;
            destroyFx.transform.localPosition = new Vector3(destroyFx.transform.localPosition.x, destroyFx.transform.localPosition.y, 0);
            destroyFx.transform.localScale = Vector3.one * fxScale;
        }

        private GameObject GetFxForModel(TileModel tileModel)
        {
            var tileFxData = tilesFxData.FirstOrDefault(tileData => tileData.tileModel == tileModel);
            return tileFxData?.fxPrefab ? tileFxData.fxPrefab : defaultFxPrefab;
        }
    }
}