using Application.Commands.BookC.Commands.Records;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.BookC.Commands;

public record AddBookCommand(
    [Required] string Title,
    string Descrption,
    double Rate,
    DateTime ReleaseDate,
    List<int> AuthorsIds,
    [Required] int TotalCount,
    int CurrentCount) : IRequest;


