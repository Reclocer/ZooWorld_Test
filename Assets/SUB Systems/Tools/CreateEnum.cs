using System.Collections.Generic;
using System.IO;

namespace SpineCustom.Generate
{
    public static class CreateEnum
    {
        public static string Path = "Assets/Plugins/SpineAnimationPlugin/Generated/AnimationEnum/";
    
        public static void Create(string enumName, List<string> allName, string path)
        {
  
            using (StreamWriter streamWriter = new StreamWriter(path + enumName+".cs"))
            {
                AddEnum(streamWriter,allName,enumName);
            }
        }

        private static void AddEnum(StreamWriter streamWriter,List<string> allName,string enumName)
        {
            streamWriter.WriteLine("namespace SpineCustom.Enum");
            streamWriter.WriteLine("{");
                streamWriter.WriteLine("    public enum " + enumName);
                streamWriter.WriteLine("    {");
                foreach (var component in allName)
                {
                    streamWriter.WriteLine(" \t" + component+",");
                }
                streamWriter.WriteLine("    }");
            streamWriter.WriteLine("}");
            streamWriter.Close();
        }

        public static void CreateSound(string enumName, List<string> allName,string path)
        {

            using(StreamWriter streamWriter = new StreamWriter(path + enumName + ".cs"))
            {
                AddEnumSound(streamWriter, allName, enumName);
            }
        }

        private static void AddEnumSound(StreamWriter streamWriter, List<string> allName, string enumName)
        {

            streamWriter.WriteLine("    public enum " + enumName);
            streamWriter.WriteLine("    {");
            foreach(var component in allName)
            {
                streamWriter.WriteLine("     \t" + component + ",");
            }
            streamWriter.WriteLine("    }");
            streamWriter.Close();
        }
    }
}
