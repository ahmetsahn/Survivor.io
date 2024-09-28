using Script.Runtime.Enum;
using Script.Runtime.WeaponModule;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.Runtime.CollectableModule.ItemModule.Model
{
    public abstract class ItemSo : ScriptableObject
    {
        public ItemType ItemType;
        
        public Sprite Icon;
        
        [TextArea(3, 10)]
        public string DetailsText;
        
        public Sprite[] CustomizationSprites;
        
        public void Customization(SpriteRenderer[] spriteRenderers)
        {
            for (int i = 0; i < CustomizationSprites.Length; i++)
            {
                spriteRenderers[i].sprite = CustomizationSprites[i];
            }
        }
        
        public void CustomizationUI(Image[] image)
        {
            for (int i = 0; i < CustomizationSprites.Length; i++)
            {
                image[i].sprite = CustomizationSprites[i];
            }
        }
        
        public virtual PlayerWeaponSo GetWeaponSo()
        {
            return null;
        }
    }
}