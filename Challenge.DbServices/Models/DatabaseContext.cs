using Challenge.DbServices.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Challenge.DbServices.Models
{
    public class DatabaseContext : DbContext
    {
        #region Constructor

        // Constructor for the DatabaseContext class.
        // This constructor takes DbContextOptions as a parameter and passes it to the base class constructor.
        // It is used to configure and initialize the database context with the provided options.
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        #endregion

        #region DbSets

        // DbSet property for the "PostCodes" entity.
        // This property allows interaction with the "PostCodes" table in the database.
        public virtual DbSet<PostCodes> PostCodes { get; set; }

        #endregion

    }
}
