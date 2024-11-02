using AdOptimize.Models.DTOs;
using System.Collections.Generic;

namespace AdOptimize.Models.DTOs
{
    public class CampanhaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int UsuarioId { get; set; }
        public List<AnuncioDTO> Anuncios { get; set; } = new List<AnuncioDTO>();
    }
}
