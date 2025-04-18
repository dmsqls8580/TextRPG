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
        //게임 전역에서 접근 가능한 플레이어 객체
        public static Player player = new Player();

        //게임 시작 시 호출되는 메서드
        public static void Start()
        {
            Console.Title = "스파르타 텍스트 RPG";

            StartScreen();  //시작화면에서 이름 입력
            NameInput();    //이름 확인 및 재입력 선택
            SelectJob();    //직업선택

            //마을 진입 안내 메시지
            Console.Clear();
            Console.WriteLine($"환영합니다 {player.Name} 님! ({player.Job})\n");
            Console.WriteLine("이제 마을로 이동합니다...");
            Console.ReadKey();

            //마을로 이동
            EnterVillage();
        }
        //시작 화면 - 이름 입력
        public static void StartScreen()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.");
            Console.Write(">> ");
            player.Name = Console.ReadLine();
        }
        //이름 입력 후 저장 및 재입력 선택
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
                        break;  //저장하면 그대로 진행
                    case "2":
                        Console.Clear();
                        StartScreen();  //다시 이름 입력
                        continue;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 아무 키나 눌러 계속...");
                        Console.ReadKey();
                        continue;
                }
                break;
            }
        }
        //직업 선택 화면
        public static void SelectJob()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("원하시는 직업을 선택해주세요.\n");

                //Player 클래스에 정의된 직업 정보 출력
                for (int i = 0; i < Player.jobInfo.Length; i++)
                {
                    var (jobName, description, atk, def, maxHp) = Player.jobInfo[i];
                    Console.WriteLine($"{i + 1}. {jobName} | {description} | 공격력: {atk}, 방어력: {def}, 체력: {maxHp}");
                }

                Console.Write("\n>> ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= Player.jobInfo.Length)
                { 
                    //선택한 직업으로 설정
                    JobType selectedJob = (JobType)(choice - 1);
                    player.SetJob(selectedJob);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 아무 키나 눌러 계속...");
                    Console.ReadKey();
                    continue;
                }

                break; // 올바른 선택이면 반복 종료
            }
        }
        //마을 화면
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
                    case "0":   //게임 종료
                        Console.WriteLine("\n게임을 종료합니다.");
                        Environment.Exit(0);
                        break;
                    case "1":   //플레이어 상태 보기
                        player.ShowStatus();
                        break;
                    case "2":   //인벤토리 열기
                        Inventory.OpenInventory();
                        break;
                    case "3":   //상점 입장
                        Shop.OpenShop();
                        break;
                    case "4":   //던전 메뉴로 이동
                        Dungeon.EnterDungeonMenu();
                        break;
                    case "5":   //휴식하기(체력회복)
                        RestSystem.Rest(player);
                        break;
                    default:    //잘못된 입력
                        Console.WriteLine("\n잘못된 입력입니다. 아무 키나 눌러 계속...");
                        Console.ReadKey(); // 잠깐 멈춰줌
                        continue;
                }
                break;
            }
        }
    }
}
