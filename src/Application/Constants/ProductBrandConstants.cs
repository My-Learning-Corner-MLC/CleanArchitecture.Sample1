namespace Sample1.Application.Common.Constants;

public static class BrandConst
{
    public static class Rules
    {
    }

    public static class ErrorMessages
    {
        public const string BRAND_ID_DOES_NOT_EXISTS = "Product brand Id does not exists.";
        
        public const string BRAND_ID_AT_LEAST_GREATER_THAN_0 = "Id at least greater than or equal to 0.";

        public const string BRAND_NAME_ALREADY_EXISTS = "Product brand name already exists.";
    }
}