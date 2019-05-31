using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Quisco.Core.Helpers;
using Quisco.Core.Models;
using Quisco.Core.Services;
using Quisco.Helpers;
using Quisco.ViewModels;

using Windows.Storage;

namespace Quisco.Services
{
    public class UserDataService
    {
        private const string _userSettingsKey = "IdentityUser";

        private UserViewModel _user;

        private IdentityService IdentityService => Singleton<IdentityService>.Instance;

        private MicrosoftGraphService MicrosoftGraphService => Singleton<MicrosoftGraphService>.Instance;

        public event EventHandler<UserViewModel> UserDataUpdated;

        public UserDataService()
        {
        }

        public void Initialize()
        {
            IdentityService.LoggedIn += OnLoggedIn;
            IdentityService.LoggedOut += OnLoggedOut;
        }

        public async Task<UserViewModel> GetUserAsync()
        {
            if (_user == null)
            {
                _user = await GetUserFromCacheAsync().ConfigureAwait(true);
                if (_user == null)
                {
                    _user = GetDefaultUserData();
                }
            }

            return _user;
        }

        private async void OnLoggedIn(object sender, EventArgs e)
        {
            _user = await GetUserFromGraphApiAsync().ConfigureAwait(true);
            UserDataUpdated?.Invoke(this, _user);
        }

        private async void OnLoggedOut(object sender, EventArgs e)
        {
            _user = null;
            await ApplicationData.Current.LocalFolder.SaveAsync<User>(_userSettingsKey, null).ConfigureAwait(true);
        }

        private async Task<UserViewModel> GetUserFromCacheAsync()
        {
            var cacheData = await ApplicationData.Current.LocalFolder.ReadAsync<User>(_userSettingsKey).ConfigureAwait(true);
            return await GetUserViewModelFromData(cacheData).ConfigureAwait(true);
        }

        private async Task<UserViewModel> GetUserFromGraphApiAsync()
        {
            var accessToken = await IdentityService.GetAccessTokenAsync().ConfigureAwait(true);
            if (string.IsNullOrEmpty(accessToken))
            {
                return null;
            }

            var userData = await MicrosoftGraphService.GetUserInfoAsync(accessToken).ConfigureAwait(true);
            if (userData != null)
            {
                userData.Photo = await MicrosoftGraphService.GetUserPhoto(accessToken).ConfigureAwait(true);
                await ApplicationData.Current.LocalFolder.SaveAsync(_userSettingsKey, userData).ConfigureAwait(true);
            }

            return await GetUserViewModelFromData(userData).ConfigureAwait(true);
        }

        private async Task<UserViewModel> GetUserViewModelFromData(User userData)
        {
            if (userData == null)
            {
                return null;
            }

            var userPhoto = string.IsNullOrEmpty(userData.Photo)
                ? ImageHelper.ImageFromAssetsFile("DefaultIcon.png")
                : await ImageHelper.ImageFromStringAsync(userData.Photo).ConfigureAwait(true);

            return new UserViewModel()
            {
                Name = userData.DisplayName,
                UserPrincipalName = userData.UserPrincipalName,
                Photo = userPhoto
            };
        }

        private UserViewModel GetDefaultUserData()
        {
            return new UserViewModel()
            {
                Name = IdentityService.GetAccountUserName(),
                Photo = ImageHelper.ImageFromAssetsFile("DefaultIcon.png")
            };
        }
        private UserViewModel GetDefaultUserData1()
        {
            return new UserViewModel()
            {
                
                Name = IdentityService.GetAccountUserName(),
                Photo = ImageHelper.ImageFromAssetsFile("DefaultIcon.png")
            };
        }
    }
}
