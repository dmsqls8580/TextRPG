using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public static class Inventory
    {
        public static void OpenInventory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리\n");

                if (Game.player.Inventory.Count == 0)
                {
                    Console.WriteLine("보유 중인 아이템이 없습니다.");
                }
                else
                {
                    foreach (var item in Game.player.Inventory)
                    {
                        string equippedMark = item.IsEquipped ? "[E] " : "";
                        string statText = item.Attack > 0 ? $"공격력 +{item.Attack}" :
                                          item.Defense > 0 ? $"방어력 +{item.Defense}" : "스탯 없음";
                        Console.WriteLine($"- {equippedMark} {item.Name,-16} | {statText,-14} | {item.Description,-40} ");

                    }
                }

                Console.WriteLine("\n1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.Write(">> ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        Game.EnterVillage();
                        break;
                    case "1":
                        Equipped();
                        break;
                    default:
                        Console.WriteLine("\n잘못된 입력입니다. 아무 키나 눌러 계속...");
                        Console.ReadKey(); // 잠깐 멈춰줌
                        continue;
                }
                break;
            }
        }

        static void Equipped()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리\n");

                if (Game.player.Inventory.Count == 0)
                {
                    Console.WriteLine("보유 중인 아이템이 없습니다.");
                }
                else
                {
                    for (int i = 0; i < Game.player.Inventory.Count; i++)
                    {
                        var item = Game.player.Inventory[i];
                        string equippedMark = item.IsEquipped ? "[E] " : "";
                        string statText = item.Attack > 0 ? $"공격력 +{item.Attack}" :
                                          item.Defense > 0 ? $"방어력 +{item.Defense}" : "스탯 없음";
                        Console.WriteLine($"{i + 1,-4} - {equippedMark} {item.Name,-16} | {statText,-14} | {item.Description,-40} ");
                    }
                }
                Console.WriteLine("\n0. 나가기");
                Console.Write("\n장착/해제할 아이템 번호를 선택하세요.\n>> ");
                string inputNum = Console.ReadLine();
                
                if (inputNum == "0") Game.EnterVillage();

                if (int.TryParse(inputNum, out int choice))
                {
                    if (choice < 1 || choice > Game.player.Inventory.Count)
                    {
                        Console.WriteLine("\n잘못된 번호입니다. 아무 키나 눌러 계속...");
                        Console.ReadKey();
                    }
                    else
                    {
                        var selectedItem = Game.player.Inventory[choice - 1];

                        if (!selectedItem.IsEquipped)
                        {
                            Console.WriteLine($"\n{selectedItem.Name}을(를) 장착하시겠습니까? (y/n)");
                            Console.Write(">> ");
                            string input = Console.ReadLine().ToLower();

                            if (input == "y")
                            {
                                // 동일한 타입의 다른 장비가 이미 장착되어 있다면 해제
                                foreach (var item in Game.player.Inventory)
                                {
                                    if (item != selectedItem && item.IsEquipped && item.Type == selectedItem.Type)
                                    {
                                        item.IsEquipped = false;
                                        Game.player.BonusAtk -= item.Attack;
                                        Game.player.BonusDef -= item.Defense;
                                        Console.WriteLine($"\n※ {item.Name} 장착 해제됨 (같은 타입 아이템)");
                                    }
                                }

                                selectedItem.IsEquipped = true;

                                Game.player.BonusAtk += selectedItem.Attack;
                                Game.player.BonusDef += selectedItem.Defense;

                                Console.WriteLine($"\n{selectedItem.Name}을(를) 장착했습니다!");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine($"\n{selectedItem.Name}을(를) 장착 해제하시겠습니까? (y/n)");
                            Console.Write(">> ");
                            string input = Console.ReadLine().ToLower();

                            if (input == "y")
                            {
                                selectedItem.IsEquipped = false;

                                Game.player.BonusAtk -= selectedItem.Attack;
                                Game.player.BonusDef -= selectedItem.Defense;

                                Console.WriteLine($"\n{selectedItem.Name}을(를) 장착 해제했습니다!");
                                Console.ReadKey();
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\n숫자를 정확히 입력해주세요. 아무 키나 눌러 계속...");
                    Console.ReadKey();
                }
            }
        }
    }
}
