namespace Sample1.Application.Common.Constants;

public static class ValidationConst
{
    public static class Value
    {
        public const int NAME_MAX_LENTGH = 50;
        public const int URI_MAX_LENGTH = 300;
        public const decimal MAX_PRICE = 9999999999999999.99M;
        public const decimal MIN_PRICE = 0;
    }

    public static class ErrorMessage
    {
        public const string PRODUCT_NAME_ALREADY_EXISTS = "Product name already exits.";
        public const string PRODUCT_PRICE_SHOULD_BE_GREATER_THAN = "Product price should be greater than ";
        public const string PRODUCT_PRICE_SHOULD_BE_LESS_THAN = "Product price should be less than ";
    }
}