using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zona.Mongolizar
{
    public class Entidade
    {
        public object Id { get; set; }
        public string Texto { get; set; }

        public string NomeArquivo { get; set; }

        public byte[] Arquivo { get; set; }
    }
}
