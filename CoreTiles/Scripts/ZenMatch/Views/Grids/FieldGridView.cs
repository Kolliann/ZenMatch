using CoreTiles.Scripts.ZenMatch.Models.ScriptableObjects;
using UnityEngine;
using ZenMatch.Controllers;
using ZenMatch.Models.ScriptableObjects;
using ZenMatch.Utils;

namespace ZenMatch.Views.Grids
{
    public class FieldGridView : GridView<FieldGridModel>
    {
        protected override void InitGrid(FieldGridModel gridModel)
        {
            Width = (gridModel.size.y - 1) * gridModel.offsets.x;
            Height = (gridModel.size.x - 1) * gridModel.offsets.y;
            gridTransform.anchoredPosition = gridModel.position;
            gridTransform.sizeDelta = new Vector2(Width, Height);
        }
        
        public override void CreateTiles(FieldGridModel gridModel, TilesSetModel tilesSetModel)
        {
            foreach (var cell in gridModel.cells.cells)
            {
                var x = -Width * 0.5f + cell.position.x * GridModel.offsets.x;
                var y = Height * 0.5f - cell.position.y * GridModel.offsets.y;
                
                var tileView = Instantiate(TileViewPrefab, Vector3.zero, Quaternion.identity, gridTransform);
                tileView.InitVisual(tilesSetModel.tiles[cell.tileId], cell.layer);
                tileView.InitColors(Configs.GameConfig.normalColor, Configs.GameConfig.fieldFadeColor);
                CreatedTiles.Add(tileView);

                tileView.transform.localPosition = Vector3.zero;
                tileView.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
                tileView.name = $"Tile [{cell.position.x},{cell.position.y}] {tilesSetModel.tiles[cell.tileId].name}";
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