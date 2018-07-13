using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WCF
{
    [DataContract]
    public class DTOEvaluacion
    {
        [DataMember]
        public string Nombre { get; set;}
        [DataMember]
        public string Justificacion { get; set; }
        [DataMember]
        public int PuntajeTotal { get; set; }
    }
}