namespace CargoObject.API.Services;

/// <summary>
/// Service for generation keys that using for set value in distributed cache (like Redis etc.)
/// Example: 
///     name, user, option.
/// Result:
///     name:user:option:
/// </summary>
public static class KeyMaker
{
    public static string CreateKey(params string[] keys)
    {
        var result = string.Empty;

        foreach (var key in keys)
        {
            result += key + ":";
        }

        return result;
    }
}
