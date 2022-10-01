using UnityEngine;

namespace ZenMatch.Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TilesSet", menuName = "ScriptableObjects/TilesSet", order = 5)]
    public class TilesSetModel : ScriptableObject
    {
        public int count;
        public TileModel[] tiles;
    }
}