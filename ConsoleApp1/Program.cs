using System;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static private int LED_PIN = 18;
        static private int BTN_PIN = 23;
        static void Main(string[] args)
        {
            //var readButtonTask = Task.Run(() => ReadBtn());
            //var blindTask = Task.Run(() => Blink());
            //readButtonTask.Wait();

            ReadBtn();
            //blindTask.Wait();
        }
        static private void ReadBtn()
        { 
            using var controller = new GpioController();
            controller.OpenPin(BTN_PIN, PinMode.Input);
            bool noHigh = true;
             
            while (noHigh)
            {
                Console.WriteLine("Bouton non pressé");
                PinValue btnVal = controller.Read(BTN_PIN);

                if(btnVal == PinValue.Low){
                    noHigh = false;
                };

                if (!noHigh)
                {
                    Console.WriteLine("Bouton pressé !!!!");
                    Blink();
                }
                Thread.Sleep(200);
            }

            
        }
        static private void Blink()
        {
            using var controller = new GpioController();
            controller.OpenPin(LED_PIN, PinMode.Output);
            bool ledOn = true;
            while (true)
            {
                controller.Write(LED_PIN, ((ledOn) ? PinValue.High : PinValue.Low));
                Thread.Sleep(500);
                Console.WriteLine("High");
                ledOn = !ledOn;
                Console.WriteLine("Low");
            }
        }
    }
}
