using System.Linq;
using System.Threading.Tasks;
using Rosengineering.DAL.Models;

namespace Rosengineering.BusinessLogic
{
    public interface ICrudManager<TModel, TKey>
		where TModel : class, IIdentity<TKey>
    {
	    ModifyStatus<TModel> Add(TModel model);

		Task<ModifyStatus<TModel>> AddAsync(TModel model);

	    ModifyStatus<TModel> Update(TModel model);

		Task<ModifyStatus<TModel>> UpdateAsync(TModel model);

	    ExecuteStatus Delete(TKey id);

	    Task<ExecuteStatus> DeleteAsync(TKey id);

	    ExecuteStatus<TModel> Get(TKey id);

	    Task<ExecuteStatus<TModel>> GetAsync(TKey id);

		IQueryable<TModel> Query { get; }
    }

	public interface ICrudManager<TModel> : ICrudManager<TModel, int> 
		where TModel : class, IIdentity<int>
	{
		
	}
}
