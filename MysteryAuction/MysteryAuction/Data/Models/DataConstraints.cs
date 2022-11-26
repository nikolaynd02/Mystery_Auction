namespace MysteryAuction.Data.Models
{
    public class DataConstraints
    { 
        public const int MaxDescriptionLength = 3000;
        //public class UserConstraints
        //{
        //    public const int MaxUsernameLength = 20;
        //}

        public class CarConstraints
        {
            public const int MaxMakerLength = 40;
            public const int MaxModelLength = 40;
            public const int MaxEngineLength = 20;
        }

        public class MysteryProductConstraints
        {
            public const int MaxNameLength = 20;
        }

        public class UnclaimedContainerConstraints
        {
            public const int MaxContainerNumberLength = 20;
            public const int MaxCheckDigitLength = 1;
            public const int MaxIsoCodeLength = 8;
            public const int MaxWeightInclContainer = 10;
            public const int MaxWeightOfContainer = 10;
            public const int MaxPackedWeight = 10;
            public const int MaxPackedVolume = 14;
        }
    }
}
