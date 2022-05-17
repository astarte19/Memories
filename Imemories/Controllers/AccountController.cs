using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Imemories.ViewModels;
using Imemories.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;
using System.Security.Claims;
namespace Imemories.Controllers
{
    public class AccountController : Controller
	{
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    
                    await _signInManager.SignInAsync(user, false);
                    //Создаем таблицу юзеру после успешной регистрации и входа
                    var user_data = user.Email;
                    string temp_email = user_data.Replace("@", string.Empty);
                    string email = temp_email.Replace(".", string.Empty);
                    SQLiteConnection USER = new SQLiteConnection("Data Source=UserData.db;");
                    USER.Open();
                    SQLiteCommand cart_table = USER.CreateCommand();
                    cart_table.CommandText =
                        $"CREATE TABLE IF NOT EXISTS {email} ( Id TEXT, ImagePath TEXT ,Text TEXT, AudioPath TEXT)";
                    cart_table.ExecuteNonQuery();
                    USER.Close();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

            [HttpGet]
            public IActionResult Login(string returnUrl = null)
            {
                return PartialView(new LoginViewModel { ReturnUrl = returnUrl });
            }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong data");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}