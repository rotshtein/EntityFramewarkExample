using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ado
{
    class DpsTest : ElectricalTest
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Start(string CurrentBatch, string Serial)
        {
            Logger.Debug("DpsTest started");
            this.StartDate = DateTime.Now;
            this.SerialNo = Serial;

            using (var db = new ritEntities())
            {
                Batch ExistBatch = null;
                var b = CurrentBatch;
                try
                {
                    ExistBatch = db.Batches.FirstOrDefault(x => x.BatchNumber == b);
                }
                catch (Exception ex)
                {
                    Logger.Error("This should happend only when the Batch table is empty", ex);
                }

                // and create if it is new
                if (ExistBatch == null)
                {
                    Batch _currentBatch = db.Batches.Create();
                    _currentBatch.BatchNumber = CurrentBatch;
                    _currentBatch.Date = this.StartDate;
                    db.Batches.Add(_currentBatch);
                    db.SaveChanges();
                    this.BatchId = _currentBatch.Id;
                    Logger.Debug("New batch created. Batch Number = " + this.Batch.BatchNumber);
                }
                else
                {
                    this.BatchId = ExistBatch.Id;
                }
            }
        }

        public void Save()
        {
            using (var db = new ritEntities())
            {
                try
                {
                    if (this.EndDate == null)
                        this.EndDate = DateTime.Now;

                    db.ElectricalTests.Add(GetBase());
                    db.SaveChanges();
                    Logger.Debug("Test saved");
                }
                catch (Exception ex)
                {
                    Logger.Info("Save DPS test result failed. Please that SErialNo, MAC and Result are field", ex);
                    Logger.Error("", ex);
                }
            }
        }

        public override string ToString()
        {
            string s = String.Format("{0},{1},{2}", SerialNo, MAC, Result ? "+" : "-");
            return s;
        }

        private ElectricalTest GetBase()
        {
            ElectricalTest e = new ElectricalTest();
            foreach (var prop in e.GetType().GetProperties())
                prop.SetValue(e, prop.GetValue(this, null));
            return e;
        }
    }
}
