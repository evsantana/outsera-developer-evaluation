using System.ComponentModel.DataAnnotations;

namespace OutseraMovies.Application.DTOs
{
    public sealed record MovieDTO
        (
            int Id,

            [Required(ErrorMessage = "Year is required")]
            int Year,

            [Required(ErrorMessage = "Title is required")]
            string Title,

            [Required(ErrorMessage = "Studios is required")]
            string Studios,

            [Required(ErrorMessage = "Producer is required")]
            string Producers,
            //ICollection<ProducerDTO> Producers,

            [Required(ErrorMessage = "Winner is required")]
            bool Winner
        );
}
