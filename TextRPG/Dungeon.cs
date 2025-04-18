using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Dungeon
    {
        //던전 난이도 정의
        public enum Difficulty
        {
            Easy,
            Normal,
            Hard
        }
        //각 난이도별 던전 정보 : 이름, 권장 방어력, 기본 보상
        private static readonly (string Name, int RequiredDef, int BaseReward)[] dungeonInfos =
        {
            ("쉬운 던전", 5, 1000),
            ("일반 던전", 11, 1700),
            ("어려운 던전", 17, 2500)
        };
        //던전 입장 메뉴
        public static void EnterDungeonMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("던전 입장\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");

                //각 던전의 이름과 권장 방어력 출력
                for (int i = 0; i < dungeonInfos.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {dungeonInfos[i].Name,-10} | 방어력 {dungeonInfos[i].RequiredDef} 이상 권장");
                }
                 
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "0") Game.EnterVillage(); //0번 선택시 마을 이동

                //던전 난이도 선택
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= dungeonInfos.Length)
                {
                    StartDungeon((Difficulty)(choice - 1));
                }
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다. 아무 키나 눌러 다시 선택해주세요.");
                    Console.ReadKey();
                    continue;
                }
                break;
            }
        }
        //선택한 난이도의 던전 시작
        public static void StartDungeon(Difficulty difficulty)
        {
            Console.Clear();
            var player = Game.player;
            var (name, requiredDef, baseReward) = dungeonInfos[(int)difficulty];

            bool isSuccess;
            int defDiff = requiredDef - player.TotalDef;
            Random rand = new Random();

            int hpBefore = player.Hp;
            int goldBefore = player.Gold;

            // 던전 성공 여부 판단
            if (player.TotalDef >= requiredDef)
            {
                isSuccess = true;

                // 체력 감소 계산
                int minLoss = 20 + defDiff;
                int maxLoss = 35 + defDiff;
                int hpLoss = rand.Next(minLoss, maxLoss + 1);
                player.Hp = player.Hp - hpLoss;

                // 보상 계산
                int minPercent = (int)player.TotalAtk;
                int maxPercent = (int)(player.TotalAtk * 2);
                int bonusPercent = rand.Next(minPercent, maxPercent + 1);
                int bonusGold = baseReward * bonusPercent / 100;
                int totalGold = baseReward + bonusGold;
                player.Gold += totalGold;
                
                //결과 출력
                Console.WriteLine($"던전 클리어!!\n{name}을(를) 클리어 하였습니다.\n");
                Console.WriteLine($"[탐험 결과]\n체력 {hpBefore} -> {player.Hp}\nGold {goldBefore} G -> {player.Gold} G");

                //레벨업 여부 확인
                Game.player.CheckLevelUp();
            }
            else if (player.TotalDef < requiredDef)
            {
                //실패 확률 40%
                int failChance = 40;
                isSuccess = rand.Next(100) >= failChance;

                int hpLoss = player.Hp / 2; // 실패 시 체력 절반 감소
                player.Hp = player.Hp - hpLoss;
                Console.WriteLine($"던전 실패...\n\n[탐험 결과]\n체력 {hpBefore} -> {player.Hp}\n보상 없음");
            }
            //사망 체크
            if (player.Hp <= 0)
            {
                Console.WriteLine("\n캐릭터가 사망했습니다.");
                Console.WriteLine("\n게임을 종료합니다.");
                Environment.Exit(0);
            }
            //다음 행동 선택
            while (true)
            {
                Console.WriteLine("\n0. 나가기");
                Console.Write(">> ");

                string input = Console.ReadLine();

                if (input == "0") EnterDungeonMenu();
                else
                {
                    Console.WriteLine("\n잘못된 입력입니다. 아무 키나 눌러 다시 선택해주세요.");
                    Console.ReadKey();
                    continue;
                }
                break;
            }
        }
    }
}
