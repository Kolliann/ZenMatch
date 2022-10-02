using System;
using CoreTiles.Scripts.ZenMatch.Controllers;
using CoreTiles.Scripts.ZenMatch.Models.ScriptableObjects;
using UnityEngine;
using ZenMatch.Animatons;
using ZenMatch.Behaviours;
using ZenMatch.Interfaces;
using ZenMatch.Models;
using ZenMatch.Models.ScriptableObjects;
using ZenMatch.Utils;
using ZenMatch.Views;
using ZenMatch.Views.Grids;

namespace ZenMatch.Controllers
{
    public class GridContainersController : MonoBehaviour
    {
        public Transform gridsParent;
        public CollectorGridView collectorGrid;
        public FieldGridView fieldGridPrefab;
        public StackGridView stackGridPrefab;

        private GridsContainer<FieldGridView, FieldGridModel> _fieldGrids;
        private GridsContainer<StackGridView, StackGridModel> _stackGrids;

        public void Init(GridsData gridsData)
        {
            collectorGrid.Clear();
            _fieldGrids?.Clear();
            _stackGrids?.Clear();
            
            _fieldGrids = new GridsContainer<FieldGridView, FieldGridModel>(gridsParent, gridsData.tilesSetModel);
            _fieldGrids.Init(gridsData.fieldGridModel, fieldGridPrefab, OnTileClick);
            
            _stackGrids = new GridsContainer<StackGridView, StackGridModel>(gridsParent, gridsData.tilesSetModel);
            _stackGrids.Init(gridsData.stackGrids, stackGridPrefab, OnTileClick);

            collectorGrid.Init(Configs.GameConfig.collectorGridModel);
            collectorGrid.AddCallback(OnTilesMatched);
        }

        public void InitFinishGameController(FinishGameController finishGameController)
        {
            var fieldTilesOwners = _fieldGrids.Grids.ConvertAll(
                new Converter<FieldGridView, ITilesOwner>(gridView => gridView));
            var stackTilesOwners = _stackGrids.Grids.ConvertAll(
                new Converter<StackGridView, ITilesOwner>(gridView => gridView));

            finishGameController.Clear();
            finishGameController.AddTilesOwners(fieldTilesOwners);
            finishGameController.AddTilesOwners(stackTilesOwners);
            finishGameController.AddTilesCollector(collectorGrid);
        }

        private void OnTileClick(Clickable clickable)
        {
            if (clickable.ClickState == Clickable.State.Clicked)
            {
                if (collectorGrid.CanInsert)
                {
                    var tileView = clickable.GetComponent<TileView>();
                    RemoveTileFromGrid(tileView);
                    RefreshViews();
                    collectorGrid.InsertTile(tileView);
                }
            }
            else
            {
                var animator = clickable.GetComponent<TileAnimator>();
                if(animator)
                    animator.AnimatePress(clickable.ClickState == Clickable.State.Pressed);
            }
        }

        private void OnTilesMatched(TileModel matchedTileModel)
        {
            CoreGameController.SoundController.PlayFoodSound(matchedTileModel);
            CoreGameController.InvokeOnMatchEvent(matchedTileModel);
        }

        private void RemoveTileFromGrid(TileView tileView)
        {
            _fieldGrids.TryRemoveTile(tileView);
            _stackGrids.TryRemoveTile(tileView);
        }

        private void RefreshViews()
        {
            _fieldGrids.RefreshViews();
            _stackGrids.RefreshViews();
        }

    }
}