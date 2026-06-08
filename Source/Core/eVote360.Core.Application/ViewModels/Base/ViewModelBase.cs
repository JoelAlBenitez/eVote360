namespace eVote360.Core.Application.ViewModels.Base
{
    public abstract class ViewModelBase<Tkey>
    {
        public required Tkey Id { get; set; }
        public required string Name {  get; set; }
        public required bool State {  get; set; }

    }
}
