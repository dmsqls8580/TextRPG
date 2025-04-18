using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public enum JobType
    {
        Warrior,
        Thief,
        Mage,
        Archer
    }

    public class Player
    {

        public static readonly (string job, string description, int atk, int def, int maxHp)[] jobInfo =
        {
            ("전사", "공격력과 방어력이 균형잡힌 전투 전문가", 3, 3, 120),
            ("도적", "민첩하고 빠른 공격을 자랑하는 직업", 4, 2, 100),
            ("마법사", "강력한 마법 공격력을 가진 직업", 5, 1, 90),
            ("궁수", "원거리에서 정확한 피해를 주는 직업", 4, 2, 100)
        };

        private int dungeonClearCount = 0;  // 누적 던전 클리어 수
        private int requiredClearsForNextLevel = 1;  // 다음 레벨까지 필요한 클리어 수 (레벨 n -> n+1은 n번 클리어해야함)

        public int Level { get; private set; } = 1; // 기본 레벨 1부터 시작
        public string Name;
        public string Job;
        public int MaxHp = 0;
        public int Hp = 0;    
        public int Gold = 2000;

        //기본 공격력, 방어력
        public float BaseAtk = 0;
        public int BaseDef = 0;

        //아이템을 통한 추가 공격력, 방어력
        public float BonusAtk = 0;
        public int BonusDef = 0;

        //총합 공격력, 방어력
        public float TotalAtk => BaseAtk + BonusAtk;
        public int TotalDef => BaseDef + BonusDef;

        //아이템 인벤토리
        public List<Item> Inventory = new List<Item>();

        //직업에 따른 스탯 초기화
        public void SetJob(JobType jobType)
        {
            var info = jobInfo[(int)jobType];
            Job = info.job;
            BaseAtk = info.atk;
            BaseDef = info.def;
            MaxHp = info.maxHp;
            Hp = MaxHp;
        }

        //캐럭터 상태를 콘솔에 출력하는 메서드
        public void ShowStatus()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상태 보기\n");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

                Console.WriteLine($"Lv. {Level}");
                Console.WriteLine($"{Name} ({Job})");

                //보너스 수치가 있는 경우 함께 출력
                string atkText = BonusAtk > 0 ? $"{TotalAtk} (+{BonusAtk})" : $"{TotalAtk}";
                string defText = BonusDef > 0 ? $"{TotalDef} (+{BonusDef})" : $"{TotalDef}";

                Console.WriteLine($"공격력 : {atkText}");
                Console.WriteLine($"방어력 : {defText}");
                Console.WriteLine($"체  력 : {Hp}/{MaxHp}");
                Console.WriteLine($"Gold : {Gold} G");

                Console.WriteLine("\n0. 나가기");
                Console.Write(">> ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        Game.EnterVillage(); //마을화면으로 돌아감
                        break;
                    default:
                        Console.WriteLine("\n잘못된 입력입니다. 아무 키나 눌러 계속...");
                        Console.ReadKey(); // 잠깐 멈춰줌
                        continue;
                }
                break;
            }
        }

        //던전 클리어 후 레벨업 체크하는 메서드
        public void CheckLevelUp()
        {
            dungeonClearCount++; //클리어 수 1 증가

            //클리어 수가 조건 이상이면 레벨업
            if (dungeonClearCount >= requiredClearsForNextLevel)
            {
                Level++; //레벨증가
                dungeonClearCount = 0; // 클리어 수 초기화
                requiredClearsForNextLevel = Level; // 다음 레벨까지 필요한 클리어 수

                //기본 공격력과 방어력 증가
                BaseAtk += 0.5f;
                BaseDef += 1;

                Console.WriteLine($"\n레벨업! 현재 레벨: {Level}");
                Console.WriteLine($"기본 공격력 +0.5 / 방어력 +1 증가했습니다!");
            }
        }
    }
}
