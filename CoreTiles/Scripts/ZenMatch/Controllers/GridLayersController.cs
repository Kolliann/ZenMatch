using System.Collections.Generic;
using UnityEngine;
using ZenMatch.Views;

namespace ZenMatch.Controllers
{
    public class GridLayersController
    {
        // TODO: для удобного и оптимизированного обхода клеток сюда подойдут деревья
        // у каждой открытой клетки есть несколько соседей, которые этой клеткой могут быть открыты
        // но не в прототипе =)
        
        private readonly List<TileView> _tiles;
        private readonly Vector2Int _offsets;
        private readonly int _cellSize;

        public GridLayersController(List<TileView> createdTiles, Vector2Int offsets, int cellSize)
        {
            _tiles = createdTiles;
            _offsets = offsets;
            _cellSize = cellSize;
        }

        public void InitLayers()
        {
            // оффсеты больше чем размер клетки, клетки не пересекаются
            if (_offsets.x >= _cellSize && _offsets.y >= _cellSize)
                return;

            Vector2 offsets = _offsets;
            var clippingDelta = 2 * Mathf.Sqrt(
                Mathf.Pow(offsets.x * 0.5f, 2)
                + Mathf.Pow(offsets.y * 0.5f, 2));
                
            foreach (var tileView in _tiles)
            {
                tileView.Show();
                
                for (int i = 0; i < _tiles.Count; i++)
                {
                    if (tileView == _tiles[i])
                        continue;
                    var distance = Vector2.Distance(tileView.Position, _tiles[i].Position);
                    // клетки пересекаются
                    if (distance <= clippingDelta)
                    {
                        if (tileView.Layer < _tiles[i].Layer)
                        {
                            tileView.Fade();
                            break;
                        }
                    }
                }
            }
        }

        public void RefreshLayers()
        {
            // TODO: optimize
            InitLayers();
        }
        
    }
}