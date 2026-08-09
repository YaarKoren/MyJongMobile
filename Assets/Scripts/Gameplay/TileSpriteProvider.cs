using UnityEngine;
using System.Collections.Generic;

public class TileSpriteProvider
{
    // TODO
    //private readonly List<Sprite> _userPhotos;
    
    private readonly List<Sprite> _defaultArt;

    public TileSpriteProvider(List<Sprite> defaultArt)
    {
        //_userPhotos = userPhotos;
        _defaultArt = defaultArt;
    }

    public Sprite GetSpriteForType(int typeId) 
    {
        return _defaultArt[typeId];
    }
}
