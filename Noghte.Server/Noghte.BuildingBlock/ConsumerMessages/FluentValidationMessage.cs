namespace Noghte.BuildingBlock.ConsumerMessages;

public static class FluentValidationMessage

{
    public static readonly Func<string, string, string> MUST_BE_GREATER_THAN =
        (param1, param2) => $"{param1}_must_be_greater_than_{param2}";


    public static readonly Func<string, string, string> MUST_BE_GREATER_THAN_OR_EQUAL =
        (param1, param2) => $"{param1}_must_be_greater_than_or_equal_{param2}";


    public static readonly Func<string, string, string> MUST_BE_BETWEEN =
        (param1, param2) => $"must_be_between_{param1} and {param2}";


    public static readonly Func<string, string, string> MUST_BE_SMALLER_THAN_OR_EQUAL =
        (param1, param2) => $"{param1}_must_be_smaller_than_or_equal_{param2}";


    public static readonly Func<string, string, string> MUST_BE_SMALLER_THAN =
        (param1, param2) => $"{param1}_must_be_smaller_than_{param2}";


    public static readonly Func<string, string, string> MUST_BE_EQUAL_TO =
        (param1, param2) => $"{param1}_must_be_equal_to_{param2}";


    public static readonly Func<string, string> MUST_BE_NUMERIC_DATA = (param) => $"{param}_must_be_numeric_data";


    public static readonly Func<string, string> DUPLICATED_DATA = (param) => $"{param}_is_duplicated";


    public static readonly Func<string, string> IS_NOT_VALID = (param) => $"{param}_is_not_valid";


    public static readonly Func<string, string> CANNOT_BE_EMPTY = (param) => $"{param}_can_not_be_empty";


    public static readonly Func<string, string, string> LENGTH_MUSY_BE_EQUAL_TO =
        (param1, param2) => $"{param1}_length_must_be_equalto{param2}";


    public static readonly Func<string, string> CANNOT_BE_INSERT_WITH = (param) => $"can_not_be_insert_with_{param}";


    public static readonly Func<string, string, string> ONE_OF_THESE_FIELD_SHOULD_FILL =
        (param1, param2) => $"at_least_one_field_should_set_({param1},{param2})";
}