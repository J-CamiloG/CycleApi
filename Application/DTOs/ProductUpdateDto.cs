using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre del producto es obligatorio.")]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        public decimal Price { get; set; }

        [Required(ErrorMessage = "La Categoría es obligatoria.")]
        public string? Category { get; set; }

        public string? ImageBase64 { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
