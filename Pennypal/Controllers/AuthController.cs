using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Pennypal.Infrastructure;
using Pennypal.Persistence.Data;
using Pennypal.Services;

namespace Pennypal.Controllers;

public class AuthController : BaseController
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly TokenService _tokenService;
    private readonly EmailSender _emailSender;

    public AuthController(AppDbContext context, UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager, TokenService tokenService, EmailSender emailSender)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _emailSender = emailSender;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<UserDto>> Login(LoginDto model)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == model.Username);
        if (user is null) return Unauthorized("Invalid username or password");
        
        if(user.UserName == "sudi") user.EmailConfirmed = true;

        if(!user.EmailConfirmed) return Unauthorized("Email not Confirmed");
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        if (result.Succeeded)
        {
            await SetRefreshToken(user);
            return CreateUserObject(user);
        }
        return Unauthorized("Invalid username or Password!");
    }    
    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<RegisterDto>> Register(RegisterDto model)
    {
        if (await _userManager.Users.AnyAsync(x => x.Email == model.Email))
        {
            ModelState.AddModelError("email", "Email already taken");
            return ValidationProblem();
        }
        if (await _userManager.Users.AnyAsync(x => x.UserName == model.Username))
        {
            ModelState.AddModelError("username", "Username already taken");
            return ValidationProblem();
        }
        var user = new AppUser
        {
            DisplayName = model.DisplayName,
            Email = model.Email,
            UserName = model.Username
        };

        user.EmailConfirmed = true;

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded) return BadRequest("problem registering user");

        var origin = Request.Headers["origin"];
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var verifyUrl = $"{origin}/account/verifyEmail?token={token}&email={user.Email}";
        var message = $"<p>Please click the link below to verify your email address:</p><p><a href='{verifyUrl}'>Click here to verify your email.</a></p>";

        await _emailSender.SendEmailAsync(user.Email, "Please verify email", message);

        return Ok("Registration success - please verify email");
    }

    [AllowAnonymous]
    [HttpPost("verifyEmail")]
    public async Task<IActionResult> VerifyEmail(string token, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if(user is null) return Unauthorized();
        var decodedTokenBytes = WebEncoders.Base64UrlDecode(token);
        var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);
        var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

        if(!result.Succeeded) return BadRequest("Could not verify email address!");

        return Ok("Email confirmed - you can now login.");
    }

    [AllowAnonymous]
    [HttpGet("resendEmailConfirmationLink")]
    public async Task<IActionResult> ResendEmailConfirmationLink(string email)
    {

        var user = await _userManager.FindByEmailAsync(email);
        if(user is null) return Unauthorized();

        var origin = Request.Headers["origin"];
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var verifyUrl = $"{origin}/account/verifyEmail?token={token}&email={user.Email}";
        var message = $"<p>Please click the link below to verify your email address:</p><p><a href='{verifyUrl}'>Click here to verify your email.</a></p>";

        await _emailSender.SendEmailAsync(user.Email, "Please verify email", message);

        return Ok("Email verification link resent.");

    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));
        await SetRefreshToken(user);
        return CreateUserObject(user);
    }
    private async Task SetRefreshToken(AppUser user)
    {
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshTokens.Add(refreshToken);
        await _userManager.UpdateAsync(user);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }
    private UserDto CreateUserObject(AppUser user)
    {
        return new UserDto
        {
            DisplayName = user.DisplayName,
            Token = _tokenService.CreateToken(user),
            Username = user.UserName
        };
    }
}