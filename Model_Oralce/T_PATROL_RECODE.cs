namespace Model_Oralce
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QPBAK.T_PATROL_RECODE")]
    public partial class T_PATROL_RECODE
    {
        [Key]
        [StringLength(50)]
        public string S_RECODE_ID { get; set; }

        [StringLength(32)]
        public string S_TASK_ID { get; set; }

        public DateTime? T_START { get; set; }

        public DateTime? T_END { get; set; }

        public short? N_CYCLE { get; set; }

        public long? N_TIME { get; set; }

        public decimal? N_MILEAGE { get; set; }

        [StringLength(50)]
        public string S_MAN_ID { get; set; }

        [StringLength(50)]
        public string S_MAN_CN { get; set; }

        [StringLength(50)]
        public string S_TOWNID { get; set; }

        [StringLength(50)]
        public string S_COMPANY { get; set; }

        [StringLength(50)]
        public string S_TOWNNAME { get; set; }

        public byte? N_DEL { get; set; }
    }
}
