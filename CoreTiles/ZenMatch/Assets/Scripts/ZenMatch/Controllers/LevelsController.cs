using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZenMatch.Controllers;
using ZenMatch.Models.ScriptableObjects;

namespace CoreTiles.Scripts.ZenMatch.Controllers
{
    public class LevelsController : MonoBehaviour
    {
        public List<LevelModel> levels;

        private int _currentLevelIndex;

        public LevelModel LastPlayedLevel { get; private set; }
        
        public int LastPlayedLevelIndex { get; private set; }
        
        public LevelModel CurrentLevel { get; private set; }

        /// <summary>
        /// Сетает уровень
        /// </summary>
        public void SetCurrentLevel(int levelIndex)
        {
            UpdateCurrentLevel(levelIndex);
        }

        /// <summary>
        /// Гетает уровень
        /// </summary>
        public LevelModel GetCurrentLevel()
        {
            UpdateCurrentLevel(_currentLevelIndex);
            LastPlayedLevel = CurrentLevel;
            LastPlayedLevelIndex = _currentLevelIndex;
            return CurrentLevel;
        }

        public LevelModel GetLevel(string levelId)
        {
            var level = levels.FirstOrDefault(level => level.id == levelId);
            if (level)
            {
                UpdateCurrentLevel(levels.IndexOf(level));
                LastPlayedLevel = level;
                LastPlayedLevelIndex = _currentLevelIndex;
                return level;
            }
            if(!string.IsNullOrEmpty(levelId))
                Debug.LogError($"Level with id {levelId} not found!");
            return GetCurrentLevel();
        }

        private void OnGameFinished(bool isWin)
        {
            if (isWin)
            {
                UpdateCurrentLevel(_currentLevelIndex + 1);
            }
        }

        private void UpdateCurrentLevel(int index)
        {
            index = Mathf.Max(index, 0);
            index = Mathf.Min(index, levels.Count - 1);
            _currentLevelIndex = index;
            CurrentLevel = levels[_currentLevelIndex];
        }

        private void Awake()
        {
            FinishGameController.OnGameFinished += OnGameFinished;
        }

        private void OnDestroy()
        {
            FinishGameController.OnGameFinished -= OnGameFinished;
        }
    }
}