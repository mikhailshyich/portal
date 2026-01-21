using CommunityToolkit.Mvvm.Input;
using Portal.MAUI.Models;

namespace Portal.MAUI.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}