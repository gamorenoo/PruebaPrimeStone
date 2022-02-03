using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class DireccionDTO
    {
        public int Id { get; set; }
        public string StringDireccion { get; set; }
        public TipoDireccion TipoDireccion { get; set; }
    }
}
