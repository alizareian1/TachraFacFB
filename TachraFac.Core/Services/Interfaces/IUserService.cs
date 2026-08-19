using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachraFac.Core.DTOs;
using TachraFac.Datalayer.Entities.User;
using TachraFac.Datalayer.Entities.Wallet;


namespace TachraFac.Core.Services.Interfaces
{
    public interface IUserService
    {
        #region UserService

        bool IsUserNameExist(string userName);
        bool IsEmailExist(string email);
        int AddUser(User user);
        User LoginUser(LoginViewModel login);
        bool ActiceAccount(string activeCode);
        User GetUserByUserName(string username);
        User GetUserByActiveCode(string activeCode);
        void UpdateUser(User user);

        void UpdateUserContact(UserContact userContact);
        int AddUserContact(UserContact userContact);
        UserContact GetUserIdByUserName(string userName);
        UserContact GetUserContactByUserId(int userId);
        int GetUserIdByUsername(string username);
        User GetUserById(int userId);
        void DeleteUser(int userId);

        #endregion

        #region UserPanel

        InformationUserViewModel GetUserInformation(string username);
        InformationUserViewModel GetUserInformation(int userId);
        //EditProfileViewModel GetDataForEditProfileUser(string username);
        bool CompareOldPassword(string  oldPassword, string username);   
        void ChangeUserPassword(string username, string newPassword);
        #endregion

        #region Wallet
        int BalanceUserWallet(string username);
        List<WalletViewModel> GetWalletUser(string username);
        int ChargeWallet(string username,int amount, string description, bool ispay=false);
        int AddWallet(Wallet wallet);
        Wallet GetWalletByWalletId(int walletId);
        void UpdateWallet(Wallet wallet);
        #endregion

        #region Admin Panel
        UsersForAdminViewModel GetUsers(int pageId=1,string filterEmail="",string filterUsername="");
        UsersForAdminViewModel GetDeleteUsers(int pageId=1,string filterEmail="",string filterUsername="");
        int AddUserFromAdmin(CreateUserViewModel createUser);
        EditUserViewModel GetUserForShowInEditMode(int userId);
        void EditUserFormAdmin(EditUserViewModel editUser);


        #endregion



        #region UserPanel

        //InformationUserViewModel GetUserInformation(string username);

        #endregion
    }
}
