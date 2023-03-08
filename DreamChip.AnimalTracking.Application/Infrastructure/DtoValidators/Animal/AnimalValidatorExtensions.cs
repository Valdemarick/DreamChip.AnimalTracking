using System.Globalization;

namespace DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.Animal;

public static class AnimalValidatorExtensions
{
    private const string AliveStatus = "alive";
    private const string DeadStatus = "dead";
    
    private const string MaleGender = "male";
    private const string FemaleGender = "female";
    private const string OtherGender = "other";
    
    public static bool IsDateTimeValidFormat(DateTime dateTime)
    {
        return dateTime.ToString(CultureInfo.InvariantCulture) 
               == dateTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz", CultureInfo.InvariantCulture);
    }

    public static bool IsLifeStatusValid(string lifeStatus)
    {
        return lifeStatus.ToLower() == AliveStatus || lifeStatus.ToLower() == DeadStatus;
    }

    public static bool IsGenderValid(string gender)
    {
        return gender.ToLower() == MaleGender
               || gender.ToLower() == FemaleGender
               || gender.ToLower() == OtherGender;
    }
}
