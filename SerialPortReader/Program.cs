using System;
using System.IO.Ports;
using System.Speech.Synthesis;
using System.Threading;

namespace SerialPortReader
{
    class Program
    {
        static SpeechSynthesizer speechSynthesizerObj;

        static void Main(string[] args)
        {
            speechSynthesizerObj = new SpeechSynthesizer();

            SerialPort arduinoPort = new SerialPort();
            arduinoPort.BaudRate = 9600;
            arduinoPort.PortName = "COM3";
            arduinoPort.Open();

            while (true)
            {
                var portValue = arduinoPort.ReadLine().Replace('\r','\n');
                TextToSpeech(portValue);
                Console.WriteLine(portValue);
            }
        }

        static void TextToSpeech(string value)
        {
            try
            {
                //Disposes the SpeechSynthesizer object   
                speechSynthesizerObj.Dispose();
                if (value != "")
                {
                    speechSynthesizerObj = new SpeechSynthesizer();
                    var distance = Convert.ToDouble(value);
                    if (distance >= 200 || distance <= 0)
                    {
                        //Asynchronously speaks the contents for no obstacle
                        speechSynthesizerObj.SpeakAsync("No Obstacle found nearby");
                    }
                    else
                    {
                        //Asynchronously speaks the contents with distance value 
                        speechSynthesizerObj.SpeakAsync("Obstacle found at a distance of " + Convert.ToString(distance) + "centimeters");                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
