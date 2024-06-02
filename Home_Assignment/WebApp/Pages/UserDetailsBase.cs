using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages
{
	public class UserDetailsBase :ComponentBase
	{
		[Inject]
		public IAuthService AuthService { get; set; }
		[Inject]
		public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
		[Inject]
		public NavigationManager NavigationManager { get; set; }

		public RegisterModel User { get; set; }
		protected override async Task OnInitializedAsync()
		{
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var userId = user.FindFirst("userId")?.Value;
            User = await AuthService.Get(userId);
		}
	}
}
