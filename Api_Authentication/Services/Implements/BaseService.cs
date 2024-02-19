using Api_Authentication.DataContext;

namespace Api_Authentication.Services.Implements
{
    public class BaseService
    {
        public readonly AppDbContext _context;
        public BaseService()
        {
            _context = new AppDbContext();
        }

    }
}
