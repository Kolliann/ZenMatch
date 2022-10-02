using UnityEngine.Playables;
using UnityEngine.Timeline;
using ZenMatch.Utils;

namespace CoreTiles.Scripts.ZenMatch.Finishers
{
    /// <summary>
    /// Завершение игры через таймлайн
    /// </summary>
    public class TimelineGameFinisher : IGameFinisher<PlayableDirector>
    {
        public void FinishGameActions(PlayableDirector playableDirector)
        {
            // Запуск плэйабла с нужного кадра
            if (playableDirector)
            {
             //   playableDirector.time = Configs.GameConfig.finishGameDirectorFrame /
           //                             ((TimelineAsset)playableDirector.playableAsset).editorSettings.frameRate;
                playableDirector.Resume();
            }
        }
    }
}