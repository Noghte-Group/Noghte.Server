namespace Noghte.Application.Configuration;

public class ReflectionExtensions
{
    public static List<Type> LoadTypesFromAssemblies(Func<Type, bool> predicate)
    {
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(predicate).ToList();

        return types;
    }
}