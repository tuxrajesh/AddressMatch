using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AddressMatch
{
    /// <summary>
    /// Contains the algorithms used in Address Matching
    /// Resources: http://www.levenshtein.net/; http://dotnetperls.com/levenshtein
    /// </summary>
    public class Helper
    {        
        /// <summary>
        /// Compute Levenshtein Distance between two strings
        /// </summary>
        /// <param name="source">Source String</param>
        /// <param name="target">Target String</param>
        /// <returns>Levenshtein Distance</returns>
        public static int ComputeLevenshteinDistance(string source, string target)
        {
            if (source == null) source = string.Empty;
            if (target == null) target = string.Empty;

            int sourceLength = source.Length;
            int targetLength = target.Length;

            int[,] matrix = new int[sourceLength + 1, targetLength + 1];

            // Source is empty; Total additions = Target Length.
            if (sourceLength == 0)
                return targetLength;

            // Target is empty; Total additions = Source Length.
            if (targetLength == 0)
                return sourceLength;

            // Contruct the Levenshtein Matrix; First Column & First Row
            for (int i = 0; i <= sourceLength; matrix[i, 0] = i++) { }
            for (int j = 0; j <= targetLength; matrix[0, j] = j++) { }

            // Construct the remaining rows and column values
            // Loop starts at 1 coz, 0 has already been defined.
            for (int i = 1; i <= sourceLength; i++)
            {
                for (int j = 1; j <= targetLength; j++)
                {
                    // calculate cost; if target = source at the index then 0 else 1.
                    int cost = target[j - 1] == source[i - 1] ? 0 : 1;

                    matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), matrix[i - 1, j - 1] + cost);
                }
            }

            // Last Element in the Matrix is the Levenstein Distance.
            return matrix[sourceLength, targetLength];
        }

        /// <summary>
        /// Append a string with the specified character and string
        /// </summary>
        /// <param name="original">Original String</param>
        /// <param name="append">String to be appended</param>
        /// <param name="aenmAppendCharacter">Append Character</param>
        /// <returns>Appended String</returns>
        public static string AppendStringWithChar(string original, string append, enmAppendCharacter aenmAppendCharacter)
        {
            if (string.IsNullOrEmpty(original))
                return append;
            else if (string.IsNullOrEmpty(append))
                return original;
            else
            {
                switch (aenmAppendCharacter)
                {
                    case enmAppendCharacter.Comma:
                        return string.Format("{0},{1}", original, append);
                    case enmAppendCharacter.CommaSpace:
                        return string.Format("{0}, {1}", original, append);
                    case enmAppendCharacter.Space:
                        return string.Format("{0} {1}", original, append);
                    case enmAppendCharacter.Dash:
                        return string.Format("{0}-{1}", original, append);
                    case enmAppendCharacter.SemiColon:
                        return string.Format("{0};{1}", original, append);
                    default:
                        return string.Format("{0}{1}", original, append);
                }
            }
        }

        /// <summary>
        /// Merge Source Collection to Target Collection
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="sourceCollection">Source Collection</param>
        /// <param name="targetCollection">Target Collection</param>
        /// <returns>Merged Target Collection</returns>
        public Collection<T> MergeCollection<T>(Collection<T> sourceCollection, Collection<T> targetCollection)
        {
            if (sourceCollection == null)
                return targetCollection;

            if (targetCollection == null)
                targetCollection = new Collection<T>();

            foreach (T element in sourceCollection)
                targetCollection.Add(element);

            return targetCollection;
        }
    }
}
