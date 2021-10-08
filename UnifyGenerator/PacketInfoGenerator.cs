using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace UnifyGenerator
{
    public class PacketInfoGenerator
    {
        private const string _templatePath =".//..//..//..//Templates";
        private const string _outPath = ".//..//..//..//..//UnifyPackets//";

        public PacketInfoGenerator()
        {
            Console.Out.WriteLine("What path should the PacketInfo have?");
            Console.Out.WriteLine("Example: 'General.Character.CharInfo' will be named CharInfo and be placed in the General.Character namespace");
            Console.Out.WriteLine("\n\n");
            var packageInfo = Console.ReadLine();

            var info = new List<Tuple<TypeCode, string>>();
            Console.Out.WriteLine("\n\n");
            Console.Out.WriteLine("Enter a Property in the syntax Type:Name:");
            Console.Out.WriteLine("Example: int:x1");
            Console.Out.WriteLine("If you just press enter the class will be generated!");
            Console.Out.WriteLine("\n\n");
            while (true)
            {
                

                var input = Console.ReadLine();
                if (input == "") break;

                var code = GetTypeCode(input.Split(':')[0]);
                var name = input.Split(':')[1].ToUpperInvariant();
                
                info.Add(new Tuple<TypeCode, string>(code,name));
                Console.Out.WriteLine("Property added!\n");
            }

            GeneratePacketInfo(packageInfo,info);
        }

        private void GeneratePacketInfo(string packagePath,List<Tuple<TypeCode, string>> info)
        {
            var text = File.ReadAllText(_templatePath + "//PacketInfoTemplate.txt");
            text = text.Replace("{{PacketInfoName}}", GetName(packagePath));
            text = text.Replace("{{NameSpacePath}}", GetPath(packagePath));

            var propTxt = GeneratePropertyText(info);
            text = text.Replace("{{Properties}}",propTxt);

            var constTxt = GenerateConstructor(info);
            text = text.Replace("{{Deserialize Properties}}",constTxt);
            
            var methodTxt = GenerateGetByteMethod(info);
            text = text.Replace("{{Serialize Properties}}",methodTxt);

            Console.Out.WriteLine(text);
            File.WriteAllText(_outPath+GetOutPath(packagePath)+".cs",text);
        }

        private string GeneratePropertyText(List<Tuple<TypeCode, string>> info)
        {
            
            var sb = new StringBuilder();
            foreach (var property in info)
            {
                sb.Append("public readonly "+ GetSimpleName(property.Item1) + " " + property.Item2+";");
                sb.Append("\n\t\t");
            }

            return sb.ToString();

        }

        private string GenerateConstructor(List<Tuple<TypeCode, string>> info)
        {
            var sb = new StringBuilder();
            foreach (var property in info)
            {
                sb.Append(property.Item2+" = buffer.Read"+ GetMethodName(property.Item1)+"();");
                sb.Append("\n\t\t\t");
            }

            return sb.ToString();
        }

        private string GenerateGetByteMethod(List<Tuple<TypeCode, string>> info)
        {
            var sb = new StringBuilder();
            foreach (var property in info)
            {
                sb.Append("buffer.Write"+ GetMethodName(property.Item1)+"("+property.Item2+");");
                sb.Append("\n\t\t\t");
            }

            return sb.ToString();
        }

        private string GetPath(string packagePath)
        {
            var index = packagePath.LastIndexOf('.');
            return packagePath.Substring(0, index);
        }

        private string GetOutPath(string packagePath)
        {
            return packagePath.Replace(".", "//");
        }
        private string GetName(string packagePath)
        {
            var split = packagePath.Split('.');
            return split[split.Length-1];
        }
        private string GetSimpleName(TypeCode code)
        {
            switch (code)
            {
                case TypeCode.Empty:
                    throw new ArgumentOutOfRangeException(nameof(code), code, null);
                case TypeCode.Object:
                    throw new ArgumentOutOfRangeException(nameof(code), code, null);
                case TypeCode.DBNull:
                    throw new ArgumentOutOfRangeException(nameof(code), code, null);
                case TypeCode.Boolean:
                    return "bool";
                case TypeCode.Char:
                    return "char";
                case TypeCode.SByte:
                    return "sbyte";
                case TypeCode.Byte:
                    return "byte";
                case TypeCode.Int16:
                    return "short";
                case TypeCode.UInt16:
                    return "ushort";
                case TypeCode.Int32:
                    return "int";
                case TypeCode.UInt32:
                    return "uint";
                case TypeCode.Int64:
                    return "long";
                case TypeCode.UInt64:
                    return "ulong";
                case TypeCode.Single:
                    return "float";
                case TypeCode.Double:
                    return "double";
                case TypeCode.Decimal:
                    throw new ArgumentOutOfRangeException(nameof(code), code, null);
                case TypeCode.DateTime:
                    throw new ArgumentOutOfRangeException(nameof(code), code, null);
                case TypeCode.String:
                    return "string";
                default:
                    throw new ArgumentOutOfRangeException(nameof(code), code, null);
            }
        }
        private string GetMethodName(TypeCode code)
        {
            switch (code)
            {
                case TypeCode.Empty:
                    throw new ArgumentOutOfRangeException(nameof(code), code, null);
                case TypeCode.Object:
                    throw new ArgumentOutOfRangeException(nameof(code), code, null);
                case TypeCode.DBNull:
                    throw new ArgumentOutOfRangeException(nameof(code), code, null);
                case TypeCode.Boolean:
                    return "Bool";
                case TypeCode.Char:
                    return "Char";
                case TypeCode.SByte:
                    return "Sbyte";
                case TypeCode.Byte:
                    return "Byte";
                case TypeCode.Int16:
                    return "Int16";
                case TypeCode.UInt16:
                    return "UInt16";
                case TypeCode.Int32:
                    return "Int32";
                case TypeCode.UInt32:
                    return "UInt32";
                case TypeCode.Int64:
                    return "Int64";
                case TypeCode.UInt64:
                    return "UInt64";
                case TypeCode.Single:
                    return "Float";
                case TypeCode.Double:
                    return "Double";
                case TypeCode.Decimal:
                    throw new ArgumentOutOfRangeException(nameof(code), code, null);
                case TypeCode.DateTime:
                    throw new ArgumentOutOfRangeException(nameof(code), code, null);
                case TypeCode.String:
                    return "String";
                default:
                    throw new ArgumentOutOfRangeException(nameof(code), code, null);
            }
        }
        private TypeCode GetTypeCode(string input)
        {
            switch (input)
            {
                case "int": return TypeCode.Int32;
                case "uint": return TypeCode.UInt32;
                case "short": return TypeCode.Int16;
                case "ushort": return TypeCode.UInt16;
                case "long": return TypeCode.Int64;
                case "ulong": return TypeCode.UInt64;
                case "bool": return TypeCode.Boolean;
                case "float": return TypeCode.Single;
                case "double": return TypeCode.Double;
                case "string": return TypeCode.String;
                case "char" : return TypeCode.Char;
                case "byte" : return TypeCode.Byte;
                case "sbyte" : return TypeCode.SByte;
                default: return TypeCode.String;
            }
        }
    }
}
