using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Identity;
using WebStore.Domain.ViewModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class Login : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<Login> _Logger;

        public Login(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger<Login> Logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Logger = Logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> IsNameFree(string UserName)
        {
            var user = _UserManager.FindByNameAsync(UserName);
            return Json(user is null ? "true" : "Пользователь с таким именем уже существует!");
        }

        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(Model);

            _Logger.LogInformation("Регистрация нового пользователя {0}!", Model.UserName);

            using (_Logger.BeginScope("Регистрация пользователя {0}", Model.UserName))
            {
                var user = new User
                {
                    UserName = Model.UserName,
                };

                var registration_result = await _UserManager.CreateAsync(user, Model.Password);
                if (registration_result.Succeeded)
                {
                    _Logger.LogInformation("Пользователь {0} зарегистрировался.", user.UserName);
                    await _UserManager.AddToRoleAsync(user, Role.User);

                    _Logger.LogInformation("Пользователю {0} назначена роль {1}.", user.UserName, Role.User);

                    await _SignInManager.SignInAsync(user, false);

                    _Logger.LogInformation("Пользователь {0} автоматически вошёл в систему после регистрации.", user.UserName);

                    return RedirectToAction("Index", "Home");
                }

                _Logger.LogWarning("Ошибка при регистрации пользователя {0}:{1}!", user.UserName, string.Join(",", registration_result.Errors.Select(e => e.Description)));


                foreach (var error in registration_result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(Model);
        }

        [AllowAnonymous]
        public IActionResult SignInUser(string ReturnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = ReturnUrl });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignInUser(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            _Logger.LogInformation("Вход пользователя {0} в систему", Model.UserName);

            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
#if DEBUG
                false
#else
                true
#endif
                );

            if (login_result.Succeeded)
            {
                _Logger.LogInformation("Вход пользователя {0} в систему был успешен.", Model.UserName);

                if (Url.IsLocalUrl(Model.ReturnUrl))
                    return Redirect(Model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            _Logger.LogWarning("Вы {0} ввели неверный пароль или имя пользователя.", Model.UserName);

            ModelState.AddModelError("", "Неверное имя пользователя, или пароль!");

            return View(Model);
        }

        public async Task<IActionResult> SignOutUser()
        {
            var user_name = User.Identity.Name;
            await _SignInManager.SignOutAsync();

            _Logger.LogInformation("Пользователь {0} вышел с портала.", user_name);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccesDenied()
        {
            _Logger.LogInformation("Отказано в доступе к {0}.", Request.Path);

            return View();
        }
    }
}
