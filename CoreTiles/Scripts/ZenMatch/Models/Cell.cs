using System;
using UnityEngine;
using ZenMatch.Enums;

namespace ZenMatch.Models
{
    [Serializable]
    public class Cell
    {
        public Vector2Int position;
        public int layer;
        public int tileId;
    }
}