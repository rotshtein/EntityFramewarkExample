//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ado
{
    using System;
    using System.Collections.Generic;
    
    public partial class CalibrationData
    {
        public string SerialNo { get; set; }
        public double SetPoint { get; set; }
        public double Pressure { get; set; }
        public Nullable<int> RightA2D { get; set; }
        public Nullable<int> LeftA2D { get; set; }
        public Nullable<double> Temp { get; set; }
        public int Id { get; set; }
    
        public virtual Calibration Calibration { get; set; }
    }
}
