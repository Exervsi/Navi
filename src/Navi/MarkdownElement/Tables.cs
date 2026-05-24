using Navi.InfoItems;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Navi.MarkdownElement
{
    /// <summary>
    /// Provides static methods for generating and formatting Markdown tables from various data sources.
    /// </summary>
    public static class Tables
    {
        /// <summary>
        /// Attempts to create a Markdown table from the specified headers and values.
        /// </summary>
        /// <param name="headers">An array of column header strings.</param>
        /// <param name="values">A two-dimensional array containing the table's cell values.</param>
        /// <param name="result">The resulting Markdown table as a string, if successful.</param>
        /// <returns>True if the table was created successfully; otherwise, false.</returns>
        public static bool TryCreateTable(string[] headers, string[,] values, out string result)
        {
            result = string.Empty;

            int numberOfColumns = headers.Length;

            if (numberOfColumns != values.GetLength(1))
                return false;

            int[] size = new int[numberOfColumns];

            //lets make sure the columns are big enough to fit the header and all the values
            for (int i = 0; i < numberOfColumns; i++)
            {
                size[i] = headers[i].Length;
                for (int j = 0; j < values.GetLength(0); j++)
                {
                    int possibleLength = values[j, i].Length;
                    if (possibleLength > size[i])
                        size[i] = possibleLength;
                }
            }

            //write the header
            for (int i = 0; i < numberOfColumns; i++)
            {
                result += "| " + headers[i] + ' ';
                int repeat = size[i] - headers[i].Length;
                for (int j = 0; j < repeat; j++)
                    result += ' ';
            }
            //write the divider
            result += "|\n";
            for (int i = 0; i < numberOfColumns; i++)
            {
                result += "| ";
                for (int j = 0; j < size[i]; j++)
                    result += '-';
                result += ' ';
            }
            result += "|\n";

            //write the values
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    result += "| " + values[i, j] + ' ';
                    int repeat = size[j] - values[i, j].Length;
                    for (int k = 0; k < repeat; k++)
                        result += ' ';
                }
                result += "|\n";
            }
            result += '\n';
            return true;
        }




        /// <summary>
        /// Attempts to extract the values of an enum type as a table, with options to include string names, numeric values, and attributes.
        /// </summary>
        /// <param name="enumItem">The enum type to extract values from.</param>
        /// <param name="result">A two-dimensional array containing the extracted values.</param>
        /// <param name="includeEnumString">Whether to include the enum's string names as columns.</param>
        /// <param name="includeEnumNumber">Whether to include the enum's numeric values as columns.</param>
        /// <param name="includeAttributes">Whether to include attribute values as columns.</param>
        /// <returns>True if the values were extracted successfully; otherwise, false.</returns>
        public static bool TryGetEnumTableValues(this InfoItems.Type enumItem, out string[,] result, bool includeEnumString = true, bool includeEnumNumber = true, bool includeAttributes = false)
        {
            result = new string[0, 0];
            if (enumItem.Data.IsEnum is false)
                return false;

            //the enums rows will equal the fields
            Field[] fields = enumItem.Fields;

            int numberOfRows = fields.Length - 1;
            int numberOfColumns = 0;

            if (includeEnumString)
                numberOfColumns++;
            if (includeEnumNumber)
                numberOfColumns++;


            if (includeAttributes)
            {
                int numberOfAttributes = 0;
                //lets skip the first field as it is the default value of the enum and will not have any attributes
                foreach ( Field field in fields.Skip(1))
                    foreach (InfoItems.Attribute attribute in field.Attributes)
                    {
                        int count = 0;
                        foreach (object value in attribute.Values)
                            count++;

                        if (count > numberOfAttributes)
                            numberOfAttributes = count;
                    }
                numberOfColumns += numberOfAttributes;
            }

            result = new string[numberOfRows, numberOfColumns];


            for(int i = 0; i < numberOfRows; i++)
            {
                int j = 0;
                if (includeEnumString)
                    result[i, j++] = fields[i+1].Data.Name;
                if (includeEnumNumber)
                    result[i, j++] = fields[i +1].Data.GetRawConstantValue().ToString();

                if (includeAttributes)
                    foreach (InfoItems.Attribute attribute in fields[i+1].Attributes)
                        foreach (object value in attribute.Values)
                                result[i, j++] = value.ToString();
            }

            return true;


        }

        /// <summary>
        /// Attempts to extract table values from an array of <see cref="InfoItem"/> objects, producing a two-column table.
        /// </summary>
        /// <param name="items">The array of <see cref="InfoItem"/> objects to extract values from.</param>
        /// <param name="result">A two-dimensional array containing the extracted values.</param>
        /// <returns>True if the values were extracted successfully; otherwise, false.</returns>
        public static bool TryGetTableValues2(InfoItem[] items, out string[,] result)
        {
            result = new string[0, 0];

            if (items.Length == 0)
                return false;



            int numberOfRows = items.Length;
            int numberOfColumns = 2;

            result = new string[numberOfRows, 2];

            result = new string[numberOfRows, numberOfColumns];


            for (int i = 0; i < numberOfRows; i++)
            {
                if (items[i] is Namespace ns)
                {
                    string name = ns.Name;
                    string path = Navi.Core.SystemExtensions.String.PathBuilders.BuildChildUrl(ns);
                    result[i, 0] = Navi.Core.SystemExtensions.String.PathBuilders.HyperLink(name, path);
                }
                if (items[i] is InfoItems.Type type)
                {
                    string name = type.Name;
                    string path = Navi.Core.SystemExtensions.String.PathBuilders.BuildChildUrl(type);
                    result[i, 0] = Navi.Core.SystemExtensions.String.PathBuilders.HyperLink(name, path);
                }
                if (items[i] is Constructor constructor)
                {
                    string name = constructor.Parent.Name + "(" + string.Join(", ", constructor.Parameters.Select(p => '`' + p.Data.ParameterType.Name + '`')) + ')';
                    string path = Navi.Core.SystemExtensions.String.PathBuilders.BuildChildUrl(constructor);
                    result[i, 0] = Navi.Core.SystemExtensions.String.PathBuilders.HyperLink(name, path);
                }
                result[i,1] = items[i].Value.Replace("\n", "");
            }

            return true;
        }

        /// <summary>
        /// Attempts to extract table values from an array of <see cref="InfoItem"/> objects, producing a three-column table.
        /// </summary>
        /// <param name="items">The array of <see cref="InfoItem"/> objects to extract values from.</param>
        /// <param name="result">A two-dimensional array containing the extracted values.</param>
        /// <returns>True if the values were extracted successfully; otherwise, false.</returns>
        public static bool TryGetTableValues3(InfoItem[] items, out string[,] result)
        {
            result = new string[0, 0];

            if (items.Length == 0)
                return false;

            int numberOfRows = items.Length;
            int numberOfColumns = 3;

            result = new string[numberOfRows, 3];

            result = new string[numberOfRows, numberOfColumns];


            for (int i = 0; i < numberOfRows; i++)
            {
                int count = 0;
                if (items[i] is Property property)
                {
                    result[i, 0] = '`' + property.Type.Name.ToString() + '`';
                    result[i, 1] = items[i].Name;
                }
                if (items[i] is Field field)
                {
                    result[i, 0] = '`' + field.Type.Name.ToString() + '`';
                    result[i, 1] = items[i].Name;
                }
                if (items[i] is Method method)
                {
                    result[i, 0] = '`' + method.Return.Type.ToString() + '`';
                    result[i, 1] = Navi.Core.SystemExtensions.String.PathBuilders.HyperLink(items[i].Name, Navi.Core.SystemExtensions.String.PathBuilders.BuildUrl(items[i]));
                }
                result[i, 2] = items[i].Value.Replace("\n", "");
            }

            return true;
        }


    }
}
