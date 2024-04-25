namespace Aira.Application.Interfaces;

public interface IApplicationDbContext
{
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	DatabaseFacade Database { get; }

	DbSet<TEntity> Set<TEntity>() where TEntity : class;

	EntityEntry Entry(object entity);

    #region DbSets

    public DbSet<Continent> Continent { get; set; }
    public DbSet<Country> Country { get; set; }

    #endregion DbSets
}