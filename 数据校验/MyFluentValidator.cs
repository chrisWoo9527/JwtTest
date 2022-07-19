using FluentValidation;
using Identity.Sql.Data.Model;

namespace 数据校验
{
    public class MyFluentValidator : AbstractValidator<UserSomething>
    {
        public MyFluentValidator()
        {
            RuleFor(x => x.Price).LessThan(10).GreaterThan(5).WithMessage("大于5小10之间");
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("不为空");
            RuleFor(x => x.Email).EmailAddress().WithMessage("必须是个邮件格式");
        }
    }
}
