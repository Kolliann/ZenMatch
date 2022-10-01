using UnityEngine;

namespace ZenMatch.Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "StackGrid", menuName = "ScriptableObjects/Grids/StackGrid", order = 1)]
    public class StackGridModel : GridModel
    {
        [Header("Отступы считаются от верхних элементов к нижним")]
        public Vector2Int offsets;
        public int[] tiles;
    }
}