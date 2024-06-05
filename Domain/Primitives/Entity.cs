using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Primitives
{
    public abstract class Entity
    {
        protected Entity(int id) => Id = id;
        protected Entity()
        {

        }

        [Key]
        public int Id { get; protected set; }
    }
}
