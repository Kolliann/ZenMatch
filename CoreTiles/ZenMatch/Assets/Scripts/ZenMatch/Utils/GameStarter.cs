using CoreTiles.Scripts.ZenMatch.Controllers;
using UnityEngine;
using ZenMatch.Controllers;

namespace ZenMatch.Utils
{
    /// <summary>
    /// Класс для триггера старта игры из таймлайна
    /// </summary>
    public class GameStarter : MonoBehaviour
    {
        public bool startGameOnEnable = true;

        private void OnEnable()
        {
            if (startGameOnEnable)
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            CoreGameController.StartGame();
        }
    }
}