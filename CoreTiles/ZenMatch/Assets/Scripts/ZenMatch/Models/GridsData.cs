using System;
using CoreTiles.Scripts.ZenMatch.Models.ScriptableObjects;
using ZenMatch.Models.ScriptableObjects;

namespace ZenMatch.Models
{
    [Serializable]
    public class GridsData
    {
        public TilesSetModel tilesSetModel;
        public FieldGridModel fieldGridModel;
        public StackGridModel[] stackGrids;
    }
}