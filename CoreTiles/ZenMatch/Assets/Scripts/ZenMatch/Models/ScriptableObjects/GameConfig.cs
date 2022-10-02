using UnityEngine;
using ZenMatch.Views;

namespace ZenMatch.Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 6)]
    public class GameConfig : ScriptableObject
    {
        public CollectorGridModel collectorGridModel;
        public TileView tileViewPrefab;
        public int cellSize;

        public float tileCollectFlyTime = 0.15f;
        public float tileShiftFlyTime = 0.07f;
        public float endFlyCallbackTime = 0.2f;
        public float finishGameDelay = 0.15f;

        public Color normalColor = new Color32(1, 1, 1, 1);
        public Color stackFadeColor = new Color(0.3f, 0.3f, 0.3f, 1);
        public Color fieldFadeColor = new Color(0.75f, 0.75f, 0.75f, 1);

        [Header("Фрейм таймлайна, с которого стартует директор после завершения игры матч-3-дзен")]
        public int finishGameDirectorFrame = 331;
    }
}