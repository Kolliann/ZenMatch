using Unity.VisualScripting;

namespace CoreTiles.Scripts.Integration.ScriptGraph
{
    // Register a string name for your Custom Scripting Event to hook it to an Event.
    // You can save this class in a separate file and add multiple Events to it as public static strings.
    public static class EventNames
    {
        public static string OnTilesGameFinished = "OnTilesGameFinished";
    }
    
    /// <summary>
    /// Компонент для триггера финиша игры через скрипт граф.
    /// </summary>
    [UnitTitle("On Tiles Game Finished")]
    [UnitCategory("Events\\Tiles")]
    public class OnFinishTilesGame : EventUnit<int>
    {
        [DoNotSerialize]// No need to serialize ports.
        public ValueOutput finishedLevelId { get; private set; }// The Event output data to return when the Event is triggered.
        
        protected override bool register => true;

        // Add an EventHook with the name of the Event to the list of Visual Scripting Events.
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventNames.OnTilesGameFinished);
        }
        
        protected override void Definition()
        {
            base.Definition();
            // Setting the value on our port.
            finishedLevelId = ValueOutput<int>(nameof(finishedLevelId));
        }
        
        // Setting the value on our port.
        protected override void AssignArguments(Flow flow, int data)
        {
            flow.SetValue(finishedLevelId, data);
        }
    }
}