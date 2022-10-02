using CoreTiles.Scripts.ZenMatch.Controllers;
using Unity.VisualScripting;
using ZenMatch.Utils;

namespace CoreTiles.Scripts.Integration.ScriptGraph
{
    /// <summary>
    /// Компонент для запуска игры через скрипт граф.
    /// </summary>
    public class PlayTilesGame : Unit
    {
        [DoNotSerialize] // No need to serialize ports.
        public ControlInput inputTrigger; //Adding the ControlInput port variable

        [DoNotSerialize] // No need to serialize ports.
        public ControlOutput outputTrigger;//Adding the ControlOutput port variable.
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput levelId; // Adding the ValueInput variable for myValueA
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput skipGameplay;
        
        protected override void Definition()
        {
            levelId = ValueInput<int>("levelId", 0);
            skipGameplay = ValueInput<bool>(nameof(skipGameplay), false);
            
            //Making the ControlInput port visible, setting its key and running the anonymous action method to pass the flow to the outputTrigger port.
            inputTrigger = ControlInput("", flow =>
            {
                Configs.LevelsController.SetCurrentLevel(flow.GetValue<int>(levelId));
                CoreGameController.StartGame(flow.GetValue<bool>(skipGameplay));
                return outputTrigger;
            });
            
            //Making the ControlOutput port visible and setting its key.
            outputTrigger = ControlOutput("");
            
            Requirement(levelId, inputTrigger); //Specifies that we need the levelId value to be set before the node can run.
            Requirement(skipGameplay, inputTrigger); //Specifies that we need the levelId value to be set before the node can run.
            Succession(inputTrigger, outputTrigger); //Specifies that the input trigger port's input exits at the output trigger port. Not setting your succession also dims connected nodes, but the execution still completes.
        }
    }
}