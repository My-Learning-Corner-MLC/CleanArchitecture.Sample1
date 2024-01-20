namespace Sample1.Application.Common.Constants;

public static class ExceptionConst
{
    public static class ErrorMessage 
    {
        public const string RESOURCE_CONFLICT = "The specified resource was conflicted.";
        public const string RESOURCE_NOT_FOUND = "Resource not found";
    }

    public static class ErrorDescription
    {
        public const string ID_CONFLICT = "The Id was conflicted.";
        public const string COULD_NOT_FOUND_ITEM_WITH_ID = "Could not found item with id: ";
    }
}