using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.MT940Parser.Factories
{
    public static class TagFactory
    {
        private static readonly string[] ValidTags = { "20", "21", "25", "28", "28C", "60m", "60F", "60M", "61", "86", "62F", "62m", "62M", "64", "65" };

        public static string Create(string rawLine)
        {
            var tag = rawLine.Substring(0, rawLine.IndexOf(":", 1)).Replace(":", "");
            if (!ValidTags.Contains(tag)) throw new InvalidDataException($"Bilinmeyen satır tipi: {tag}");
            return tag;
        }
    }
}