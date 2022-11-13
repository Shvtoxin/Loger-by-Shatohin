using System;
using Serilog;

namespace Loger
{
    class Program
    {
        static void Main(string[] args)
        {
            
            decimal well = 61.40m;
            decimal com1 = 8.0m;
            decimal com2 = 0.37m;
            var chek = false;
            var contributed = 0.0m;
            var log = new LoggerConfiguration().MinimumLevel.Information().Enrich.WithProperty("Курс доллара:", well).WriteTo.Seq("http://localhost:5341/", apiKey: "zf6yDO6iaMlMDehfOV97").CreateLogger();
            while (!chek)
            {
                log.Information($"Введеная сумма для обмена");
                Console.Write("Введите сумму в долларах для обмена: ");
                chek = decimal.TryParse(Console.ReadLine(), out contributed);
                if(!chek)
                {
                    log.Error("Клиент ввел не корректную сумму для обмена.");
                }
            }
            log.Information($"Клиент ввел корректную сумму:{contributed}");
            var translation = contributed * well;
            decimal issued;
            if (contributed > 500)
            {
                Console.WriteLine("Введеная сумма больше 500 долларов. Коммисия составит 0,37%.");
                issued = (translation - ((translation * com2) / 100));
                Console.WriteLine("Получено: " + issued + "рублей.");
            }
            else
            {
                Console.WriteLine("Введеная сумма меньше 500 долларов. Коммисия составит 8 рублей.");
                issued = (translation - com1);
                Console.WriteLine("Получено: " + issued + "рублей.");
            }
            log.Information($"Перевод в $ прошел успешно, получено:{issued}");
            Console.ReadLine();
        }
    }

}
