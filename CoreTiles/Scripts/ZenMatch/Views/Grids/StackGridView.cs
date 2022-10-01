using UnityEngine;
using ZenMatch.Controllers;
using ZenMatch.Models.ScriptableObjects;
using ZenMatch.Utils;

namespace ZenMatch.Views.Grids
{
    public class StackGridView : GridView<StackGridModel>
    {
        protected override void InitGrid(StackGridModel gridModel)
        {
            Width = CellSize;
            Height = CellSize;
            gridTransform.anchoredPosition = gridModel.position;
            gridTransform.sizeDelta = new Vector2(Width, Height);
        }

        public override void CreateTiles(StackGridModel gridModel, TilesSetModel tilesSetModel)
        {
            int layer = gridModel.tiles.Length;
            var position = new Vector2(-Width * 0.5f + HalfCellSize, Height * 0.5f - HalfCellSize);
            foreach (var tileId in gridModel.tiles)
            {
                var tileView = Instantiate(TileViewPrefab, gridTransform, false);
                tileView.transform.localPosition = Vector3.zero;
                tileView.GetComponent<RectTransform>().anchoredPosition = position;
                tileView.InitVisual(tilesSetModel.tiles[tileId], layer);
                tileView.InitColors(Configs.GameConfig.normalColor, Configs.GameConfig.stackFadeColor);
                CreatedTiles.Add(tileView);
                tileView.name = $"Tile [{layer}] {tilesSetModel.tiles[tileId].name}";
                position += gridModel.offsets;
                layer--;
            }
            
            GridLayersController = new GridLayersController(CreatedTiles, GridModel.offsets, CellSize);
            GridLayersController.InitLayers();
        }
        
        public override void RefreshViews()
        {
            GridLayersController.RefreshLayers();
            CreatedTiles.ForEach(tile => tile.UpdateClickable());
        }
    }
}