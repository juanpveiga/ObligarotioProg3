using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WCF
{
    [DataContract]
    public class DTOEmprendimiento
    {
        [DataMember]
        public int CodId { get; set; }
        [DataMember]
        public string Titulo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public double Costo { get; set; }
        [DataMember]
        public int TiempoEjecucion { get; set; }
       
        [DataMember]
        public int Puntaje { get; set; }
    }
}