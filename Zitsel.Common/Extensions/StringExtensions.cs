// <copyright file="StringExtensions.cs" company="Thomas Beckett">
// Copyright (c) Thomas Beckett. All rights reserved.
// </copyright>

namespace Zitsel.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Provides extension methods for string manipulation.
    /// </summary>
    public static partial class StringExtensions
    {
        /// <summary>
        /// Determines whether the specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="str">The string to test.</param>
        /// <returns>true if the string is null, empty, or consists only of white-space characters; otherwise, false.</returns>
        public static bool IsNullOrWhiteSpace(this string? str) => string.IsNullOrWhiteSpace(str);

        /// <summary>
        /// Determines whether the specified string is null or empty.
        /// </summary>
        /// <param name="str">The string to test.</param>
        /// <returns>true if the string is null or empty; otherwise, false.</returns>
        public static bool IsNullOrEmpty(this string? str) => string.IsNullOrEmpty(str);

        /// <summary>
        /// Removes the specified text from the string.
        /// </summary>
        /// <param name="str">The string to strip.</param>
        /// <param name="textToStrip">The text to remove.</param>
        /// <returns>The modified string.</returns>
        public static string Strip(this string str, string textToStrip) => str.Replace(textToStrip, string.Empty);

        /// <summary>
        /// Pluralizes a string based on the count provided.
        /// </summary>
        /// <param name="str">The string to be pluralized.</param>
        /// <param name="plural">The plural form of the string.</param>
        /// <param name="count">The count determining whether the string should be pluralized.</param>
        /// <returns>The pluralized string if the count is not equal to 1; otherwise, returns the original string.</returns>
        public static string Pluralize(this string str, string plural, int count) => count == 1 ? str : $"{str}{plural}";

        /// <summary>
        /// Determines whether the input string is a valid Base64-encoded string.
        /// </summary>
        /// <remarks>
        /// Base64 encoding is a method of encoding binary data into a text format.
        /// This method checks if the input string can be successfully decoded from Base64 format.
        /// </remarks>
        /// <param name="str">The input string to check for Base64 encoding.</param>
        /// <returns>True if the input string is valid Base64-encoded data; otherwise, false.</returns>
        public static bool IsBase64(this string str) => Convert.TryFromBase64String(str, new(new byte[str.Length]), out _);

        /// <summary>
        /// Determines whether the specified string is equal to the current string, ignoring case.
        /// </summary>
        /// <param name="str">The current string.</param>
        /// <param name="comparison">The string to compare with the current string.</param>
        /// <returns>
        /// <c>true</c> if the specified string is equal to the current string, ignoring case; otherwise, <c>false</c>.
        /// </returns>
        public static bool EqualsIgnoreCase(this string str, string comparison) => str.Equals(comparison, StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Determines whether the specified substring occurs within the current string, ignoring case.
        /// </summary>
        /// <param name="str">The current string.</param>
        /// <param name="comparison">The substring to compare with the current string.</param>
        /// <returns>
        /// <c>true</c> if the specified substring occurs within the current string, ignoring case; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsIgnoreCase(this string str, string comparison) => str.Contains(comparison, StringComparison.InvariantCultureIgnoreCase);

        /// <summary>
        /// Converts the input string to PascalCase format.
        /// </summary>
        /// <remarks>
        /// PascalCase is a naming convention where the first letter of each word (except the first word)
        /// is capitalized, and there are no spaces between words.
        /// </remarks>
        /// <param name="str">The input string to convert.</param>
        /// <returns>A string in PascalCase format.</returns>
        public static string PascalCase(this string str)
        {
            if (str.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }

            if (str.IsAllUpper() || str.Length == 1)
            {
                return str;
            }

            var charArray = str.ToCharArray();
            var offset = 0;

            for (int i = 1; i < charArray.Length; i++)
            {
                if (
                    char.IsUpper(charArray[i]) ||
                    (char.IsDigit(charArray[i]) && !char.IsDigit(charArray[i - 1]) && !char.IsUpper(charArray[i - 1])))
                {
                    str = str.Insert(i + offset, " ");
                    offset++;
                }
            }

            return str;
        }

        /// <summary>
        /// Checks if each char in a string is uppercase.
        /// </summary>
        /// <param name="input">The string to check.</param>
        /// <returns>true if all characters are uppercase; otherwise, false.</returns>
        public static bool IsAllUpper(this string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsUpper(input[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Removes the specified texts from the string.
        /// </summary>
        /// <param name="str">The string to strip.</param>
        /// <param name="textToStrip">The texts to remove.</param>
        /// <returns>The modified string.</returns>
        public static string Strip(this string str, params string[] textToStrip)
        {
            foreach (var text in textToStrip)
            {
                str = str.Replace(text, string.Empty);
            }

            return str;
        }

        /// <summary>
        /// Removes the specified texts from the string.
        /// </summary>
        /// <param name="str">The string to strip.</param>
        /// <param name="textToStrip">The texts to remove.</param>
        /// <returns>The modified string.</returns>
        public static string Strip(this string str, StringComparison stringComparison, params string[] textToStrip)
        {
            foreach (var text in textToStrip)
            {
                str = str.Replace(text, string.Empty, stringComparison);
            }

            return str;
        }

        /// <summary>
        /// Determines whether the specified pattern is a valid regular expression.
        /// </summary>
        /// <param name="pattern">The pattern to test.</param>
        /// <returns>true if the pattern is a valid regular expression; otherwise, false.</returns>
        public static bool IsValidRegex(this string pattern)
        {
            if (pattern.IsNullOrWhiteSpace())
            {
                return false;
            }

            try
            {
                Regex.Match(string.Empty, pattern);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Removes empty strings from the input string array and returns a new array containing only non-empty strings.
        /// </summary>
        /// <param name="array">The input string array.</param>
        /// <returns>A new string array without empty strings.</returns>
        public static string[] RemoveEmpty(this string[] array)
        {
            var list = new List<string>();
            foreach (var item in array)
            {
                if (!item.IsNullOrWhiteSpace())
                {
                    list.Add(item);
                }
            }

            return [.. list];
        }

        /// <summary>
        /// Replaces the last occurrence of a specified string within the input string with another specified string, using the given string comparison option.
        /// </summary>
        /// <param name="str">The original string in which to search for the specified substring.</param>
        /// <param name="from">The substring to be replaced.</param>
        /// <param name="to">The substring to replace the last occurrence of <paramref name="from"/>.</param>
        /// <param name="stringComparison">Specifies the rules for the string comparison.</param>
        /// <returns>
        /// A new string that is equivalent to the original string except that the last occurrence of <paramref name="from"/> is replaced with <paramref name="to"/>.
        /// If the <paramref name="from"/> substring is not found, the original string is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the substring <paramref name="from"/> is not found in the original string.</exception>
        public static string ReplaceLast(this string str, string from, string to, StringComparison stringComparison)
        {
            var index = str.LastIndexOf(from, stringComparison);
            var newStr = str.Remove(index, from.Length);

            newStr = newStr.Insert(index, to);

            return newStr;
        }

        /// <summary>
        /// Replaces the first occurrence of a specified string within the input string with another specified string, using the given string comparison option.
        /// </summary>
        /// <param name="str">The original string in which to search for the specified substring.</param>
        /// <param name="from">The substring to be replaced.</param>
        /// <param name="to">The substring to replace the first occurrence of <paramref name="from"/>.</param>
        /// <param name="stringComparison">Specifies the rules for the string comparison.</param>
        /// <returns>
        /// A new string that is equivalent to the original string except that the first occurrence of <paramref name="from"/> is replaced with <paramref name="to"/>.
        /// If the <paramref name="from"/> substring is not found, the original string is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the substring <paramref name="from"/> is not found in the original string.</exception>
        public static string ReplaceFirst(this string str, string from, string to, StringComparison stringComparison)
        {
            var index = str.IndexOf(from, stringComparison);
            var newStr = str.Remove(index, from.Length);

            newStr = newStr.Insert(index, to);

            return newStr;
        }

        /// <summary>
        /// Checks if the provided string matches any of the specified string values
        /// using a case-insensitive comparison with invariant culture.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <param name="find">An array of string values to compare against.</param>
        /// <returns>
        /// <c>true</c> if the provided string matches any of the specified string values;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAny(this string str, params string[] find)
        {
            foreach (var item in find)
            {
                if (str.EqualsIgnoreCase(item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Retrieves the appropriate determiner ("a" or "an") for a given string, optionally in lowercase.
        /// </summary>
        /// <param name="str">The input string.</param>
        /// <param name="lowercase">Specifies whether the determiner should be in lowercase.</param>
        /// <returns>The determiner ("a" or "an") based on the first character of the input string.</returns>
        public static string GetDeterminer(this string str, bool lowercase)
        {
            if (str.Length > 0 && "aeiouAEIOU".Contains(str[0]))
            {
                return lowercase ? "an" : "An";
            }

            return lowercase ? "a" : "A";
        }

        /// <summary>
        /// Converts a string representation to an enumerated type of the specified <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The enumerated type to convert to.</typeparam>
        /// <param name="str">The string representation of the enumerated value.</param>
        /// <returns>
        /// An enumerated type of the specified <typeparamref name="T"/> parsed from the input string.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <typeparamref name="T"/> is not an enumerated type.
        /// </exception>
        public static T ToEnum<T>(this string str)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return (T)Enum.Parse(typeof(T), str.Strip(" "));
        }

        /// <summary>
        /// Attempts to convert a string representation to an enumerated type of the specified <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The enumerated type to convert to.</typeparam>
        /// <param name="str">The string representation of the enumerated value.</param>
        /// <returns>
        /// An enumerated type of the specified <typeparamref name="T"/> parsed from the input string,
        /// or the default value of <typeparamref name="T"/> if the conversion fails.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <typeparamref name="T"/> is not an enumerated type.
        /// </exception>
        public static T TryToEnum<T>(this string str)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return Enum.TryParse(str.Strip(" "), out T value) ? value : default;
        }
    }
}
