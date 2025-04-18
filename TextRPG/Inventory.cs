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
        //인벤토리 열기
        public static void OpenInventory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리\n");

                //아이템이 없을 경우
                if (Game.player.Inventory.Count == 0)
                {
                    Console.WriteLine("보유 중인 아이템이 없습니다.");
                }
                else //아이템이 존재할 경우
                {
                    //아이템 리스트 출력
                    foreach (var item in Game.player.Inventory)
                    {
                        string equippedMark = item.IsEquipped ? "[E] " : "";    //장착 표시
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
                        Game.EnterVillage(); //마을로 돌아가기
                        break;
                    case "1":
                        Equipped(); //장착 관리
                        break;
                    default:
                        Console.WriteLine("\n잘못된 입력입니다. 아무 키나 눌러 계속...");
                        Console.ReadKey(); // 잠깐 멈춰줌
                        continue;
                }
                break;
            }
        }


        //장착/해체 관리 기능
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
                    for (int i = 0; i < Game.player.Inventory.Count; i++) //인덱스 번호와 함께 장착 상태 출력
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
                
                //나가기
                if (inputNum == "0") Game.EnterVillage();

                //숫자 입력인지 체크
                if (int.TryParse(inputNum, out int choice))
                {
                    //인벤토리에 존재하지 않는 번호인 경우
                    if (choice < 1 || choice > Game.player.Inventory.Count)
                    {
                        Console.WriteLine("\n잘못된 번호입니다. 아무 키나 눌러 계속...");
                        Console.ReadKey();
                    }
                    else
                    {
                        var selectedItem = Game.player.Inventory[choice - 1];

                        //장착되지 않은 경우 -> 장착
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

                                //선택한 아이템 장착
                                selectedItem.IsEquipped = true;

                                Game.player.BonusAtk += selectedItem.Attack;
                                Game.player.BonusDef += selectedItem.Defense;

                                Console.WriteLine($"\n{selectedItem.Name}을(를) 장착했습니다!");
                                Console.ReadKey();
                            }
                        }
                        else //이미 장착된 경우 -> 해제
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
                else //숫자가 아닌 경우
                {
                    Console.WriteLine("\n숫자를 정확히 입력해주세요. 아무 키나 눌러 계속...");
                    Console.ReadKey();
                }
            }
        }
    }
}
