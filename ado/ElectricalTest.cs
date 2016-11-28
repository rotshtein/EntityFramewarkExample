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
    
    public partial class ElectricalTest
    {
        public string SerialNo { get; set; }
        public string MAC { get; set; }
        public bool Result { get; set; }
        public string SwVersion { get; set; }
        public string HwVersion { get; set; }
        public Nullable<int> BatchId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> IdleCurrent { get; set; }
        public Nullable<int> ConnectedCurrent { get; set; }
        public Nullable<int> PS1_TargetOutput { get; set; }
        public Nullable<int> PS1_Output { get; set; }
        public Nullable<int> PS2_TargetOutput { get; set; }
        public Nullable<int> PS2_Output { get; set; }
        public Nullable<int> PS_Left_A2D_Idle { get; set; }
        public Nullable<int> PS_Left_A2D_UnderPressure { get; set; }
        public Nullable<int> PS_Right_A2D_Idle { get; set; }
        public Nullable<int> PS_Right_A2D_UnderPressure { get; set; }
        public Nullable<int> Current_6V { get; set; }
        public Nullable<int> Current_24V { get; set; }
        public Nullable<int> Output_1_6V { get; set; }
        public Nullable<int> Output_2_6V { get; set; }
        public string Output_1_24V { get; set; }
        public string Output_2_24V { get; set; }
        public Nullable<decimal> JigTemp { get; set; }
        public Nullable<decimal> DpTemp { get; set; }
    
        public virtual Batch Batch { get; set; }
    }
}
