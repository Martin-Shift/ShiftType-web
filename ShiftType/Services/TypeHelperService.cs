
namespace ShiftType.Services;

public static class TypeHelperService
{
    public static int CountErrors(string input, string check)
    {
        var errors = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (check[i] != input[i])
            {
                errors++;
            }
        }
        return errors;
    }
    public static int CountWPM(string input, string check, double SecondsSpent)
    {
        var errors = CountErrors(input, check);
           var MinutesTyped = (double)SecondsSpent / 60;
        return (int)((((double)input.Length - errors)/4.5) / MinutesTyped);
    }
}

