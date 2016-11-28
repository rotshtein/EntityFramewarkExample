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

    public partial class RIT_QAEntities
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                Logger.Error(exceptionMessage, ex);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }

    public static class utils
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string RecordToString(Object obj, string delimiter = ",")
        {
            string s = "";
            foreach (var prop in obj.GetType().GetProperties())
            {
                s += prop.Name + "=" + prop.GetValue(obj, null) + delimiter;
            }

            return s.Substring(0, s.Length - delimiter.Length);
        }

        public static int GetBatchId(string Batch)
        {
            using (var db = new RIT_QAEntities())
            {
                Batch ExistBatch = null;
                var b = Batch;
                try
                {
                    ExistBatch = db.Batches.FirstOrDefault(x => x.BatchNumber == b);
                }
                catch (Exception ex)
                {
                    Logger.Error("This should happend only when the Batch table is empty", ex);
                }

                int BatchId = 0;
                // and create if it is new
                if (ExistBatch == null)
                {
                    Batch _currentBatch = db.Batches.Create();
                    _currentBatch.BatchNumber = Batch;
                    _currentBatch.Date = DateTime.Now;
                    db.Batches.Add(_currentBatch);
                    db.SaveChanges();
                    BatchId = _currentBatch.Id;
                    Logger.Debug("New batch created. Batch Number = " + _currentBatch.BatchNumber);
                }
                else
                {
                    BatchId = ExistBatch.Id;
                }

                return BatchId;
            }
        }

    }
}

