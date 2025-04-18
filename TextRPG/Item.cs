using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    // 아이템의 종류를 나타내는 열거형
    public enum ItemType
    {
        Weapon, //무기
        Armor   //방어구
    }

    //아이템 클래스
    public class Item
    {
        public string Name;     //아이템 이름
        public ItemType Type;   //아이템 타입
        public float Attack;    //공격력 수치
        public int Defense;     //방어력 수치
        public string Description; //설명
        public int Price;       //구매 가격
        public int SellPrice => (int)(Price * 0.85);//판매 가격(85%)
        public bool IsEquipped = false; //장착여부

        //생성자
        public Item(string name, ItemType type, float atk, int def, string description, int price)
        {
            Name = name;
            Type = type;
            Attack = atk;
            Defense = def;
            Price = price;
            Description = description;
        }
    }

    //상점에서 구매 가능한 아이템
    public class ShopItem : Item
    {
        public bool IsPurchased = false; //구매 여부

        //상점 아이템도 Item의 모든 속성 그대로 사용
        public ShopItem(string name, ItemType type, float atk, int def, string description, int price)
           : base(name, type, atk, def, description, price) { }
        
    }
}
