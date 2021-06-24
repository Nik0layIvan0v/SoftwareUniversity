using System.Collections.Generic;

namespace BattleCards.Data
{
    public static class DataConstants
    {
        //User Constraints
        public const int MinUsernameLength = 5;

        public const int MaxUsernameLength = 20;

        public const int MinPasswordLength = 6;

        public const int MaxPasswordLength = 20;

        public const string EmailRegularExpression =
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        //Card Constraints

        public const int MinCardNameLength = 5;

        public const int MaxCardNameLength = 15;

        public const int MaxDescriptionLength = 200;

        public static List<string> AllowedKeywords = new List<string>
        {
            "Tough",
            "Challenger",
            "Elusive",
            "Overwhelm",
            "Lifesteal",
            "Ephemeral",
            "Fearsome"
        };

    }
}