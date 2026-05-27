using Microsoft.EntityFrameworkCore;

namespace LogiSphere.Infrastructure.Data.Database.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
}
