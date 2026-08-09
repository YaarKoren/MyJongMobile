using UnityEngine;
using System.Collections.Generic;

public class TileSpriteProvider
{
    private readonly List<Sprite> _userPhotos;
    private readonly List<Sprite> _defaultArt;
    private readonly Dictionary<int, Sprite> _cache;

    public TileSpriteProvider(List<Sprite> userPhotos, List<Sprite> defaultArt)
    {
        _userPhotos = userPhotos;
        _defaultArt = defaultArt;
    }

    public Sprite GetSpriteForType(int typeId) 
    {
        Sprite sprite;
        sprite = _userPhotos.Count > 0
            ? _userPhotos[typeId % _userPhotos.Count]
            : _defaultArt[typeId % _defaultArt.Count];

        return sprite;
    }
}
