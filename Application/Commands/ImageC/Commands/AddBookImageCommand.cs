using MediatR;

namespace Application.Commands.ImageC.Commands;

public record AddBookImageCommand(string FileName, string ContentType, byte[] Image, int BookId): IRequest;
