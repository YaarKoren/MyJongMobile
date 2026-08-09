using System.Collections.Generic; // Dictionary

public static class BoardCatalog
{
    private static readonly Dictionary<string, string> BoardPaths = new()
    {
        { "turtle_classic", "Boards/turtle_classic" },
    };
    
    public static bool TryGetPath(string boardKey, out string resourcePath) 
    {
        return BoardPaths.TryGetValue(boardKey, out resourcePath);
    }
}
