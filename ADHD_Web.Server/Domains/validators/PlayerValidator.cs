namespace ADHD_Web.Server.Domains.validators;

public static class PlayerValidator
{
    public static readonly string[] GenderList =
    [
        "male",
        "female",
        "other"
    ];

    public static bool VerifyAge(int age)
    {
        if (age <= 0) return false;
        if (age >= 200) return false;
        return true;
    }

    public static bool VerifyGender(string gender)
    {
        var lowCaseGender = gender.ToLower();
        var checkExistGender = GenderList.Contains(lowCaseGender);
        return checkExistGender;
    }
}