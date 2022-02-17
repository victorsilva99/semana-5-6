using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class Caminhao
    {
        [Key]
        [Required(ErrorMessage = "Informe a ID do caminhão.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID não pode ser nulo (0) ou um número negativo.")]
        [Display(Name ="ID do caminhão")]
        public int IdCaminhao { get; set; }

        [Required(ErrorMessage = "Informe o nome do caminhão.")]
        [Display(Name ="Nome do caminhão")]
        [StringLength(50, ErrorMessage = "Limite máximo de caracteres foi excedido (50).")]
        public string Nome { get; set; }

        [Display(Name = "Descrição do caminhão")]
        [StringLength(100, ErrorMessage = "Limite máximo de caracteres foi excedido (100).")]
        public string Descricao { get; set; }

        [Display(Name = "Data da criação")]
        [Required(ErrorMessage = "Informe a data de criação.")]
        [DataType(DataType.Date)]
        public DateTime DataCriacao { get; set; }

    }
}
