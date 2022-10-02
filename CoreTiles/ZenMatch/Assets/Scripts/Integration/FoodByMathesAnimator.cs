using CoreTiles.Scripts.ZenMatch.Controllers;
using UnityEngine;
using ZenMatch.Controllers;
using ZenMatch.Models.ScriptableObjects;

namespace Integration
{
    /// <summary>
    /// Скрипт спавнит на сцену сматченную в матч3дзен еду
    /// </summary>
    public class FoodByMathesAnimator : MonoBehaviour
    {
        [SerializeField]
        public Animator animator;
        
        [SerializeField]
        public float speedMultiplier;
        
        private static readonly int SpeedMultiplierHash = Animator.StringToHash("SpeedMultiplier");

        private void Awake()
        {
            CoreGameController.OnMatch += ZenMatchGameControllerOnMatch;
            CoreGameController.OnGameStarted += ZenMatchGameControllerOnGameStarted;
            CoreGameController.OnGameFinished += ZenMatchGameControllerOnGameFinished;
        }

        private void OnDestroy()
        {
            CoreGameController.OnMatch -= ZenMatchGameControllerOnMatch;
            CoreGameController.OnGameStarted -= ZenMatchGameControllerOnGameStarted;
            CoreGameController.OnGameFinished -= ZenMatchGameControllerOnGameFinished;
        }

        private void ZenMatchGameControllerOnGameStarted()
        {
        }

        private void ZenMatchGameControllerOnGameFinished(bool isWin)
        {
            if (!isWin)
            {
                // TODO: логика удаления еды при рестарте
                ReturnToInitialState();
            }
        }

        private void ZenMatchGameControllerOnMatch(TileModel tileModel)
        {
            if (tileModel != null)
            {
                animator.SetFloat(SpeedMultiplierHash, speedMultiplier);
                animator.SetTrigger(tileModel.id);
            }
        }

        private void ReturnToInitialState()
        {
            // TODO: логика удаления еды при рестарте
        }
    }
}