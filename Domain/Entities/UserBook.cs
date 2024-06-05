using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserBook : Entity
    {
        public string UserId { get; set; }
        public int BookId { get; set; }
        public int BooksCount { get; set; }
        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
}
