
using UnityEngine;
using System.Collections.Generic; // Dictionary

public class BoardManager : MonoBehaviour
{
    // original, untouched loaded data    
    public BoardLayoutData Data { get; private set; }
    
    // dictionaty to keep existing tiles on board
    private Dictionary<int, TileView> _idToTile;

    private TileView _selectedTile;   // null = nothing selected
    
    public System.Action OnWin;

    [SerializeField] private TileView tilePrefab;

    //grid step (in JSON units) -> world-space distance, per tile
    [SerializeField] private float unitSize = 0.5f; 

    // fileds to keep the board centered
    private float _centerX;
    private float _centerY;
    
    // per-layer diagonal offset, for the "stacked" look
    [SerializeField] private float layerVisualOffset = 0.12f; 

    public void BuildBoard(BoardLayoutData layout, TileSpriteProvider spriteProvider) 
    {
        GetCenterCoords(layout);

        _idToTile = new Dictionary<int, TileView>();

        foreach (var tileData in layout.tiles) {
            var tileView = Instantiate(tilePrefab, transform);

            // position the tile
            float visualOffset = tileData.layer * layerVisualOffset;

            tileView.transform.localPosition = new Vector3(
                (tileData.x - _centerX) * unitSize + visualOffset,
                (tileData.y - _centerY) * unitSize + visualOffset,
                -tileData.layer * 0.01f
            );

            // initialize the tile with its picture
            tileView.Init(tileData, spriteProvider.GetSpriteForType(tileData.typeId), this);
            
            // insert to the dictionary
            _idToTile[tileData.id] = tileView;
        }
        
    }

    private bool IsTileCovered(TileView tile)
    {
        var x = tile.Data.x;
        var y = tile.Data.y;
        var layer = tile.Data.layer;
        
        foreach (var otherTile in _idToTile.Values) {
            var otherX = otherTile.Data.x;
            var otherY = otherTile.Data.y;
            var otherLayer = otherTile.Data.layer;

            if (x == otherX && y == otherY && layer < otherLayer) {
                return true;
            }
        }

        return false;
    }

    private bool IsTileBlocked(TileView tile)
    {
        var x = tile.Data.x;
        var y = tile.Data.y;
        var layer = tile.Data.layer;

        bool leftOccupied = false;
        bool rightOccupied = false;

        
         foreach (var otherTile in _idToTile.Values)
        {
            var otherX = otherTile.Data.x;
            var otherY = otherTile.Data.y;
            var otherLayer = otherTile.Data.layer;

            if (otherY == y && otherLayer == layer)
            {
                if (otherX == x - 2) {
                    leftOccupied = true;
                } 
                if (otherX == x + 2) {
                    rightOccupied = true;
                } 
            }
        }

        return leftOccupied && rightOccupied;
    }

    private void RemoveTile(TileView tile)
    {
        // remove it from _idToTile
        _idToTile.Remove(tile.Data.id);
        
        // destroy its GameObject
        Destroy(tile.gameObject);
    }

    public void OnTileTapped(TileView tile)
    {
        // the tile is covered or blocked -> ignore
        if (IsTileCovered(tile) || IsTileBlocked(tile)) {
            return;
        }

        // the tile is already selected -> unselect
        if (_selectedTile == tile) {
            tile.SetSelected(false);
            _selectedTile = null;
            return;
        }

        // no tile is selected -> select
        if (_selectedTile == null) {
            tile.SetSelected(true);
            _selectedTile = tile;
            return;
        }

        // different tile is selected  -> check fot match
  
        if (_selectedTile.Data.typeId == tile.Data.typeId) { // match
            
            RemoveTile(_selectedTile);
            RemoveTile(tile);
            _selectedTile = null;

            if (_idToTile.Count == 0) {
                OnWin?.Invoke();
            }

        } else {                                            // not a match
            
            // unselect current select, select the new one
            var other = _selectedTile;
            other.SetSelected(false);
            tile.SetSelected(true);
            _selectedTile = tile;
        }

    }

    private void GetCenterCoords(BoardLayoutData layout) 
    {
        int minX = int.MaxValue;
        int maxX = int.MinValue;    
        int minY = int.MaxValue; 
        int maxY = int.MinValue;

        foreach (var t in layout.tiles) {
            minX = Mathf.Min(minX, t.x);
            maxX = Mathf.Max(maxX, t.x);
            minY = Mathf.Min(minY, t.y);
            maxY = Mathf.Max(maxY, t.y);
        }

        _centerX = (minX + maxX) / 2f;
        _centerY = (minY + maxY) / 2f;
    }
}
