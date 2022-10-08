using System.ComponentModel.DataAnnotations;

namespace Noghte.BuildingBlock.Exceptions;

public enum ConsumerStatusCode
{
    [Display(Name = "عملیات با موفقیت انجام شد")]
    Success = 200,

    [Display(Name = "خطایی در سرور رخ داده است")]
    ServerError = 500,

    [Display(Name = "پارامتر های ارسالی معتبر نیستند")]
    BadRequest = 400,

    [Display(Name = "یافت نشد")]
    NotFound = 404,

    [Display(Name = "خطای احراز هویت")]
    UnAuthorized = 401,
    
    [Display(Name = "متد درخواست  ارسال شده نامعتبر می باشد")]
    MethodNotAllowed = 405
}