namespace MysteryAuction.Data.Models
{
    public class DataConstraints
    {
        public class UserConstraints
        {
            public const int MaxUsernameLength = 20;
        }

        public class MysteryProductConstraints
        {
            public const int MaxNameLength = 20;
            public const int MaxDescriptionLength = 3000;
        }
    }
}
