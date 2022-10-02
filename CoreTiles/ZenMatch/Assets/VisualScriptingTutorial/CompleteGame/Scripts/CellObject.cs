using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace VisualScriptingTutorial
{
    public class CellObject : TurnReceiver
    {
        public bool AddGroundMesh = true;
    
        private void OnEnable()
        {
            EntryPoint.Instance.TurnManager.RegisterTurnReceiver(this);
        }

        private void OnDisable()
        {
            EntryPoint.Instance.TurnManager.UnregisterTurnReceiver(this);
        }

        public override void Undo()
        {
            
        }
    }
}