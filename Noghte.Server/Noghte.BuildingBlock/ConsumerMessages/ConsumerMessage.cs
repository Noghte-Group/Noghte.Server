namespace Noghte.BuildingBlock.ConsumerMessages;

public static class ConsumerMessage

{
    public static readonly Func<string, string> CREATE_SUCCESSFULLY = (param) => $"{param}_created_successfully";


    public static readonly Func<string, string> DELETE_SUCCESSFULLY = (param) => $"{param}_deleted_successfully";


    public static readonly Func<string, string> UPDATE_SUCCESSFULLY = (param) => $"{param}_updated_successfully";


    public static readonly Func<string, string> GET_SUCCESSFULLY = (param) => $"{param}_fetched_successfully";


    public static readonly Func<string, string> GET_PAGINATED_SUCCESSFULLY =
        (param) => $"{param}pagination_fetched_successfully";


    public static readonly Func<string, string> NOTFOUND = (param) => $"{param} Not Found";


    public static readonly Func<string, string> DUPLICATED = (param) => $"{param}_already_exists";


    public static readonly Func<string> ACCESS_DENDIED =
        () => $"the_user_has_no_suitable_permission_to_call_this_action";


    public static readonly Func<string, string> ACCESS_RESTRICT =
        (param) => $"you_have_been_restricted, reason: {param}";

    public static readonly Func<string> Authenticated_Successfully=
        () => $"user_have_been_autheticated_successfully";

    public static readonly Func<string> Sent_Successfully =
    () => $"otp_has_been_sent_successfully";
}