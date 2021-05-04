using System;
using System.Device.Gpio;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Test_Button
{
    public class Program
    {
        public static int LED_PIN = 18;
        public static int BTN_PIN = 23;
        public static PinValue StatBtn = PinValue.High;

        public static void Main(string[] args)
        {
            Console.WriteLine("Sonnette en fonctionnement");
            ButtonPress();
        }

        private static void ButtonPress()
        {
            using var controller = new GpioController();

            while (true)
            {
                controller.OpenPin(BTN_PIN, PinMode.Input);
                PinValue btnVal = controller.Read(BTN_PIN);
                controller.ClosePin(BTN_PIN);
                if (btnVal != StatBtn)
                {
                    if (btnVal == PinValue.Low)
                    {
                        Console.WriteLine("On t'appelle !");
                        //CallAPI();
                        controller.OpenPin(LED_PIN, PinMode.Output);
                        controller.Write(LED_PIN, PinValue.High);
                        Thread.Sleep(1000);
                        controller.ClosePin(LED_PIN);
                    }
                    else
                    {
                        controller.OpenPin(LED_PIN, PinMode.Output);
                        controller.Write(LED_PIN, PinValue.Low);
                        controller.ClosePin(LED_PIN);
                    }
                    StatBtn = PinValue.High;
                }
            }
        }

        //static private bool CallAPI()
        //{
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("http://192.168.29.38:5001/")
        //    HttpResponseMessage message = client.PostAsync("Sonnette", new StringContent("Dring dring").Result
        //    return message.IsSuccessStatusCode;
        //}

    }
}
