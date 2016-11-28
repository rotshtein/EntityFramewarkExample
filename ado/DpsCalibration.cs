using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ado
{
    class DpsCalibration
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        Calibration DpsDetails = null;
        List<CalibrationData> DataRecords = null;
        public DpsCalibration(string CurrentBatch, string Serial)
        {
            DpsDetails = new Calibration();
            DpsDetails.SerialNo = Serial;
            DpsDetails.BatchId = utils.GetBatchId(CurrentBatch);
            DpsDetails.Date = DateTime.Now;

            DataRecords = new List<CalibrationData>();
        }

        public void AddData(double setPoint, double pressure, int rightA2D, int leftA2D, double temp)
        {
            DataRecords.Add(new CalibrationData { SerialNo = DpsDetails.SerialNo, SetPoint = setPoint, Pressure = pressure,
                                                  RightA2D = rightA2D, LeftA2D = leftA2D, Temp = temp});

        }

        public void Save()
        {
            using (var db = new RIT_QAEntities())
            {
                try
                {
                    db.Calibrations.Add(DpsDetails);
                    foreach (CalibrationData record in DataRecords)
                        db.CalibrationDatas.Add(record);
                    db.SaveChanges();
                    Logger.Debug("Calibrations saved");
                }
                catch (Exception ex)
                {
                    Logger.Info("Save DPS calibration result failed.");
                    Logger.Error("", ex);
                }
            }
        }

        public List<CalibrationData> GetCalibrationData()
        {
            return GetCalibrationData(DpsDetails.SerialNo);
        }
        public static List<CalibrationData> GetCalibrationData(string SerialNo)
        {
            List<CalibrationData> list = new List<CalibrationData>();
            using (var db = new RIT_QAEntities())
            {
                foreach (CalibrationData data in db.Calibrations.FirstOrDefault(x => x.SerialNo == SerialNo).CalibrationDatas.ToList())
                {
                    db.Entry(data).State = System.Data.Entity.EntityState.Detached;
                    list.Add(data);
                }
            }
            return list;
        }

        public Batch GetCalibrationBatch()
        {
            return GetCalibrationBatch(DpsDetails.SerialNo);
        }
        public static Batch GetCalibrationBatch(string SerialNo)
        {
            using (var db = new RIT_QAEntities())
            {
                Batch batch = db.Calibrations.FirstOrDefault(x => x.SerialNo == SerialNo).Batch;
                db.Entry(batch).State = System.Data.Entity.EntityState.Detached;
                return batch;
            }
        }

        public Calibration GetDpsInfo()
        {
            return GetDpsInfo(DpsDetails.SerialNo);
        }
        public static Calibration GetDpsInfo(string SerialNo)
        {
            using (var db = new RIT_QAEntities())
            {
                Calibration dps = db.Calibrations.FirstOrDefault(x => x.SerialNo == SerialNo);
                db.Entry(dps).State = System.Data.Entity.EntityState.Detached;
                return dps;
            }
        }


    }
}
