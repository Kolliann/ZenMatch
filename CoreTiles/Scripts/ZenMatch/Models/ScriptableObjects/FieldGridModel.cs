using UnityEngine;
using ZenMatch.Models;
using ZenMatch.Models.ScriptableObjects;

namespace CoreTiles.Scripts.ZenMatch.Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FieldGrid", menuName = "ScriptableObjects/Grids/FieldGrid", order = 0)]
    public class FieldGridModel : GridModel
    {
        // TODO: лучше отталкиваться от центра грида.
        // конфигурация грида = точка центра + размер ячейки.
        // нужны 2 разных грида: грид-сетка(тайлы по узлам сетки) + грид-точка(тайлы в точке с указанным смещением)
        
        public Vector2Int size;
        public Vector2Int offsets;
        public GridCellsModel cells;
    }
}