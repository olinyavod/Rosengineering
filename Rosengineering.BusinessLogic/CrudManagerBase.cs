﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Rosengineering.DAL;
using Rosengineering.DAL.Models;

namespace Rosengineering.BusinessLogic
{
	public abstract class CrudManagerBase<TModel, TKey> : ICrudManager<TModel, TKey> where TModel : class, IIdentity<TKey>
	{
		private readonly IValidator<TModel> _validator;
		private readonly RosengineeringDbContext _context;

		protected CrudManagerBase(IValidator<TModel> validator, RosengineeringDbContext context)
		{
			_validator = validator;
			_context = context;
		}

		protected virtual IQueryable<TModel> GetQuery()
		{
			return _context.Set<TModel>();
		}

		public virtual ModifyStatus<TModel> Add(TModel model)
		{
			try
			{
				var validationResult = _validator.Validate(model).ToValidStatus<TModel>();
				if (!validationResult.IsValid)
					return validationResult;
				_context.Set<TModel>().Add(model);
				_context.SaveChanges();
				return new ModifyStatus<TModel>
				{
					Result = model
				};
			}
			catch (Exception ex)
			{
				return ex.ToStatus<ModifyStatus<TModel>>();
			}
		}

		public virtual async Task<ModifyStatus<TModel>> AddAsync(TModel model)
		{
			try
			{
				var validationResult = (await _validator.ValidateAsync(model)).ToValidStatus<TModel>();
				if (!validationResult.IsValid)
					return validationResult;
				_context.Set<TModel>().Add(model);
				await _context.SaveChangesAsync();
				return new ModifyStatus<TModel>
				{
					Result = model
				};
			}
			catch (Exception ex)
			{
				return ex.ToStatus<ModifyStatus<TModel>>();
			}
		}

		public virtual ModifyStatus<TModel> Update(TModel model)
		{
			try
			{
				var validationResult = _validator.Validate(model).ToValidStatus<TModel>();
				if (!validationResult.IsValid)
					return validationResult;
				_context.SaveChanges();
				return new ModifyStatus<TModel>
				{
					Result = model
				};
			}
			catch (Exception e)
			{
				return e.ToStatus<ModifyStatus<TModel>>();
			}
		}

		public virtual async Task<ModifyStatus<TModel>> UpdateAsync(TModel model)
		{
			try
			{
				var validationResult = (await _validator.ValidateAsync(model)).ToValidStatus<TModel>();
				if (!validationResult.IsValid)
					return validationResult;
				await _context.SaveChangesAsync();
				return new ModifyStatus<TModel>
				{
					Result = model
				};
			}
			catch (Exception ex)
			{
				return ex.ToStatus<ModifyStatus<TModel>>();
			}
		}

		public virtual ExecuteStatus Delete(TKey id)
		{
			try
			{
				var item = _context.Set<TModel>().Find(id);
				_context.Set<TModel>().Remove(item);
				_context.SaveChanges();
				return new ExecuteStatus();
			}
			catch (Exception e)
			{
				return e.ToStatus<ExecuteStatus>();
			}
		}

		public virtual async Task<ExecuteStatus> DeleteAsync(int id)
		{
			try
			{
				var item = await _context.Set<TModel>().FindAsync(id);
				_context.Set<TModel>().Remove(item);
				await _context.SaveChangesAsync();
				return new ExecuteStatus();
			}
			catch (Exception e)
			{
				return e.ToStatus<ExecuteStatus>();
			}
		}

		public IQueryable<TModel> Query => GetQuery();
	}

	public abstract class CrudManagerBase<TModel> : CrudManagerBase<TModel, int> where TModel : class, IIdentity<int>
	{
		protected CrudManagerBase(IValidator<TModel> validator, RosengineeringDbContext context) 
			: base(validator, context)
		{
		}
	}
}