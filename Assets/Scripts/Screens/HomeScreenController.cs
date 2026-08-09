using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenController : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Game";

    public void OnPlayButtonPressed()
    {
        GameConfig.Shape = BoardShape.Turtle;
        GameConfig.Difficulty = BoardDifficulty.Classic;

        SceneManager.LoadScene(sceneToLoad);
    }
}