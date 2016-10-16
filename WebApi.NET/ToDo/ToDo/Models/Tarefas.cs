using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class Tarefas
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} é obrigatório!")]
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public bool Realizada { get; set; }

        [Required(ErrorMessage = "{0} é obrigatória!")]
        public DateTime DataLimite { get; set; }

        public string UserName { get; set; }
    }
}
