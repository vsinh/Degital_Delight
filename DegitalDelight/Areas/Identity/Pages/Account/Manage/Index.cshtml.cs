// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DegitalDelight.Models;
using DegitalDelight.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DegitalDelight.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IPictureService _pictureService;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IPictureService pictureService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _pictureService = pictureService;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "New Password")]
            public string NewPassword { get; set; }

            [Display(Name = "Confirm Password")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Username")]
            public string Username { get; set; }


        }
        public int CreatedYear { get; set; }
        public string ImagePath { get; set; }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            CreatedYear = user.CreatedDate.Value.Year;
            ImagePath = user.ImagePath;

            Input = new InputModel
            {
                Username = userName,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile inputFile)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (inputFile != null)
            {
                user.ImagePath = _pictureService.CreateAccountPicture(inputFile);
            }

            CreatedYear = user.CreatedDate.Value.Year;
            ImagePath = user.ImagePath;

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return Page();
                }
            }

            if (!string.IsNullOrEmpty(Input.NewPassword) && !string.IsNullOrEmpty(Input.ConfirmPassword))
            {
                if (Input.NewPassword != Input.ConfirmPassword)
                {
                    StatusMessage = "New password and confirm password must be the same.";
                    return Page();
                }
                if (Input.NewPassword.Length <= 3)
                {
                    StatusMessage = "New password must longer than 3 characters.";
                    return Page();
                }
                else
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.Password, Input.NewPassword);
                    if (!changePasswordResult.Succeeded)
                    {

                        StatusMessage = "Mật khẩu cũ không chính xác.";
                        return Page();
                    }
                }
            }
            else
            {
                return Page();
            }


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
