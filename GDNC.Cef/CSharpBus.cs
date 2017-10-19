using System;
using System.Threading;
using System.Threading.Tasks;
using CefSharp;

namespace GDNC.Cef
{
    public class CSharpBus
    {
        public string Echo(string echo)
        {
            return echo;
        }

        public ComplexObject Complex()
        {
            return new ComplexObject
            {
                Value1 = "1",
                Value2 = "2",
                SimpleObject = new SimpleObject
                {
                    StringValue = "3",
                    IntValue = 1,
                    FloatValue = 2.3f,
                    DoubleValue = 4.5f
                }
            };
        }

        public string JsDelay(int delay = 1000)
        {
            Thread.Sleep(delay);

            return "result";
        }

        public void Throw(string error)
        {
            throw new Exception(error);
        }

        public async void AsyncDelay(string echo, int delay, bool success, IJavascriptCallback resolve, IJavascriptCallback reject)
        {
            await Task.Delay(delay);

            if (success)
            {
                if (!resolve.IsDisposed)
                {
                    await resolve.ExecuteAsync(echo);
                }
            }
            else
            {
                if (!reject.IsDisposed)
                {
                    await reject.ExecuteAsync("Error");
                }
            }
        }
    }
}
