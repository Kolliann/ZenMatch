using System;
using System.Collections.Generic;
using UnityEngine;
using ZenMatch.Behaviours;
using ZenMatch.Views;
using Object = UnityEngine.Object;

namespace ZenMatch.Utils
{
    public class GridTilesShifter
    {
        private readonly List<TileView> _tiles;
        private readonly Func<int, Vector2> _positionGetter;

        public GridTilesShifter(List<TileView> tiles, Func<int, Vector2> positionGetter)
        {
            _tiles = tiles;
            _positionGetter = positionGetter;
        }
        
        public void ShiftAllTilesToLeft()
        {
            for (var i = 0; i < _tiles.Count; i++)
            {
                var tileView = _tiles[i];
                var position = _positionGetter(i);
                ShiftTile(tileView, position);
            }
        }

        public void ShiftTilesToRight(int fromIndex)
        {
            for (var i = fromIndex; i < _tiles.Count; i++)
            {
                var tileView = _tiles[i];
                var position = _positionGetter(i + 1);
                ShiftTile(tileView, position);
            }
        }

        private void ShiftTile(TileView tileView, Vector2 targetPosition)
        {
            tileView.SetTargetPosition(targetPosition);
            var flyable = tileView.GetComponent<Flyable>();
            if (flyable && !flyable.Flying)
            {
                flyable.Fly(targetPosition, Configs.GameConfig.tileShiftFlyTime);
            }
        }
    }
}