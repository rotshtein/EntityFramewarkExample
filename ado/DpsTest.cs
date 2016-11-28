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
            this.BatchId = utils.GetBatchId(CurrentBatch);
        }

        public void Save()
        {
            using (var db = new RIT_QAEntities())
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
            return String.Format("{0,15},{1,15},{2,6}", SerialNo, MAC, Result ? "+" : "-");
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
