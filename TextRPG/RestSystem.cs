using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class RestSystem
    {
        //휴식 하기
        public static void Rest(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("휴식하기");

                //현재 보유 골드 표시
                Console.WriteLine("500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {0} G)\n", player.Gold);
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "0") Game.EnterVillage(); //마을 이동

                if (input == "1") //휴식 선택
                {
                    //골드가 충분한 경우
                    if (player.Gold >= 500)
                    {
                        int hpBefore = player.Hp;   //회복 전 체력 저장
                        player.Gold -= 500;         //골드 차감
                        player.Hp += 100;           //현재 체력 +100

                        //회복 후 체력이 최대 체력을 초과하지 않도록 제한
                        if (player.Hp > player.MaxHp) player.Hp = player.MaxHp;
                        Console.WriteLine("\n휴식을 완료했습니다! 체력이 회복되었습니다.");
                        Console.WriteLine($"체력 {hpBefore} -> {player.Hp}");

                    }
                    else //골드 부족 시
                    {
                        Console.WriteLine("\nGold가 부족합니다.");
                    }

                    Console.WriteLine("\n아무 키나 눌러 계속...");
                    Console.ReadKey();
                }
                else //잘못된 입력 처리
                {
                    Console.WriteLine("\n잘못된 입력입니다. 아무 키나 눌러 계속...");
                    Console.ReadKey();
                }
            }
        }
    }
}
