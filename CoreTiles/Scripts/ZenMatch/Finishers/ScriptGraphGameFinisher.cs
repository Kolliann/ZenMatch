using CoreTiles.Scripts.Integration.ScriptGraph;
using Unity.VisualScripting;

namespace CoreTiles.Scripts.ZenMatch.Finishers
{
    /// <summary>
    /// Завершение игры через скрипт граф
    /// </summary>
    public class ScriptGraphGameFinisher : IGameFinisher<int>
    {
        public void FinishGameActions(int finishedLevelId)
        {
            EventBus.Trigger(EventNames.OnTilesGameFinished, finishedLevelId);
        }
    }
}