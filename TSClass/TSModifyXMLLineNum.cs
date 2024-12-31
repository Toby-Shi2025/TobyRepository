namespace MyFirstCSharpProject
{
    using System;
    using System.IO;
    using System.Xml.Linq;
    public class TSModifyXMLLineNum
    {
        static void Main()
        {
            // 指定 XML 文件的路径
        string filePath = @"C:\Users\Administrator\Desktop\TS_backup\ALFA\New Test12-10\UATTest - 副本.xml";

        // 检查文件是否存在
        if (!File.Exists(filePath))
        {
            Console.WriteLine("文件不存在。");
            return;
        }

        try
        {
            // 加载 XML 文件
            XDocument xmlDoc = XDocument.Load(filePath);

            // 查找所有 GLJour 节点
            var glJourElements = xmlDoc.Descendants("GLJour");

            int lineNumber = 1; // 初始化行号

            foreach (var glJour in glJourElements)
            {
                // 查找 LineNum 节点
                var lineNumElement = glJour.Element("LineNum");
                if (lineNumElement != null)
                {
                    // 设置新的 LineNum 值
                    lineNumElement.Value = lineNumber.ToString();
                    lineNumber++; // 自增行号
                }
            }

            // 保存修改后的 XML 文件
            xmlDoc.Save(filePath);

            Console.WriteLine("LineNum 已成功更新。");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"处理 XML 文件时出错: {ex.Message}");
        }
        }
    }   

}
