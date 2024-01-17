using System.Text.RegularExpressions;
using Eshop.Domain.SeedWork;

namespace Eshop.Domain.Customers.Rules;

public partial class CustomerNameMustNotBeEmptyAndConsistsOnlyLetters(string name) : IBusinessRule
{
    public string Message => "Customer name must not be empty and consists only letters.";

    public bool IsBroken() => string.IsNullOrWhiteSpace(name) || !OnlyLetters().IsMatch(name);

    [GeneratedRegex(@"^[a-zA-Z]+$")]
    private static partial Regex OnlyLetters();
}