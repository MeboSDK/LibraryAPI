using Domain.Primitives;

namespace Domain.Entities
{
    public class BookImage : Entity
    {
        public int BookId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Image { get; set; }
    }
}
