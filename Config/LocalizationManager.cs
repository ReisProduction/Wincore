using System.Globalization;
namespace ReisProduction.Wincore.Config;
/// <summary>
/// Localization manager for handling localization operations.
/// </summary>
public static class LocalizationManager
{
    /// <summary>
    /// Gets the localized string for the given key.
    /// </summary>
    public static string GetString(this string key) => key;
    /// <summary>
    /// Formats a string to indicate it is "for" another string, considering localization.
    /// </summary>
    public static string ForThis(this string x, string forX)
    {
        string f0r = "internal-For".GetString();
        x = x.GetString();
        forX = forX.GetString();
        return (/*LocalizationManager.SelectedLanguage ??*/ System.System.TwoLetterISOLanguageName) switch
        {
            "tr" => $"{forX} {f0r} {x}",
            _ => $"{x} {f0r} {forX}"
        };
    }
    /// <summary>
    /// Transforms the case of the input string based on the specified TextCase and culture.
    /// </summary>
    public static string TransformCase(this string input, TextCase textCase, CultureInfo? culture = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(input);
        culture ??= CultureInfo.CurrentCulture;
        input = input.Trim();
        return textCase switch
        {
            TextCase.Upper => input.ToUpper(culture),
            TextCase.Lower => input.ToLower(culture),
            TextCase.Sentence => char.ToUpper(input[0], culture) + input[1..].ToLower(culture),
            TextCase.Title => culture.TextInfo.ToTitleCase(input.ToLower(culture)),
            _ => input
        };
    }
    /// <summary>
    /// Applies the Shift key transformation to non-letter characters.
    /// </summary>
    public static char ShiftToNonLetter(this char c) => c switch
    {
        '1' => '!',
        '2' => '@',
        '3' => '#',
        '4' => '$',
        '5' => '%',
        '6' => '^',
        '7' => '&',
        '8' => '*',
        '9' => '(',
        '0' => ')',
        '-' => '_',
        '=' => '+',
        ';' => ':',
        ',' => '<',
        '.' => '>',
        '/' => '?',
        '`' => '~',
        '[' => '{',
        ']' => '}',
        '\\' => '|',
        '\'' => '"',
        _ => c
    };
    /// <summary>
    /// Applies the AltGr key transformation to non-letter characters.
    /// </summary>
    public static char AltGrToNonLetter(this char c) => c switch
    {
        'q' or 'Q' => '@',
        'e' or 'E' => '€',
        '2' => '²',
        '3' => '³',
        '4' => '¼',
        '5' => '½',
        '6' => '¾',
        '7' => '{',
        '8' => '[',
        '9' => ']',
        '0' => '}',
        '$' => '£',
        '-' => '\\',
        ',' => ';',
        '.' => ':',
        '_' => '|',
        '<' => '>',
        _ => c
    };
    /// <summary>
    /// Translates a boolean value to its corresponding localized string based on provided keys.
    /// </summary>
    public static string ToInternalText(this bool value, string trueKey, string falseKey) => (value ? trueKey : falseKey).GetString();
}