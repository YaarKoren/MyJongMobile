using UnityEngine;
using System.Collections.Generic; // List

public class GameScreenController : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private List<Sprite> defaultArt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boardManager.OnWin += HandleWin;

        BoardLayoutData boardData = BoardLoader.Load(); 

        if (boardData == null) {
            Debug.LogError("error in loading board");
            return;
        }

        TileSpriteProvider spriteProvider = new TileSpriteProvider(defaultArt);

        boardManager.BuildBoard(boardData, spriteProvider);

        
    }

    private void HandleWin()
    {
        Debug.Log("You won!");
        // TODO: show a win panel, play a sound, etc.
    }

}
