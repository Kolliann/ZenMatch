using System;
using CoreTiles.Scripts.ZenMatch.Animatons;
using CoreTiles.Scripts.ZenMatch.Finishers;
using UnityEngine;
using UnityEngine.Playables;
using ZenMatch.Controllers;
using ZenMatch.Models.ScriptableObjects;
using ZenMatch.Utils;

namespace CoreTiles.Scripts.ZenMatch.Controllers
{
    public class CoreGameController : MonoBehaviour
    {
        /// <summary>
        /// Событие матча. TileModel - моделька продукта (ScriptableObject)
        /// </summary>
        public static event Action<TileModel> OnMatch;
        /// <summary>
        /// Событие старта игры
        /// </summary>
        public static event Action OnGameStarted;
        /// <summary>
        /// Событие финиша игры
        /// </summary>
        public static event Action<bool> OnGameFinished;

        /// <summary>
        /// Триггер старта игры
        /// </summary>
        private static Action<bool> _startGameTrigger;

        /// <summary>
        /// Контроллер звуков
        /// </summary>
        public static SoundController SoundController { get; private set; }

        // TODO: move
        public PlayableDirector playableDirector;
        
        public GameConfig gameConfig;
        
        [SerializeField]
        public Canvas Canvas;
        
        public GameWindowAnimator gameWindowAnimator;
        public SoundController soundController;
        public LevelsController levelsController;
        public GridContainersController gridContainersController;

        private readonly FinishGameController _finishGameController = new();
        private readonly IGameFinisher<int> _gameFinisher = new ScriptGraphGameFinisher();

        /// <summary>
        /// Метод старта игры
        /// </summary>
        public static void StartGame(bool skipGameplay = false)
        {
            _startGameTrigger?.Invoke(skipGameplay);
        }

        public static void InvokeOnMatchEvent(TileModel tileModel)
        {
            OnMatch?.Invoke(tileModel);
        }

        private void Awake()
        {
            Configs.GameConfig = gameConfig;
            Configs.GameController = this;
            Configs.LevelsController = levelsController;
            
            SoundController = soundController;
            _startGameTrigger += StartGameWithLevel;
            FinishGameController.OnGameFinished += OnFinishGame;
        }

        private void OnDestroy()
        {
            _startGameTrigger -= StartGameWithLevel;
            FinishGameController.OnGameFinished -= OnFinishGame;
        }

        private void StartGameWithLevel(bool skipGameplay)
        {
            var levelModel = levelsController.GetCurrentLevel();
            gridContainersController.Init(levelModel.grids);
            gridContainersController.InitFinishGameController(_finishGameController);
            ShowGameWindow(skipGameplay);
            OnGameStarted?.Invoke();
        }

        private void ShowGameWindow(bool skipGameplay)
        {
            // TODO: bad
            if(playableDirector)
                playableDirector.Pause();
            
            gameWindowAnimator.ShowGameWindow(() =>
            {
                if(skipGameplay)
                    OnFinishGame(isWin: true);
            });
        }

        private void OnFinishGame(bool isWin)
        {
            if (isWin)
            {
                gameWindowAnimator.HideGameWindow(() =>
                {
                    _gameFinisher.FinishGameActions(levelsController.LastPlayedLevelIndex);
                });
            }
            OnGameFinished?.Invoke(isWin);
        }
    }
}
