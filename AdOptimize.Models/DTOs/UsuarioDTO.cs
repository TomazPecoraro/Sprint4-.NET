using System.Collections.Generic;
using AdOptimize.Models.DTOs;

namespace AdOptimize.Models.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Telefone { get; set; }
        public List<CampanhaDTO> Campanhas { get; set; } = new List<CampanhaDTO>();
    }
}
