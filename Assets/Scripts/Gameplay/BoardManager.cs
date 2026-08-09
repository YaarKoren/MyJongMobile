using UnityEngine;
using System.Collections.Generic; // Dictionary

public class BoardManager : MonoBehaviour
{
    // original, untouched loaded data    
    public BoardLayoutData Data { get; private set; }
    
    // dictionaty to keep existing tiles on board
    private Dictionary<int, TileView> _idToTile;

    private TileView _selectedTile;   // null = nothing selected
    
    [SerializeField] private TileView tilePrefab;

    //grid step (in JSON units) -> world-space distance, per tile
    [SerializeField] private float unitSize = 0.5f; 

    public void BuildBoard(BoardLayoutData layout, TileSpriteProvider spriteProvider) 
    {
        _idToTile = new Dictionary<int, TileView>();

        foreach (var tileData in layout.tiles) {
            var tileView = Instantiate(tilePrefab, transform);

            // position the tile
            tileView.transform.localPosition = new Vector3(
                tileData.x * unitSize,
                tileData.y * unitSize,
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

    private void RemoveTile(TileView tile)
    {
        // remove it from _idToTile
        _idToTile.Remove(tile.Data.id);
        
        // destroy its GameObject
        Destroy(tile.gameObject);
    }

    public void OnTileTapped(TileView tile)
    {
        // the tile is covered -> ignore
        if (IsTileCovered(tile)) {
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
  
        if (_selectedTile.Data.typeId == tile.Data.typeId) { // natch
            
            RemoveTile(_selectedTile);
            RemoveTile(tile);
            _selectedTile = null;

        } else {                                            // not a match
            
            // unselect current select, select the new one
            var other = _selectedTile;
            other.SetSelected(false);
            tile.SetSelected(true);
            _selectedTile = tile;
        }

        return;
        

    }
}
