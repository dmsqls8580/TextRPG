using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Shop
    {
        //후에 개별 참조를 위해 따로 변수 선언
        static ShopItem old_sword = new ShopItem("낡은 검", ItemType.Weapon, 5, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600);
        static ShopItem bronze_ax = new ShopItem("청동 도끼", ItemType.Weapon, 7, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500);
        static ShopItem sparta_spear = new ShopItem("스파르타의 창", ItemType.Weapon, 10, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000);

        static ShopItem double_dagger = new ShopItem("이중 단검", ItemType.Weapon, 12, 0, "두 개의 단검으로 빠른 연속 공격이 가능합니다.", 3500);
        static ShopItem mana_staff = new ShopItem("마나 지팡이", ItemType.Weapon, 14, 0, "마력을 증폭시켜주는 전용 지팡이입니다.", 3800);
        static ShopItem precisior_bow = new ShopItem("정확의 활", ItemType.Weapon, 15, 0, "한 발 한 발이 치명적인 정밀 활입니다.", 4000);

        static ShopItem novice_armor = new ShopItem("수련자 갑옷", ItemType.Armor, 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000);
        static ShopItem iron_armor = new ShopItem("무쇠 갑옷", ItemType.Armor, 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000);
        static ShopItem sparta_armor = new ShopItem("스파르타의 갑옷", ItemType.Armor, 0, 17, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500);

        static ShopItem agility_vest = new ShopItem("민첩의 조끼", ItemType.Armor, 0, 12, "움직임을 방해하지 않는 가벼운 조끼입니다.", 3000);
        static ShopItem hunter_clothes = new ShopItem("사냥꾼의 가죽옷", ItemType.Armor, 0, 15, "기동성과 방어력을 겸비한 가죽옷입니다.", 3200);
        static ShopItem magic_robe = new ShopItem("마법 로브", ItemType.Armor, 0, 17, "마법 방어에 특화된 마도사의 로브입니다.", 3500);


        static List<ShopItem> shopItems = new List<ShopItem>()
        {
            old_sword, bronze_ax, sparta_spear, double_dagger, mana_staff, precisior_bow, novice_armor, iron_armor, sparta_armor, agility_vest, hunter_clothes, magic_robe
        };

        public static void OpenShop()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine($"[보유 골드] {Game.player.Gold} G\n");
                Console.WriteLine("[아이템 목록]");

                Console.WriteLine($"{"번호",-4} {"이름",-15} {"효과",-12} {"설명",-40} {"가격"}");
                Console.WriteLine(new string('-', 80));

                for (int i = 0; i < shopItems.Count; i++)
                {
                    var item = shopItems[i];
                    string status = item.IsPurchased ? "구매완료" : $"{item.Price} G";
                    string bonus = item.Attack > 0 ? $"공격력 +{item.Attack}" : $"방어력 +{item.Defense}";

                    Console.WriteLine($"- {i + 1,-4} {item.Name,-15} {bonus,-12} {item.Description,-40} {status}");
                }

                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");
                Console.Write(">> ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        Game.EnterVillage();
                        break;
                    case "1":
                        BuyItem();
                        break;
                    case "2":
                        SellItem();
                        break;
                    default:
                        Console.WriteLine("\n잘못된 입력입니다. 아무 키나 눌러 계속...");
                        Console.ReadKey(); // 잠깐 멈춰줌
                        continue;
                }
                break;
            }
        }
        static void BuyItem()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 구매\n");
                Console.WriteLine($"[보유 골드] {Game.player.Gold} G\n");

                for (int i = 0; i < shopItems.Count; i++)
                {
                    var item = shopItems[i];
                    string status = item.IsPurchased ? "구매완료" : $"{item.Price} G";
                    string bonus = item.Attack > 0 ? $"공격력 +{item.Attack}" : $"방어력 +{item.Defense}";

                    Console.WriteLine($"- {i + 1,-4} {item.Name,-15} {bonus,-12} {item.Description,-40} {status}");
                }

                Console.WriteLine("\n0. 나가기");
                Console.Write("\n구매할 아이템 번호를 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (input == "0") OpenShop();

                if (int.TryParse(input, out int index) && index >= 1 && index <= shopItems.Count)
                {
                    var selectedItem = shopItems[index - 1];

                    if (selectedItem.IsPurchased)
                    {
                        Console.WriteLine("\n이미 구매한 아이템입니다.");
                        Console.ReadKey();
                    }
                    else if (Game.player.Gold >= selectedItem.Price)
                    {
                        selectedItem.IsPurchased = true;
                        Game.player.Gold -= selectedItem.Price;
                        Game.player.Inventory.Add(selectedItem); // 인벤토리에 추가
                        Console.WriteLine($"\n{selectedItem.Name}을(를) 구매했습니다!");
                    }
                    else
                    {
                        Console.WriteLine("\nGold가 부족합니다.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다. 아무 키나 눌러 계속...");
                    Console.ReadKey(); // 잠깐 멈춰줌
                }
            }
        }
        static void SellItem()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 판매\n");
                Console.WriteLine($"[보유 골드] {Game.player.Gold} G\n");

                if (Game.player.Inventory.Count == 0)
                {
                    Console.WriteLine("판매할 아이템이 없습니다.");
                    Console.ReadKey();
                }

                for (int i = 0; i < Game.player.Inventory.Count; i++)
                {
                    var item = Game.player.Inventory[i];
                    Console.WriteLine($"- {i + 1} {item.Name} | 판매가: {item.SellPrice} G");
                }

                Console.WriteLine("\n0. 나가기");
                Console.Write("\n판매할 아이템 번호를 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (input == "0") OpenShop();

                if (int.TryParse(input, out int index) && index >= 1 && index <= Game.player.Inventory.Count)
                {
                    var selectedItem = Game.player.Inventory[index - 1];
                    
                    // 장착된 아이템이라면 자동으로 해제
                    if (selectedItem.IsEquipped)
                    {
                        selectedItem.IsEquipped = false;
                        Console.WriteLine($"\n{selectedItem.Name} 아이템의 장착이 해제되었습니다.");
                        Game.player.BonusAtk -= selectedItem.Attack;
                        Game.player.BonusDef -= selectedItem.Defense;
                    }
                    
                    if (selectedItem is ShopItem shopItem) //구매완료 표시 제거
                    {
                        shopItem.IsPurchased = false;
                    }

                    // 판매 처리
                    Game.player.Gold += selectedItem.SellPrice; // 판매골드 더해짐
                    Game.player.Inventory.RemoveAt(index - 1); // 인벤토리에서 삭제
                    

                    Console.WriteLine($"\n{selectedItem.Name}을(를) 판매했습니다! {selectedItem.SellPrice} G를 받았습니다.");
                    Console.WriteLine("\n아무 키나 눌러 계속...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다. 아무 키나 눌러 계속...");
                    Console.ReadKey(); // 잠깐 멈춰줌
                }
            }
        }
    }
}
