public enum BoardShape {
    ShapeTurtle,
    ShapeCat
}

public enum BoardDifficulty {
    DifficultyEasy,
    DifficultyClassic
}

public static class GameConfig  {
    public static BoardShape Shape; 
    public static BoardDifficulty Difficulty;

    public static string BoardKey => $"{Shape}_{Difficulty}".ToLowerInvariant();
}


