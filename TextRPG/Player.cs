using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Player
    {
        public int Level = 1;
        public string Name;
        public string Job;
        public int BaseAtk = 1;
        public int BaseDef = 1;
        public int MaxHp = 100;
        public int Hp = 100;    
        public int Gold = 2000;

        public int BonusAtk = 0;
        public int BonusDef = 0;

        public int TotalAtk => BaseAtk + BonusAtk;
        public int TotalDef => BaseDef + BonusDef;

        public List<Item> Inventory = new List<Item>();

        public void ShowStatus()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상태 보기\n");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

                Console.WriteLine($"Lv. {Level}");
                Console.WriteLine($"{Name} ({Job})");

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
                        Game.EnterVillage();
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
