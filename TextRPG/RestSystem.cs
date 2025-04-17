using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class RestSystem
    {
        public static void Rest(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("휴식하기");
                Console.WriteLine("500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {0} G)\n", player.Gold);
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "0") Game.EnterVillage();

                if (input == "1")
                {
                    if (player.Gold >= 500)
                    {
                        int hpBefore = player.Hp;
                        player.Gold -= 500;
                        player.Hp += 100; // 혹시 MaxHP가 있다면 player.HP = player.MaxHP;
                        if (player.Hp > player.MaxHp) player.Hp = player.MaxHp;
                        Console.WriteLine("\n휴식을 완료했습니다! 체력이 회복되었습니다.");
                        Console.WriteLine($"체력 {hpBefore} -> {player.Hp}");

                    }
                    else
                    {
                        Console.WriteLine("\nGold가 부족합니다.");
                    }

                    Console.WriteLine("\n아무 키나 눌러 계속...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다. 아무 키나 눌러 계속...");
                    Console.ReadKey();
                }
            }
        }
    }
}
