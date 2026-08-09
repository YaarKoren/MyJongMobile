using System.Collections.Generic; // List, Dictionary

[System.Serializable]
public class BoardLayoutData {
  public string shape;
  public string difficulty;
  public int tileCount;
  public int designCount;
  public int copiesPerDesign;
  public List<TileData> tiles;

  public bool Validate(out string error)
  {
    // check 1: does tiles.Count actually match tileCount
    if (tileCount != tiles.Count) {
      error = "tile count does not match actuall number of tiles";
      return false;
    }

    // check 2: does designCount * copiesPerDesign equal tileCount
    if (designCount * copiesPerDesign != tileCount) {
      error = "design count * copies per design don't match tile count";
      return false;
    }

    // check 3: does every typeId appear exactly copiesPerDesign times
    var count = new Dictionary<int, int>();
    foreach (var tile in tiles) {
      int design = tile.typeId;
      if (!count.ContainsKey(design)) {
        count[design] = 0;
      }
      ++count[design];
    }

    foreach (var kv in count) {
      if (kv.Value != copiesPerDesign) {
        error = $"design {kv.Key} appears {kv.Value} times, expected {copiesPerDesign}";
        return false;

      }
    }

    error = null;
    return true;
  }
}
