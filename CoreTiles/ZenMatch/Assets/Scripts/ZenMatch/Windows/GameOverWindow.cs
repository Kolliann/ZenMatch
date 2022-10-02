using System;
using CoreTiles.Scripts.ZenMatch.Controllers;
using UnityEngine;
using ZenMatch.Controllers;

namespace ZenMatch.Windows
{
    public class GameOverWindow : WindowBase
    {
        protected override void CustomAwake()
        {
            FinishGameController.OnGameFinished += OnGameFinished;
            Show(false);
        }
        private void OnDestroy()
        {
            FinishGameController.OnGameFinished -= OnGameFinished;
        }

        private void OnGameFinished(bool isWin)
        {
            if (!isWin)
            {
                Show(true);
            }
        }

        public void OnRestartClick()
        {
            CoreGameController.StartGame();
            Show(false);
        }
    }
}