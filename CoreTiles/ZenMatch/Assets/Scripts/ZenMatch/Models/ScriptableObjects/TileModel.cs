using UnityEngine;

namespace ZenMatch.Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Tile", order = 2)]
    public class TileModel : ScriptableObject
    {
        public string id;
        public GameObject icon;
    }
}