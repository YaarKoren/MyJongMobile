using UnityEngine;
using System.Collections.Generic; // List
using UnityEngine.SceneManagement;

public class GameScreenController : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private List<TileArtSet> _artSets;

    [SerializeField] private string homeScene = "Home";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boardManager.OnWin += HandleWin;

        var chosenSet = _artSets[GameConfig.ArtSetIndex];

        BoardLayoutData boardData = BoardLoader.Load(); 

        if (boardData == null) {
            Debug.LogError("error in loading board");
            return;
        }

        var spriteProvider = new TileSpriteProvider(chosenSet.sprites);

        boardManager.BuildBoard(boardData, spriteProvider, chosenSet.artScale);

        
    }

    private void HandleWin()
    {
        Debug.Log("You won!");
        // TODO: show a win panel, play a sound, etc.
    }

    public void OnHomeButtonPressed()
    {
        SceneManager.LoadScene(homeScene);
    }

}
