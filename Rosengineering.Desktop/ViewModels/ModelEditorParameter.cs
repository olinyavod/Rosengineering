namespace Rosengineering.Desktop.ViewModels
{
	public class ModelEditorParameter<TKey>
	{
		public TKey Id { get; set; }

		public bool IsNew { get; set; }
	}

	public class ModelEditorParameter : ModelEditorParameter<int>
	{
		
	}
}