namespace MyFirstCSharpProject
{
    using System;

    class TSTest
    {
        static void Main(string[] args)
        {
            /// 提示用户输入第一个数字
            TSTest program = new TSTest();
            program.Run();
        }

        private void Run()
        {
            Console.Write("请输入第一个数字: ");
            string? input1 = Console.ReadLine();

            // 检查输入是否为 null 或空
            if (string.IsNullOrWhiteSpace(input1))
            {
                Console.WriteLine("输入无效，请输入一个有效的数字。");
                return;
            }

            // 将输入的字符串转换为整数
            int number1 = int.Parse(input1);

            // 提示用户输入第二个数字
            Console.Write("请输入第二个数字: ");
            string? input2 = Console.ReadLine();

            // 检查输入是否为 null 或空
            if (string.IsNullOrWhiteSpace(input2))
            {
                Console.WriteLine("输入无效，请输入一个有效的数字。");
                return;
            }

            // 将输入的字符串转换为整数
            int number2 = int.Parse(input2);

            // 计算两个数字的和
            int sum = number1 + number2;

            // 输出结果
            Console.WriteLine($"两个数字的和是: {sum}");
        }
    }

}