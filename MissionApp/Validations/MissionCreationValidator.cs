using MissionApp.Entities;
using MissionApp.Exceptions;
using System.Globalization;


namespace MissionApp.Validations
{
    public class MissionCreationValidator
    {
        private static readonly string prefix = "Invalid mission provided: ";
        private static readonly string[] DateFormats = { "MMM dd, yyyy, h:mm:ss tt" };
        public static void validate(Mission mission) 
        {
            if (mission == null)
            {
                throw new ValidationException(string.Format("{0} Received empty mission. Please contact support.", prefix));
            }
            validateNotEmpty("codename", mission.CodeName);
            validateNotEmpty("address", mission.Address);
            validateNotEmpty("date", mission.Date);
            validateNotEmpty("country", mission.Country);
            if (mission.Location == null || mission.Location.Values.Count != 2)
            {
                throw new ValidationException(string.Format("{0} Coordinates for mission are missing.", prefix));
            }
            if (!IsValidDate(mission.Date))
            {
                throw new ValidationException(string.Format("{0} Received invalid date: {1}.", prefix, mission.Date));
            }
        }

        private static void validateNotEmpty(string key, string value)
        {
            if (value == null)
            {
                throw new ValidationException(string.Format("{0} Missing value for key {1}.", prefix, key));
            }
        }

        public static bool IsValidDate(string dateString)
        {
            return DateTime.TryParseExact(
                dateString,
                DateFormats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _);
        }
    }
}
