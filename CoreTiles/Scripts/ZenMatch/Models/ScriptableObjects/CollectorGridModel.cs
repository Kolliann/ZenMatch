using UnityEngine;

namespace ZenMatch.Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CollectorGrid", menuName = "ScriptableObjects/Grids/CollectorGrid", order = 2)]
    public class CollectorGridModel : GridModel
    {
        public Vector2Int size;
        public Vector2Int cellSize;
        public int spacing;
    }
}