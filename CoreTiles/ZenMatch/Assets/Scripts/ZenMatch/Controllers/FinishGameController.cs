using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZenMatch.Interfaces;
using ZenMatch.Utils;

namespace ZenMatch.Controllers
{
    public class FinishGameController
    {
        /// <summary>
        /// Событие финиша игры. bool - выигрыш/проигрыш
        /// </summary>
        public static event Action<bool> OnGameFinished;
        
        private readonly List<ITilesOwner> _tilesOwners;
        private ITilesCollector _tilesCollector;

        public void AddTilesOwners(List<ITilesOwner> tilesOwners)
        {
            tilesOwners.ForEach(tilesOwner => tilesOwner.OnTilesCountChange += OnTilesCountChange);
            _tilesOwners.AddRange(tilesOwners);
        }
        public void AddTilesCollector(ITilesCollector tilesCollector)
        {
            _tilesCollector = tilesCollector;
            _tilesCollector.OnTilesFull += TilesCollectorOnTilesFull;
        }

        private void TilesCollectorOnTilesFull()
        {
            OnGameFinished?.Invoke(false);
        }

        private void OnTilesCountChange()
        {
            var tilesLeft = _tilesOwners.Sum(tilesOwner => tilesOwner.TilesCount);
            if (tilesLeft == 0)
            {
                var coroutineStarter = _tilesCollector.gameObject.GetComponent<MonoBehaviour>();
                coroutineStarter.StartCoroutine(AwaitedFinishGame(true, Configs.GameConfig.finishGameDelay));
            }
        }
        
        private IEnumerator AwaitedFinishGame(bool isWin, float delay)
        {
            yield return new WaitForSeconds(delay);
            OnGameFinished?.Invoke(isWin);
        }

        public void Clear()
        {
            _tilesOwners.Clear();
        }
    }
}