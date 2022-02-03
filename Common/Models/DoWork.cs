using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class DoWork
    {
        [Key]
        public Guid Id { get; set; }
        public bool EstadoBorrado { get; set; }
        public string Evento { get; set; }
        public DateTime Fecha { get; set; }
    }
}
