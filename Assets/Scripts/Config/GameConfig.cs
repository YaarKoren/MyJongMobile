using UnityEngine;

public enum BoardShape {
    Turtle,
    Cat
}

public enum BoardDifficulty {
    Easy,
    Classic
}

public static class GameConfig  {
    public static BoardShape Shape = BoardShape.Turtle;
    public static BoardDifficulty Difficulty = BoardDifficulty.Classic;

    public static int ArtSetIndex = 0;

    public static string BoardKey => $"{Shape}_{Difficulty}".ToLowerInvariant();
}


