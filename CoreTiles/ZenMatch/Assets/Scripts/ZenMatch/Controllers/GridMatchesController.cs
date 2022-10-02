using System;
using System.Collections.Generic;
using CoreTiles.Scripts.ZenMatch.Controllers;
using UnityEngine;
using ZenMatch.Animatons;
using ZenMatch.Enums;
using ZenMatch.Models.ScriptableObjects;
using ZenMatch.Views;

namespace ZenMatch.Controllers
{
    public class MatchesController
    {
        private readonly GameObject _fxHolder;
        private readonly List<TileView> _tiles;

        public bool HasMatches => MatchedTiles.Count > 0;
        public List<TileView> MatchedTiles { get; }

        private TileModel _matchedTileModel;

        public MatchesController(List<TileView> tiles, GameObject fxHolder)
        {
            _tiles = tiles;
            _fxHolder = fxHolder;
        }

        /// <summary>
        /// Проверка матчей на поле
        /// </summary>
        public bool CheckMatches()
        {
            if (_tiles.Count < 3)
                return false;
            
            // TODO: не самый лучший алгоритм. для прототипа норм
            for (int i = 1; i < _tiles.Count - 1; i++)
            {
                if (_tiles[i].TileModel == _tiles[i - 1].TileModel &&
                    _tiles[i].TileModel == _tiles[i + 1].TileModel)
                {
                    if (MatchedTiles.Contains(_tiles[i]))
                        continue;
                    MatchedTiles.InsertRange(0, _tiles.GetRange(i -1, 3));
                    _matchedTileModel = _tiles[i].TileModel;
                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// Логическая очистка сматченных тайлов, запуск визуала дестроя тайлов
        /// </summary>
        /// <param name="tileDestroyedCallback"> Каллбэк визуального уничтожения тайлов </param>
        /// <param name="gridCallback"> Каллбэк логической очистки сматченных тайлов для грида </param>
        public void StartDestroyMatchedTiles(
            Action<TileAnimator> tileDestroyedCallback, Action<List<TileView>, TileModel> gridCallback)
        {
            MatchedTiles.ForEach((tileView) =>
            {
                tileView.GetComponent<TileAnimator>().AnimateDestroy(tileDestroyedCallback);
                tileView.GetComponent<TileFxActivator>().ActivateDestroyFx(tileView, _fxHolder);
            });

            gridCallback?.Invoke(MatchedTiles, _matchedTileModel);
            MatchedTiles.Clear();
            _matchedTileModel = null;
            
            CoreGameController.SoundController.PlayActionSound(SoundActionType.TilesMatch);
        }
        
    }
}