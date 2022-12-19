using System.ComponentModel.DataAnnotations.Schema;

namespace handson.Models
{
    [Table("contas")]
    public class Contas
    {
        [Column("codigo")]
        public string id { get; set; }

        [Column("nome")]
        public String nome { get; set; }

        [Column("tipo")]
        public String tipo { get; set; }

        [Column("lancamento")]
        public Boolean lancamento { get; set; }
    }
}