using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Game
    {
        public static Player player = new Player();

        public static void Start()
        {
            Console.Title = "스파르타 텍스트 RPG";

            StartScreen();
            NameInput();
            SelectJob();

            Console.Clear();
            Console.WriteLine($"환영합니다 {player.Name} 님! ({player.Job})\n");
            Console.WriteLine("이제 마을로 이동합니다...");
            Console.ReadKey();

            EnterVillage();
        }

        public static void StartScreen()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.");
            Console.Write(">> ");
            player.Name = Console.ReadLine();
        }

        public static void NameInput()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine($"입력하신 이름은 {player.Name} 입니다.\n");
                Console.WriteLine("1. 저장");
                Console.WriteLine("2. 취소");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        break;
                    case "2":
                        Console.Clear();
                        StartScreen();
                        continue;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 아무 키나 눌러 계속...");
                        Console.ReadKey();
                        continue;
                }
                break;
            }
        }

        public enum job
        {

        }

        public static void SelectJob()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("원하시는 직업을 선택해주세요.\n");
                Console.WriteLine("1. 전사 - 공격력과 방어력이 균형잡힌 전투 전문가");
                Console.WriteLine("2. 도적 - 민첩하고 빠른 공격을 자랑하는 직업");
                Console.WriteLine("3. 마법사 - 강력한 마법 공격력을 가진 직업");
                Console.WriteLine("4. 궁수 - 원거리에서 정확한 피해를 주는 직업");
                Console.Write("\n>> ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        player.Job = "전사";
                        break;
                    case "2":
                        player.Job = "도적";
                        break;
                    case "3":
                        player.Job = "마법사";
                        break;
                    case "4":
                        player.Job = "궁수";
                        break;
                    default:
                        Console.WriteLine("\n잘못된 입력입니다. 아무 키나 눌러 계속...");
                        Console.ReadKey(); // 잠깐 멈춰줌
                        continue;
                }

                break; // 올바른 선택이면 반복 종료
            }
        }
        public static void EnterVillage()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");

                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전 입장");
                Console.WriteLine("5. 휴식하기");

                Console.WriteLine("\n0. 게임 종료");

                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        Console.WriteLine("\n게임을 종료합니다.");
                        Environment.Exit(0);
                        break;
                    case "1":
                        player.ShowStatus();
                        break;
                    case "2":
                        Inventory.OpenInventory();
                        break;
                    case "3":
                        Shop.OpenShop();
                        break;
                    case "4":
                        Dungeon.EnterDungeonMenu();
                        break;
                    case "5":
                        RestSystem.Rest(player);
                        break;
                    default:
                        Console.WriteLine("\n잘못된 입력입니다. 아무 키나 눌러 계속...");
                        Console.ReadKey(); // 잠깐 멈춰줌
                        continue;
                }
                break;
            }
        }
    }
}
