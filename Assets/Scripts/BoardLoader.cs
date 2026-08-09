using UnityEngine;

public static class BoardLoader
{
    /**
     * use BoardCatalog to get the board path.
     * if that fails, log an error and return null.
     */
    public static string GetBoardPath() 
    {
        string path;

        if (!BoardCatalog.TryGetPath(GameConfig.BoardKey, out path)) {
            Debug.LogError($"No board registered for key '{GameConfig.BoardKey}'");
            return null;
        }

        return path;
    }

    /**
     * use Resources.Load<TextAsset>(path) to get the raw JSON text, describing the board.
     * if that comes back null (file genuinely missing despite the catalog saying otherwise),
     * log an error and return null.
     */
    public static string GetRawBoard(string path)
    {
        TextAsset res = Resources.Load<TextAsset>(path);

        if (res == null) {
            Debug.LogError($"No file in path '{path}'");
            return null;
        }

        return res.text;
    }

    /**
     * use JsonUtility.FromJson<BoardLayoutData>(text   ) to parse it.
     * call .Validate(out error) on the result 
     * if that fails, log the error and return null.
     * otherwise, return the parsed, validated BoardLayoutData.
     */
    public static BoardLayoutData Load()
    {
        string path = GetBoardPath();

        if (path == null) {
            return null;
        }

        string boardText = GetRawBoard(path);

        if (boardText == null) {
            return null;
        }

        // parse the board text
        BoardLayoutData board = JsonUtility.FromJson<BoardLayoutData>(boardText);

        // validate
        string error;
        if (!board.Validate(out error)) {
            Debug.LogError(error);
            return null;
        }

        return board;
    }

}
