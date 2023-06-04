using Microsoft.EntityFrameworkCore;
using Puzzles.Data.Base;
using Puzzles.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzles.Data.Services
{
    public class IngredientsService : EntityBaseRepository<Ingredient>, IIngredientsService
    {
        public IngredientsService(AppDbContext context) :base(context)
        {

        }
      
     
    }
}
