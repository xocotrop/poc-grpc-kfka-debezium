using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PockGRPC.Models
{
    public class Modelo
    {
        public string Endereco { get; internal set; }
        public int Id { get; internal set; }
        public int? IntNull { get; internal set; }
        public string Nome { get; internal set; }
        public string Telefone { get; internal set; }
        public List<string> ListaString { get; internal set; }
    }
}
