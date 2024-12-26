using Microsoft.Extensions.Localization;

namespace Utils.Resources.Localization;

public class Localization : IStringLocalizer
{
    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        throw new NotImplementedException();
    }

    LocalizedString IStringLocalizer.this[string name] => this[name];

    public LocalizedString this[string name, params object[] arguments] => this[name];
}