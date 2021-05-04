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
                        if (CallAPI())
                        {
                            Console.WriteLine("On t'appelle !");
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
                    }
                    StatBtn = PinValue.High;
                }
            }
        }

        private static bool CallAPI()
        {

            HttpClient client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://192.168.29.90:5001"),
            };

            HttpResponseMessage response = client.Send(request);

            if (response.IsSuccessStatusCode)
            {
                Console.Write("Request succeed");
                return true;
            }
            else
            {
                Console.Write("Request failed");
                return false;
            }
        }
    }
}
