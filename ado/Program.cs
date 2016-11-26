using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using log4net;
using log4net.Config;
using System.Collections.Generic;

[assembly: XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]

namespace ado
{
    internal class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void Main() //string[] args)
        {
            Console.WriteLine("The version of mscorlib.dll is: {0}",
              typeof(Program).Assembly.GetName().Version);

            DpsTest dps = new DpsTest();

            dps.Start("22566322221", GetSerial());

            dps.MAC = GetMAC();

            dps.Result = GetResult();

            dps.Save();

            #region How to use the database
            Console.WriteLine("Entered: " + dps.ToString());

            using (var db = new ritEntities())
            {
                foreach (ElectricalTest e in  db.Batches.Where(x => x.Id == dps.BatchId).First().ElectricalTests)
                    Console.WriteLine( String.Format("{0,15}\t{1,15}\t{2,6}", e.SerialNo , e.MAC ,e.Result ? "+" : "-"));
            }
            #endregion
        }


        #region Get info (Serial, MAC and Result) for example
        private static string GetSerial()
        {
            using (var db = new ritEntities())
            {
                var test = db.ElectricalTests.Create();
                do
                {
                    test.SerialNo = new Random().Next(1000, 10000000).ToString();
                } while (db.ElectricalTests.Find(test.SerialNo) != null);

                return test.SerialNo;
            }
        }

        private static string GetMAC()
        {
            using (var db = new ritEntities())
            {
                // Create new MAC based on the last in table
                PhysicalAddress lastMac;
                byte[] macArray = { 0x04, 0x11, 0x22, 0x33, 0x44, 0xFF };
                try
                {
                    lastMac = PhysicalAddress.Parse(db.ElectricalTests.Min(r => r.MAC).ToUpper());
                    macArray = lastMac.GetAddressBytes();
                    macArray[5] -= 1; // Chnage the MAC address
                }
                catch (Exception ex)
                {
                    Logger.Warn("This should happend only when the ElectricalTest table is empty", ex);
                }

                lastMac = PhysicalAddress.Parse(string.Concat(Array.ConvertAll(macArray, j => j.ToString("X2"))));
                return lastMac.ToString();
            }
            #endregion
        }

        private static bool GetResult()
        {
            return (new Random().Next(1, 10) != 1);
        }
    }
}