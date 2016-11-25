using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using log4net;
using log4net.Config;

[assembly: XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]

namespace ado
{
    internal class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void Main() //string[] args)
        {
            DpsTest dps = new DpsTest();

            dps.Start("22566322221", "5544223");

            dps.MAC = "040011223344";

            dps.Save();
#if false
            // Create database connection
            Logger.Info("Hello");
            using (var db = new ritEntities())
            {
                var test = db.ElectricalTests.Create();
                var batch = db.Batches.Create();

            #region Delete all

#if False
                var testsToDelete = db.ElectricalTests.Where(x => x.SerialNo != "" );
                db.ElectricalTests.RemoveRange(testsToDelete);

                var batchsToDelete = db.Batches.Where(x => x.BatchNumber != "");
                db.Batches.RemoveRange(batchsToDelete);
                db.SaveChanges();
#endif

            #endregion

            #region Create test recors

                // Get unique random serial number
                do
                {
                    test.SerialNo = new Random().Next(1000, 10000000).ToString();
                } while (db.ElectricalTests.Find(test.SerialNo) != null);

                // Create new MAC based on the last in table
                PhysicalAddress lastMac;
                byte[] macArray = {0x04, 0x11, 0x22, 0x33, 0x44, 0xFF};
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
                test.MAC = lastMac.ToString();

                // Set the result
                test.Result = !test.MAC.Contains("e");

            #endregion

            #region Set batch number

                //Set batch number
                var BatchNumber = "22566322221";
                Batch b = null;

            #endregion

            #region Create or find an existing batch record

                // Checed in new batch
                try
                {
                    b = db.Batches.First(x => x.BatchNumber == BatchNumber);
                }
                catch (Exception ex)
                {
                    Logger.Error("This should happend only when the Batch table is empty", ex);
                }

                // and create if it is new
                if (b == null)
                {
                    batch.BatchNumber = BatchNumber;
                    batch.Date = DateTime.Now;
                    db.Batches.Add(batch);
                    db.SaveChanges();
                }
                else
                {
                    batch = b;
                }

            #endregion

            #region Save to DB

                test.BatchId = batch.Id;
                batch.ElectricalTests.Add(test);
                db.SaveChanges();

            #endregion

                var et = batch.ElectricalTests.ToList();
                et.ForEach(
                    r => Console.WriteLine("SerialNo: " + r.SerialNo + "\t\tBatch number: " + r.Batch.BatchNumber));
            }
#endif
        }
    }
}