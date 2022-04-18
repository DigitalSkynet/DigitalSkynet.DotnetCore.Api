using DigitalSkynet.DotnetCore.DataStructures.Generics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalSkynet.DotnetCore.DataAccess.StaticData;

	public static class StaticDataProviderExtensions
	{
		public static DataBuilder<TEntity> HasDataProvider<TProvider, TEntity>(
			this EntityTypeBuilder<TEntity> builder)
			where TProvider : StaticDataProvider<TEntity>
			where TEntity : class
		{
			return builder.HasData(LazySingleton<TProvider>.Instance.Entities);
		}
	}
