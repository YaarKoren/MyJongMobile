using UnityEngine;
using UnityEngine.EventSystems; // IPointerClickHandler

public class TileView : MonoBehaviour, IPointerClickHandler
{
    public TileData Data { get; private set; }
    
    private BoardManager _board;
    
    [SerializeField] private SpriteRenderer artRenderer;
    [SerializeField] private SpriteRenderer highlightRenderer;

    // fit the art to board
    private float _artScale = 1f;
    
    public void Init(TileData data, Sprite artSprite, float artScale, BoardManager board)
    {
        Data = data;
        artRenderer.sprite = artSprite;
        _artScale = artScale;
        _board = board;

        // fit the art to board
        artRenderer.transform.localScale = Vector3.one * artScale;

    }   

    // what happens when this tile is tapped
    // BoardManager decides it the tile is selected, not the tile
    // so, update BoardManager
    public void OnPointerClick(PointerEventData eventData) {
        _board.OnTileTapped(this);
    }


    public void SetSelected(bool value) {
        // highlight the edges of the tile
        //highlightRenderer.enabled = value;
        
        // make the tile larger, keep fitted to board
        float scale = value ? _artScale * 1.15f : _artScale;
        transform.localScale = Vector3.one * scale;
    }
}
