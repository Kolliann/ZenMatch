using UnityEngine;

namespace ZenMatch.Models.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
    public class LevelModel : ScriptableObject
    {
        public string id;
        public GridsData grids;
    }
}