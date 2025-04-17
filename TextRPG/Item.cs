using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Potion
    }
    public class Item
    {
        public string Name;
        public ItemType Type; // "무기", "방어구", "소모품" 등
        public int Attack;
        public int Defense;
        public string Description;
        public int Price;
        public int SellPrice => (int)(Price * 0.85);
        public bool IsEquipped = false;

        public Item(string name, ItemType type, int atk, int def, string description, int price)
        {
            Name = name;
            Type = type;
            Attack = atk;
            Defense = def;
            Price = price;
            Description = description;
        }
    }

    

    public class ShopItem : Item
    {
        public bool IsPurchased = false;

        public ShopItem(string name, ItemType type, int atk, int def, string description, int price)
           : base(name, type, atk, def, description, price) { }
        
    }
}
