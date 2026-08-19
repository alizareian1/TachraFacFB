
﻿using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachraFac.Core.DTOs;
using TachraFac.Core.Genrator;
using TachraFac.Core.Security;
using TachraFac.Core.Services.Interfaces;
using TachraFac.Datalayer.Context;
using TachraFac.Datalayer.Entities.User;

using TachraFac.Datalayer.Entities.Wallet;


namespace TachraFac.Core.Services
{
    public class UserService : IUserService
    {
        private TachraContext _context;
        public UserService(TachraContext context)
        {
            _context = context;
        }

        public bool ActiceAccount(string activeCode)
        {
            var user = _context.tblUser.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user == null || user.IsActive)
            {
                return false;
            }
            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUnicCode();
            _context.SaveChanges();
            return true;
        }

        public int AddUser(User user)
        {
            _context.tblUser.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }


        public int AddUserContact(UserContact userContact)
        {
            _context.tblUserContact.Add(userContact);
            _context.SaveChanges();
            return userContact.UserId;
        }

        public int AddUserFromAdmin(CreateUserViewModel createUser)
        {
            User user = new User();
           
            user.Password = PasswordHelper.EncodePasswordMd5(createUser.Password);
            user.ActiveCode = NameGenerator.GenerateUnicCode();
            user.Email = createUser.Email;
            user.IsActive = true;
            user.RegisterDate = DateTime.Now;
            user.UserName = createUser.UserName;
            user.Name = createUser.Name;
            UserContact userContact = new UserContact
            {
                UserId = user.UserId,
                User = user,
                Address = createUser.Address,
                PhoneNumber = createUser.Mobile,
                PostalCode = createUser.PostalCode,
            };

            #region Save Avatar
            if (createUser.userAvatar != null)
            {      
                string imagePath = "";
                user.UserAvatar = NameGenerator.GenerateUnicCode() + Path.GetExtension(createUser.userAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/AvatarUser", user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    createUser.userAvatar.CopyTo(stream);
                }                
            }          
            #endregion

            int userId = AddUser(user);
            AddUserContact(userContact);
            return userId;


        }

        public int AddWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            _context.SaveChanges();
            return wallet.WalletId;
        }

        public int BalanceUserWallet(string username)
        {
            int userId = GetUserIdByUsername(username);
            var Deposit = _context.Wallets.Where(w => w.UserId == userId && w.TypeId == 1 && w.IsPay).Select(w => w.Amount).ToList();

            var Withdrawal = _context.Wallets.Where(w => w.UserId == userId && w.TypeId == 2).Select(w => w.Amount).ToList();

            return (Deposit.Sum() - Withdrawal.Sum());
        }

        public void ChangeUserPassword(string username, string newPassword)
        {
            var user = GetUserByUserName(username);
            user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
            UpdateUser(user);
        }

        public int ChargeWallet(string username, int amount, string description, bool ispay = true)
        {
            Wallet wallet = new Wallet()
            {
                Amount = amount,
                CreateDate = DateTime.Now,
                Discription = description,
                IsPay = ispay,
                TypeId = 1,
                UserId = GetUserIdByUsername(username)
            };
            return AddWallet(wallet);
        }

        public bool CompareOldPassword(string oldPassword, string username)
        {
            string hashpassword = PasswordHelper.EncodePasswordMd5(oldPassword);
            return _context.tblUser.Any(u=> u.Password == hashpassword && u.UserName ==username);
        }

        public void DeleteUser(int userId)
        {
            User user = GetUserById(userId);
            user.IsDelete = true;
            UpdateUser(user);
        }

        public void EditUserFormAdmin(EditUserViewModel editUser)
        {
            User user = GetUserById(editUser.userId);
            user.Email = editUser.Email;
            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);
            }

            if (editUser.userAvatar != null)
            {
                if(editUser.AvatarName != "Defult.jpg")
                {
                    // Delete Old Image
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/AvatarUser", editUser.AvatarName);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }
                
                // Save New Image
                user.UserAvatar = NameGenerator.GenerateUnicCode() + Path.GetExtension(editUser.userAvatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/AvatarUser", user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    editUser.userAvatar.CopyTo(stream);
                }
            }
            _context.tblUser.Update(user);
            _context.SaveChanges();
        }

        public UsersForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUsername = "")
        {
            IQueryable<User> result = _context.tblUser.IgnoreQueryFilters().Where(u=>u.IsDelete);
            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUsername))
            {
                result = result.Where(u => u.UserName.Contains(filterUsername));
            }

            //Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;
            UsersForAdminViewModel list = new UsersForAdminViewModel();
            list.CurentPage = pageId;
            list.PageCount = result.Count() / take;
            list.users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();
            return list;
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.tblUser.SingleOrDefault(u => u.ActiveCode == activeCode);
        }


        public User GetUserById(int userId)
        {
            return _context.tblUser.Find(userId);
        }

        public User GetUserByUserName(string username)
        {
            return _context.tblUser.SingleOrDefault(u => u.UserName == username);
        }


        public UserContact GetUserContactByUserId(int userId)
        {
            return _context.tblUserContact.SingleOrDefault(u => u.UserId == userId);
        }

        public EditUserViewModel GetUserForShowInEditMode(int userId)
        {
            return _context.tblUser.Where(u => u.UserId == userId)
                .Select(u => new EditUserViewModel
                {
                    userId = u.UserId,
                    AvatarName = u.UserAvatar,
                    UserName = u.UserName,
                    Name = u.Name,
                    Email = u.Email,
                    UserRoles = u.userRoles.Select(r=>r.RoleId).ToList(),
                    Address = u.UserContact.Address,
                    Mobile = u.UserContact.PhoneNumber,
                    PostalCode = u.UserContact.PostalCode,
                }).Single();
           
        }

        public UserContact GetUserIdByUserName(string userName)
        {
            var user = GetUserByUserName(userName);
            var userId = user.UserId;
            return _context.tblUserContact.SingleOrDefault(u => u.UserId == userId);

        }

        public int GetUserIdByUsername(string username)
        {
            return _context.tblUser.Single(u => u.UserName == username).UserId;
        }

        public InformationUserViewModel GetUserInformation(string username)
        {
            var user = GetUserByUserName(username);
            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.Name = user.Name;
            information.Email = user.Email;
            information.RegisterDate = user.RegisterDate;
            information.Wallet = BalanceUserWallet(username);

            var userContent = GetUserIdByUserName(username);
            information.Address = userContent.Address;
            information.PhoneNumber = userContent.PhoneNumber;
            information.PostalCode = userContent.PostalCode;
            information.UserAvatar = "UserAvatarDefault.png";
            return information;
        }

        public InformationUserViewModel GetUserInformation(int userId)
        {
            var user = GetUserById(userId);
            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.Name = user.Name;
            information.Email = user.Email;
            information.RegisterDate = user.RegisterDate;
            information.Wallet = BalanceUserWallet(user.UserName);

            //var userContent = GetUserIdByUserName(user.UserName);
            //information.Address = userContent.Address;
            //information.PhoneNumber = userContent.PhoneNumber;
            //information.PostalCode = userContent.PostalCode;
            //information.UserAvatar = "UserAvatarDefault.png";
            return information;
        }

        public UsersForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUsername = "")
        {
            
            IQueryable<User> result = _context.tblUser;
            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUsername))
            {
                result = result.Where(u => u.UserName.Contains(filterUsername));
            }

            //Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;
            UsersForAdminViewModel list = new UsersForAdminViewModel();
            list.CurentPage = pageId;
            list.PageCount = result.Count() / take;
            list.users=result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList(); 
            return list;
        } 

        public Wallet GetWalletByWalletId(int walletId)
        {
            return _context.Wallets.Find(walletId);
        }

        public List<WalletViewModel> GetWalletUser(string username)
        {
            int userId = GetUserIdByUsername(username);
            return _context.Wallets.Where(w => w.IsPay && w.UserId == userId)
                .Select(w => new WalletViewModel
                {
                    WalletId = w.WalletId,
                    Amount = w.Amount,
                    Type = w.TypeId,
                    Discription = w.Discription,
                    DateTime = w.CreateDate,
                    IsPay = true,
                })
                .ToList();
        


            //information.Email = user.Email;
            //information.RegiisterDate = user.RegisterDate;
            //information.Wallet = 0;

            //// Add another parameters
            //return information;
        }

        public bool IsEmailExist(string email)
        {
            return _context.tblUser.Any(u => u.Email == email);
        }

        public bool IsUserNameExist(string userName)
        {
            return _context.tblUser.Any(u => u.UserName == userName);
        }

        public User LoginUser(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string username = login.UserName;
            return _context.tblUser.SingleOrDefault(u => u.UserName == username && u.Password == hashPassword);

        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }


        public void UpdateUserContact(UserContact userContact)
        {
            _context.Update(userContact);
            _context.SaveChanges();
        }

        public void UpdateWallet(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            _context.SaveChanges();
        }

    }
}
