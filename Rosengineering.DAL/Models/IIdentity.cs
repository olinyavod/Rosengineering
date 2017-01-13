namespace Rosengineering.DAL.Models
{
    public interface IIdentity<TKey>
    {
		TKey Id { get; set; }
    }
}
