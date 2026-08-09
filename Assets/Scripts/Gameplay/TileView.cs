using UnityEngine;
using UnityEngine.EventSystems; // IPointerClickHandler

public class TileView : MonoBehaviour, IPointerClickHandler
{
    public TileData Data { get; private set; }
    
    private BoardManager _board;
    
    [SerializeField] private SpriteRenderer artRenderer;
    [SerializeField] private SpriteRenderer highlightRenderer;
    
    public void Init(TileData data, Sprite artSprite, BoardManager board)
    {
        Data = data;
        _board = board;
        artRenderer.sprite = artSprite;
    }   

    // what happens when this tile is tapped
    // BoardManager decides it the tile is selected, not the tile
    // so, update BoardManager
    public void OnPointerClick(PointerEventData eventData) {
        _board.OnTileTapped(this);
    }


    public void SetSelected(bool value) {
        highlightRenderer.enabled = value;
        
        // make the tile larger
        transform.localScale = value ? new Vector3(1.15f, 1.15f, 1f) : Vector3.one;
    }
}
