using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenController : MonoBehaviour
{
    [SerializeField] private string gameScene = "Game";
    [SerializeField] private string testServerScene = "TestServer";


    public void OnPlayButtonPressed()
    {
        GameConfig.Shape = BoardShape.Turtle;
        GameConfig.Difficulty = BoardDifficulty.Classic;

        SceneManager.LoadScene(gameScene);
    }

    public void OnTestServerButtonPressed()
    {
        SceneManager.LoadScene(testServerScene);
    }
}