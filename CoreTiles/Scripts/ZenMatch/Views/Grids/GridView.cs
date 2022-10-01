using System;
using System.Collections.Generic;
using UnityEngine;
using ZenMatch.Controllers;
using ZenMatch.Interfaces;
using ZenMatch.Models.ScriptableObjects;
using ZenMatch.Utils;

namespace ZenMatch.Views.Grids
{
    public abstract class GridView<TGridModel> : MonoBehaviour, ITilesOwner
        where TGridModel: GridModel
    {
        public RectTransform gridTransform;
        public event Action OnTilesCountChange;
        public int TilesCount => CreatedTiles.Count;

        protected GridLayersController GridLayersController;
        protected readonly List<TileView> CreatedTiles = new();
        protected TGridModel GridModel;
        
        protected int CellSize => Configs.GameConfig.cellSize;
        protected float Width { get; set; }
        protected float Height { get; set; }
        protected float HalfCellSize => CellSize * 0.5f;
        protected TileView TileViewPrefab => Configs.GameConfig.tileViewPrefab;

        public void Init(TGridModel gridModel)
        {
            Clear();
            GridModel = gridModel;
            InitGrid(gridModel);
        }

        protected abstract void InitGrid(TGridModel gridModel);

        public virtual void CreateTiles(TGridModel gridModel, TilesSetModel tilesSetModel)
        {
        }

        public virtual void RefreshViews()
        {
        }

        public void AddComponentToCells<T>(Action<T> callback) where T : Component, IHaveCallback<T>
        {
            foreach (var cell in CreatedTiles)
            {
                T component = cell.gameObject.AddComponent<T>();
                component.AddCallback(callback);
            }
        }
        public void AddComponentToCells<T>() where T : Component
        {
            foreach (var cell in CreatedTiles)
            {
                cell.gameObject.AddComponent<T>();
            }
        }

        public void RemoveTile(TileView tileView)
        {
            if(CreatedTiles.Contains(tileView))
                CreatedTiles.Remove(tileView);
            OnTilesCountChange?.Invoke();
        }

        public void SortTiles()
        {
            CreatedTiles.Sort((tileView1, tileView2) => tileView1.Layer - tileView2.Layer);
            for (int i = 0; i < CreatedTiles.Count; i++)
            {
                CreatedTiles[i].transform.SetSiblingIndex(i);
            }
        }

        public void Clear()
        {
            foreach (var tileView in CreatedTiles) 
                Destroy(tileView.gameObject);
            CreatedTiles.Clear();
        }
    }
}