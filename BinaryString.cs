using System;
namespace EGA_lab3
{
    public class BinaryString
    {
        public const int MU_FORMAT_WIDTH = 2;                           // Ширина приспособленности при форматированном выводе
        public const int COUNTER_FORMAT_WIDTH = 2;                      // Ширина счётчика при форматированном выводе

        private const int MOD_B = 2;                                    // Мощность алфавита (2 для бинарного)
        private int L;                                                  // Длина кодировки

        public const int MU_RAND_MIN = 0, MU_RAND_MAX = 9999;           // Диапазон для случайного вычисления приспособленности

        public string StrVal;                                                       // Значение бинарной строки
        private static Dictionary<int, int> Mu = new Dictionary<int, int>();        // Словарь, хранящий значения приспособленности


        public BinaryString(int length, MuMode muMode)                  // Конструктор, создающий бинарную строку случайным образом
        {
            L = length;
            StrVal = Generate();
            if (!Mu.ContainsKey(IntVal()))
                Mu[IntVal()] = MuCalc(muMode);
        }

        public BinaryString(int length, MuMode muMode, int rawInt)      // Конструктор, создающий бинарную строку для указанного числа
        {
            L = length;
            StrVal = RawIntToStr(rawInt);
            if (!Mu.ContainsKey(IntVal()))
                Mu[IntVal()] = MuCalc(muMode);
        }

        public BinaryString(int length, MuMode muMode, string rawStr)   // Конструктор, создающий бинарную строку добавлением нулей к существующей
        {
            L = length;
            StrVal = FormatRawStr(rawStr);
            if (!Mu.ContainsKey(IntVal()))
                Mu[IntVal()] = MuCalc(muMode);
        }

        private string Generate()                   // Функция генерации случайной бинарной строки
        {
            Random random = new Random();

            string binaryString = "";
            for (int i = 0; i < L; i++)
            {
                binaryString = $"{random.Next(0, 10) % MOD_B}" + binaryString;
            }
            return binaryString;
        }

        private string RawIntToStr(int rawInt)      // Функция перевода числа в бинарную строку
        {
            string binaryString = "";
            int remainder;

            while (rawInt > 0)
            {
                remainder = rawInt % MOD_B;
                binaryString = $"{remainder}" + binaryString;
                rawInt /= MOD_B;
            }
            return FormatRawStr(binaryString);
        }

        private string FormatRawStr(string rawStr)  // Функция форматирования строки (добавляет нули в начале)
        {
            string binaryString = rawStr;
            for (int i = 0; binaryString.Length < L; i++)
            {
                binaryString = "0" + binaryString;
            }
            return binaryString;
        }

        public int IntVal()                         // Функция перевода бинарной строки в число
        {
            int result = 0;

            for (int i = 0; i < L; i++)
            {
                result += Convert.ToInt32($"{StrVal[i]}") * Convert.ToInt32(Math.Pow(MOD_B, L - (i + 1)));
            }
            return result;
        }

        public enum MuMode                          // Режимы вычисления приспособленности:
        {
            Rand,                                       // Rand - случайное значение в заданном диапазоне
            Natural,                                    // Natural - соответствующее натуральное значение
            Quadratic,                                  // Quadratic - квадратичная функция
        }

        private int MuCalc (MuMode mode)            // Функция вычисления приспособленности
        {
            int mu;
            switch(mode)
            {
                case MuMode.Rand:
                    Random random = new Random();
                    mu = random.Next(MU_RAND_MIN, MU_RAND_MAX + 1);
                    break;
                case MuMode.Natural:
                    mu = IntVal();
                    break;
                case MuMode.Quadratic:
                    mu = Convert.ToInt32(Math.Pow(Convert.ToDouble(IntVal()) - Math.Pow(2, L - 1), 2));
                    break;
                default:
                    mu = -1;
                    break;
            }
            return mu;
        }

        public int GetMu()  // Функция, возвращающая приспособленность для данной бинарной строки
        {
            return Mu[IntVal()];
        }

        public static void CreateLandscape(int stringLength, MuMode muMode, int landscapeLength, int start = 0, int step = 1)  // Функция задания ландшафта приспособленности
        {
            for (int i = start; i < start + landscapeLength * step; i += step)
            {
                BinaryString landscapeString = new BinaryString(stringLength, muMode, i);
                Console.WriteLine($"{i,COUNTER_FORMAT_WIDTH} = {landscapeString.StrVal}, mu = {BinaryString.Mu[landscapeString.IntVal()], MU_FORMAT_WIDTH}");
            }
        }
    }
}

