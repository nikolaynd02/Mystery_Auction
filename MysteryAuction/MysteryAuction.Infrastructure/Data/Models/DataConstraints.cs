namespace MysteryAuction.Infrastructure.Data.Models
{
    public class DataConstraints
    {
        public class MysteryProductConstraints
        {
            public const int MaxDescriptionLength = 3000;
            public const int MaxNameLength = 20;
        }

        public class CategoryConstraints
        {
            public const int MaxCategoryLength = 20;
        }
    }
}
