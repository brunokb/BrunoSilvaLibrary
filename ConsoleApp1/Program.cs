using BrunoSilvaLibrary;
using BrunoSilvaLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IUserService> channelFactory = new ChannelFactory<IUserService>("UserServiceEndpoint");
            IUserService proxy = channelFactory.CreateChannel();
            var test = proxy.UserReturn();
                foreach (var t in test)
            {
                Console.WriteLine(t.ToString());
            }
         
        }
    }
}
