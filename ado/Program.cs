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
            string testedSerial = GetSerial();

            #region Add Electrical test result
            DpsTest dps = new DpsTest();

            dps.Start("22566322221", testedSerial);

            dps.MAC = GetMAC();

            dps.Result = GetResult();

            dps.Save(); 
            #endregion

            #region How to use the Electrical test database
            Console.WriteLine("Entered: " + dps.ToString());

            using (var db = new RIT_QAEntities())
            {
                foreach (ElectricalTest e in  db.Batches.Where(x => x.Id == dps.BatchId).First().ElectricalTests)
                    Console.WriteLine( String.Format("{0,15}\t{1,15}\t{2,6}", e.SerialNo , e.MAC ,e.Result ? "+" : "-"));
            }
            #endregion

            #region How to use the Calibration test database
            DpsCalibration d = new DpsCalibration("22566322221", testedSerial);
            d.AddData(1, 0.99887766, 1223, 1222, 7);
            d.AddData(1.5, 1.448877, 223, 222, 7.1);
            d.AddData(2, 1.99887766, 13, 12, 7.2);
            d.Save();
            #endregion

            #region Detached use
            Console.WriteLine(utils.RecordToString(d.GetDpsInfo()));
            Console.WriteLine(utils.RecordToString(d.GetCalibrationBatch()));
            foreach (CalibrationData cd in d.GetCalibrationData())
            {
                Console.WriteLine(utils.RecordToString(cd));
            }
            #endregion

            #region Attached use
            using (var db = new RIT_QAEntities())
            {
                Calibration dd = db.Calibrations.FirstOrDefault(x => x.SerialNo == testedSerial);

                foreach (CalibrationData cd in dd.CalibrationDatas.ToList())
                {
                    Console.WriteLine("Setpoint {0}, Pressure{1},temp{3}", cd.SetPoint, cd.Pressure, cd.Temp);
                }
            }
            #endregion

        }

        #region Get info (Serial, MAC and Result) for example
        private static string GetSerial()
        {
            using (var db = new RIT_QAEntities())
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
            using (var db = new RIT_QAEntities())
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
            
        }

        private static bool GetResult()
        {
            return (new Random().Next(1, 10) != 1);
        }
        #endregion
    }
}