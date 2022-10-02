using System.Text;
using EGA_lab3;

internal class Program
{
    private static void Main(string[] args)
    {
        const int L = 5;
        const BinaryString.MuMode MODE = BinaryString.MuMode.Natural;

        Console.WriteLine("------======= LANDSCAPE =======------\n");
        BinaryString.CreateLandscape(L, MODE, 32);

        Console.WriteLine("\n------======= HILL CLIMBING METHOD (BREADTH-FIRST SEARCH) =======------\n");
        Console.WriteLine("Enter N: ");
        int N = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();

        BinaryString maxS = new BinaryString(L, MODE);

        for (int i = 0; i < N; i++)
        {
            List<BinaryString> omega = Omega(maxS);
            string omegaString = $"Omega = {OmegaToString(omega)}\n";

            BinaryString maxOmega = omega[0];

            for (int j = 1; j < omega.Count; j++)
            {
                if (maxOmega.GetMu() < omega[j].GetMu())
                {
                    maxOmega = omega[j];
                }
            }

            string reportString = $"Step {i + 1,BinaryString.COUNTER_FORMAT_WIDTH}: ";
            reportString += $"maxS = {maxS.StrVal}, max = {maxS.GetMu(),BinaryString.MU_FORMAT_WIDTH}, ";
            reportString += $"maxOmega = {maxOmega.StrVal}, mu = {maxOmega.GetMu(),BinaryString.MU_FORMAT_WIDTH} ";

            if (maxS.GetMu() < maxOmega.GetMu())
            {
                maxS = maxOmega;
                reportString += "<- CHANGE";
                Console.WriteLine(reportString);
                Console.WriteLine(omegaString);
            }
            else
            {
                Console.WriteLine(reportString);
                Console.WriteLine(omegaString);
                break;
            }

        }

        Console.WriteLine($"Final result: {maxS.StrVal}, mu = {maxS.GetMu()}");


        List<BinaryString> Omega(BinaryString currentString)
        {
            List<BinaryString> omega = new List<BinaryString>();
            for (int j = 0; j < L; j++)
            {
                StringBuilder nearbyString = new StringBuilder(currentString.StrVal);
                if (nearbyString[j] == '0')
                {
                    nearbyString[j] = '1';
                }
                else if (nearbyString[j] == '1')
                {
                    nearbyString[j] = '0';
                }
                omega.Add(new BinaryString(L, MODE, Convert.ToString(nearbyString)!));
            }
            return omega;
        }

        string OmegaToString(List<BinaryString> omega)
        {
            string result = "{ ";
            if (omega.Count > 0)
                result += $"{omega[0].StrVal} ({omega[0].GetMu()})";
            for (int i = 1; i < omega.Count; i++)
            {
                result += $", {omega[i].StrVal} ({omega[i].GetMu()})";
            }
            result += " }";
            return result;
        }
    }
}