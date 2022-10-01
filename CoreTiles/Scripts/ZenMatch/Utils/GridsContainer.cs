using System;
using System.Collections.Generic;
using UnityEngine;
using ZenMatch.Behaviours;
using ZenMatch.Interfaces;
using ZenMatch.Models.ScriptableObjects;
using ZenMatch.Views;
using ZenMatch.Views.Grids;
using Object = UnityEngine.Object;

namespace ZenMatch.Utils
{
    public class GridsContainer<TGridView, TGridModel> 
        where TGridView : GridView<TGridModel>
        where TGridModel : GridModel
    {
        public List<TGridView> Grids { get; } = new();
        
        private readonly TilesSetModel _tilesSetModel;
        private readonly Transform _gridsParent;

        public GridsContainer(Transform gridsParent, TilesSetModel tilesSetModel)
        {
            _gridsParent = gridsParent;
            _tilesSetModel = tilesSetModel;
        }
        
        public void Init(TGridModel gridModel, TGridView gridPrefab, Action<Clickable> onTileClick)
        {
            Clear();
            CreateGrid(gridModel, gridPrefab);
            ConfigureGridCells(onTileClick);
        }
        
        public void Init(TGridModel[] gridModels, TGridView gridPrefab, Action<Clickable> onTileClick)
        {
            Clear();
            foreach (var gridModel in gridModels) 
                CreateGrid(gridModel, gridPrefab);
            ConfigureGridCells(onTileClick);
        }

        private void CreateGrid(TGridModel gridModel, TGridView gridPrefab) 
        {
            var gridView = Object.Instantiate(gridPrefab, _gridsParent, false);
            gridView.Init(gridModel);
            gridView.CreateTiles(gridModel, _tilesSetModel);
            gridView.SortTiles();
            Grids.Add(gridView);
        }

        private void ConfigureGridCells(Action<Clickable> onTileClick)
        {
            AddComponentToCells<Clickable>(onTileClick);
            AddComponentToCells<Flyable>();
            RefreshViews();
        }

        public void TryRemoveTile(TileView tileView)
        {
            Grids.ForEach(gridView => gridView.RemoveTile(tileView));
        }

        public void RefreshViews()
        {
            Grids.ForEach(gridView => gridView.RefreshViews());
        }

        public void Clear()
        {
            foreach (var grid in Grids) 
                Object.Destroy(grid.gameObject);
            Grids.Clear();
        }

        private void AddComponentToCells<T>(Action<T> onClickCallback) where T : Component, IHaveCallback<T>
        {
            Grids.ForEach(gridView => gridView.AddComponentToCells<T>(onClickCallback));
        }

        private void AddComponentToCells<T>() where T : Component
        {
            Grids.ForEach(gridView => gridView.AddComponentToCells<T>());
        }
    }
}