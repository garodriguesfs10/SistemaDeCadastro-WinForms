using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCadastro.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string EstadoCivil { get; set; }
        public string Telefone { get; set; }
        public bool CasaPropria { get; set; }
        public bool Veiculo { get; set; }

        public char Sexo { get; set; }
    }
}
