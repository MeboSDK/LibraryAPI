using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.BookC.Commands;

public record UpdateBookCommand(
    int Id,
    [Required] string Title,
    string Descrption,
    double Rate,
    string ImagePath,
    int[] AuthorsIds,
    DateTime ReleaseDate,
    [Required] int TotalCount,
    int CurrentCount) : IRequest;

