﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace websiteAvailability
{
    interface IWebChecker
    {
        bool PingHost(string nameOrAddress);
    }

    public class SimpleWebChecker: IWebChecker
    {
        public bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
                var pingTime = pingable ? reply.RoundtripTime : 0;
                Console.WriteLine("'{0}' pingable is {1}, time is {2})", nameOrAddress, pingable, pingTime);
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            return pingable;
        }
    }
}