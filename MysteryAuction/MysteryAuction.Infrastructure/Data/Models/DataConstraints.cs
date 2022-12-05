namespace MysteryAuction.Infrastructure.Data.Models
{
    public class DataConstraints
    {
        public class MysteryProductConstraints
        {
            public const int MaxDescriptionLength = 3000;
            public const int MinDescriptionLength = 30;
            public const int MaxNameLength = 20;
            public const int MinNameLength = 3;
        }

        public class CategoryConstraints
        {
            public const int MaxCategoryLength = 20;
        }
    }
}
