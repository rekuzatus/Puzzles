using Puzzles.Data.Base;
using Puzzles.Models;

namespace Puzzles.Data.Services
{
    public class GlassesService:EntityBaseRepository<Glass>, IGlassesService
    {
        public GlassesService(AppDbContext context) : base(context)
        {

        }
    }
}
