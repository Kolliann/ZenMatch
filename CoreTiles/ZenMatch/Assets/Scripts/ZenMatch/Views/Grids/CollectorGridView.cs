using System;
using System.Collections.Generic;
using System.Linq;
using CoreTiles.Scripts.ZenMatch.Controllers;
using UnityEngine;
using ZenMatch.Animatons;
using ZenMatch.Behaviours;
using ZenMatch.Controllers;
using ZenMatch.Enums;
using ZenMatch.Interfaces;
using ZenMatch.Models.ScriptableObjects;
using ZenMatch.Utils;

namespace ZenMatch.Views.Grids
{
    public class CollectorGridView : GridView<CollectorGridModel>,
        IHaveCallback<TileModel>, ITilesCollector
    {
        public GameObject fxHolder;
        public event Action OnTilesFull;

        public bool CanInsert => CreatedTiles.Count - _matchesController.MatchedTiles.Count < GridModel.size.y;

        private Action<TileModel> _onTilesMacthedCallback;
        private MatchesController _matchesController;
        private GridTilesShifter _gridTilesShifter;

        protected override void InitGrid(CollectorGridModel gridModel)
        {
            Width = gridModel.size.y * (gridModel.cellSize.x + gridModel.spacing);
            Height = gridModel.size.x * gridModel.cellSize.y;
            gridTransform.sizeDelta = new Vector2(Width, Height);

            var fxHolderRect = fxHolder.GetComponent<RectTransform>();
            if (fxHolderRect)
            {
                fxHolderRect.anchoredPosition = gridModel.position;
                fxHolderRect.sizeDelta = new Vector2(Width, Height);
            }

            _matchesController = new MatchesController(CreatedTiles, fxHolder);
            _gridTilesShifter = new GridTilesShifter(CreatedTiles, GetTilePosition);
        }

        public void AddCallback(Action<TileModel> onTilesMatchedCallback)
        {
            _onTilesMacthedCallback = onTilesMatchedCallback;
        }

        public void InsertTile(TileView tileView)
        {
            Vector2 nextPosition;
            if (HasSameTiles(tileView.TileModel))
            {
                var lastSameTile = CreatedTiles.LastOrDefault(tile => tile.TileModel == tileView.TileModel);
                var insertTileIndex = CreatedTiles.LastIndexOf(lastSameTile) + 1;
                if (insertTileIndex < CreatedTiles.Count)
                {
                    _gridTilesShifter.ShiftTilesToRight(insertTileIndex);
                    nextPosition = GetTilePosition(insertTileIndex);
                    CreatedTiles.Insert(insertTileIndex, tileView);
                }
                else
                {
                    nextPosition = GetTilePosition(CreatedTiles.Count);
                    CreatedTiles.Add(tileView);
                }
            }
            else
            {
                nextPosition = GetTilePosition(CreatedTiles.Count);
                CreatedTiles.Add(tileView);
            }

            ConfigureInsertedTile(tileView, nextPosition);
        }

        private void ConfigureInsertedTile(TileView tileView, Vector2 position)
        {
            tileView.SetTargetPosition(position);
            tileView.transform.SetParent(gridTransform);
            var flyable = tileView.GetComponent<Flyable>();
            if (flyable)
            {
                flyable.Fly(position, Configs.GameConfig.tileCollectFlyTime, EndFlyCallback);
            }
            var clickable = tileView.GetComponent<Clickable>();
            if (clickable)
            {
                clickable.SetInteractable(false);
            }

            // TODO
            // TilesController
            // Collect, ITile<State>
            // Collector
            // GetTargetPos

            _matchesController.CheckMatches();
        }

        private void EndFlyCallback(Flyable flyable)
        {
            var tileAnimator = flyable.GetComponent<TileAnimator>();
            if (tileAnimator)
            {
                tileAnimator.AnimateInsert(TilePreInsertedCallback, TileInsertedCallback);
            }
        }

        private void TilePreInsertedCallback(TileAnimator tileAnimator)
        {
            CoreGameController.SoundController.PlayActionSound(SoundActionType.TilePlaced);
            if (_matchesController.HasMatches)
            {
                _matchesController.StartDestroyMatchedTiles(TileDestroyedCallback, ClearMatchedTilesCallback);
            }
        }

        private void TileInsertedCallback(TileAnimator tileAnimator)
        {
            CheckFinishGameFieldIsFull();
        }

        private void TileDestroyedCallback(TileAnimator tileAnimator)
        {
            Destroy(tileAnimator.gameObject);
            _gridTilesShifter.ShiftAllTilesToLeft();
            // TODO: FlyController.UpdateTargets
        }

        private void ClearMatchedTilesCallback(List<TileView> matchedTiles, TileModel matchedTileModel)
        {
            CreatedTiles.RemoveAll(matchedTiles.Contains);
            matchedTiles.Clear();
            _onTilesMacthedCallback?.Invoke(matchedTileModel);
        }

        private void CheckFinishGameFieldIsFull()
        {
            if (!CanInsert)
            {
                OnTilesFull?.Invoke();
            }
        }

        private Vector2 GetTilePosition(int index)
        {
            var pos = new Vector2(
                -Width * 0.5f + HalfCellSize + index * (CellSize + GridModel.spacing),
                gridTransform.anchoredPosition.y - 12); // отступ бэкграунда
            var worldPos = Configs.GameController.Canvas.transform.TransformPoint(pos);
            return worldPos;
        }

        private bool HasSameTiles(TileModel tileModel)
        {
            return CreatedTiles.Any(tile => tile.TileModel == tileModel);
        }
    }
}