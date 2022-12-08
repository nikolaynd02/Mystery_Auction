namespace MysteryAuction.Infrastructure.Data.Models
{
    public class DataConstraints
    {
        public class MysteryProductConstraints
        {
            public const int MaxDescriptionLength = 3_000;
            public const int MinDescriptionLength = 30;

            public const int MaxNameLength = 20;
            public const int MinNameLength = 3;

            public const int MaxPrice = 999_999_999;
            public const int MinPrice = 1;
        }

        public class CategoryConstraints
        {
            public const int MaxNameLength = 20;
            public const int MinNameLength = 2;
        }
    }
}
