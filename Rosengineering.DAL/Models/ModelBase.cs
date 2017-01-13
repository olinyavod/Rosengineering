namespace Rosengineering.DAL.Models
{
	public abstract class ModelBase<TKey> : IIdentity<TKey>
	{
		public TKey Id { get; set; }
	}

	public abstract class ModelBase : ModelBase<int>
	{
		
	}
}