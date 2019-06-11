namespace Model_Oralce
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QPBAK.T_PERSON_NAME")]
    public partial class T_PERSON_NAME
    {
        [StringLength(50)]
        public string S_MAN_ID { get; set; }

        [StringLength(50)]
        public string S_MAN_NAME { get; set; }

        [StringLength(50)]
        public string S_MAN_NAME_ABBREVIATION { get; set; }

        [StringLength(50)]
        public string S_COM_FULLNAME { get; set; }

        [StringLength(50)]
        public string S_COM_NAME { get; set; }

        [StringLength(50)]
        public string S_TOWNID { get; set; }

        [StringLength(11)]
        public string S_PHONE { get; set; }

        public decimal? N_X { get; set; }

        public decimal? N_Y { get; set; }

        public short? N_STATUS { get; set; }

        public DateTime? D_UPDATE { get; set; }

        [Key]
        [StringLength(50)]
        public string S_ID { get; set; }

        [StringLength(200)]
        public string HDPIC { get; set; }

        [StringLength(50)]
        public string P_TOWNID { get; set; }
    }
}
