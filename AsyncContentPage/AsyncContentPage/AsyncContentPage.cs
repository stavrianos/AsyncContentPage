using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsyncContentPage
{
    public class AsyncContentPage : ContentPage
    {
        public bool? Result { get; set; } = null;

        public AsyncContentPage()
        {
            ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).Popped += AsyncContentPage_Popped; 
        }

        private void AsyncContentPage_Popped(object sender, NavigationEventArgs e)
        {
            if (!Result.HasValue) this.Result = false;
        }
    }

    public static class AsyncContentTask
    {
        public static async Task<bool> PushAsyncWithReply(this INavigation navigation,AsyncContentPage page)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
            if (navigation.NavigationStack.Count > 0)
                await navigation.PushAsync(page);
            else if (navigation.ModalStack.Count > 0)
                await navigation.PushModalAsync(page);
            else
                throw new NullReferenceException
                {
                    Source = "It's recommended that a NavigationPage should be populated with ContentPage instances only.",
                     HelpLink = "https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/navigation/hierarchical",                    
                };
            
            Xamarin.Forms.Device.StartTimer(new TimeSpan(100), () => 
            {
                if (page.Result.HasValue)
                {
                    taskCompletionSource.SetResult(page.Result.Value);
                    return false;
                }
                return true;
            });
            return await taskCompletionSource.Task;
        }
          
    }
}
