using System;
using UnityEngine;

namespace Script.Runtime
{
    public class GameAssets
    {
        public Sprite[] DefaultHoodSprites;
        public Sprite[] DefaultArmorSprites;
        public Sprite[] DefaultWeaponSprites;
        public Sprite[] DefaultShoesSprites;
        
        public GameAssets(GameAssetsConfig config)
        {
            DefaultHoodSprites = config.DefaultHoodSprites;
            DefaultArmorSprites = config.DefaultArmorSprites;
            DefaultWeaponSprites = config.DefaultWeaponSprites;
            DefaultShoesSprites = config.DefaultShoesSprites;
        }
    }
    
    [Serializable]
    public struct GameAssetsConfig
    {
        public Sprite[] DefaultHoodSprites;
        public Sprite[] DefaultArmorSprites;
        public Sprite[] DefaultWeaponSprites;
        public Sprite[] DefaultShoesSprites;
    }
}