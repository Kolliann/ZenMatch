using UnityEngine;
using UnityEngine.UI;
using ZenMatch.Behaviours;
using ZenMatch.Enums;
using ZenMatch.Models.ScriptableObjects;
using ZenMatch.Utils;

namespace ZenMatch.Views
{
    public class TileView : MonoBehaviour
    {
        private int CellSize => Configs.GameConfig.cellSize;
        private TileViewState _tileViewState = TileViewState.Default;

        private RectTransform _rectTransform;
        private Graphic[] _graphics;
        private Color _normalColor;
        private Color _fadeColor;

        public int Layer { get; private set; }
        public TileModel TileModel { get; private set; }
        public Vector2 Position => _rectTransform.anchoredPosition;
        
        /// <summary>
        /// Финальная позиция объекта в матч-панельке
        /// </summary>
        public Vector2 TargetPosition { get; private set; }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.sizeDelta = new Vector2(CellSize, CellSize);
        }
        
        // TODO: Grid -> Tile
        // TODO: TileParams: Position, Colors, layer, etc
        public void Init(Vector2 position)
        {
            _rectTransform.anchoredPosition = position;
        }
        
        public void SetTargetPosition(Vector2 targetPosition)
        {
            TargetPosition = targetPosition;
        }
        
        public void InitVisual(TileModel tileModel, int layer)
        {
            TileModel = tileModel;
            Layer = layer;
            var visual = Instantiate(tileModel.icon, Vector3.zero, Quaternion.identity, transform);
            visual.transform.localPosition = Vector3.zero;
            visual.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            _graphics = visual.GetComponentsInChildren<Graphic>();
        }

        public void InitColors(Color normalColor, Color fadeColor)
        {
            _normalColor = normalColor;
            _fadeColor = fadeColor;
        }
        
        public void Fade()
        {
            if (_tileViewState == TileViewState.Faded)
                return;
            foreach (var graphic in _graphics)
            {
                graphic.color = _fadeColor;
            }
            _tileViewState = TileViewState.Faded;
        }
        
        public void Show()
        {
            if (_tileViewState == TileViewState.Showed)
                return;
            foreach (var graphic in _graphics)
            {
                graphic.color = _normalColor;
            }
            _tileViewState = TileViewState.Showed;
        }
        
        public void UpdateClickable()
        {
            GetComponent<Clickable>().SetInteractable(_tileViewState == TileViewState.Showed);
        }
    }
}