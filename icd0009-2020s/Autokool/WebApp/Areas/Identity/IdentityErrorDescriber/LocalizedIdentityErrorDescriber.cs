using System;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Areas.Identity.IdentityErrorDescriber
{
    /// <summary>
    /// Localized Identity Error Describer
    /// </summary>
    public class LocalizedIdentityErrorDescriber : Microsoft.AspNetCore.Identity.IdentityErrorDescriber
{
    /// <summary>
    /// DefaultError
    /// </summary>
    /// <returns></returns>
    public override IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.DefaultError }; }
    /// <summary>
    /// Concurrency failure
    /// </summary>
    /// <returns></returns>
    public override IdentityError ConcurrencyFailure() { return new IdentityError { Code = nameof(ConcurrencyFailure), Description = Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.ConcurrencyFailure }; }
    /// <summary>
    /// Password doesnt match
    /// </summary>
    /// <returns></returns>
    public override IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.PasswordMismatch }; }
    /// <summary>
    /// Invalid token
    /// </summary>
    /// <returns></returns>
    public override IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description = Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.InvalidToken }; }
    /// <summary>
    /// Login has already been associated
    /// </summary>
    /// <returns></returns>
    public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.LoginAlreadyAssociated }; }
    /// <summary>
    /// Username is invalid
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = nameof(InvalidUserName), Description = string.Format(Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.InvalidUserName, userName) }; }
    /// <summary>
    /// Email is invalid
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = nameof(InvalidEmail), Description = string.Format(Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.InvalidEmail, email)  }; }
    /// <summary>
    /// Username has been taken
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = string.Format(Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.DuplicateUserName, userName)  }; }
    /// <summary>
    /// Email has been taken
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = nameof(DuplicateEmail), Description = string.Format(Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.DuplicateEmail, email)  }; }
    /// <summary>
    /// Role does not exist
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = nameof(InvalidRoleName), Description = string.Format(Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.InvalidRoleName, role)  }; }
    /// <summary>
    /// Role has already been made
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = nameof(DuplicateRoleName), Description = string.Format(Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.DuplicateRoleName, role)  }; }
    /// <summary>
    /// Passowrd has already been set
    /// </summary>
    /// <returns></returns>
    public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.UserAlreadyHasPassword }; }
    /// <summary>
    /// User lockout is not turned on
    /// </summary>
    /// <returns></returns>
    public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.UserLockoutNotEnabled }; }
    /// <summary>
    /// User has already been assigned to the given role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = string.Format(Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.UserAlreadyInRole, role)  }; }
    /// <summary>
    /// User is no in the required role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = nameof(UserNotInRole), Description = string.Format(Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.UserNotInRole, role)  }; }
    /// <summary>
    /// Password is too short
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = string.Format(Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.PasswordTooShort, length)  }; }
    /// <summary>
    /// Password requires non alphanumeric character
    /// </summary>
    /// <returns></returns>
    public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.PasswordRequiresNonAlphanumeric }; }
    /// <summary>
    /// Password requires digit character
    /// </summary>
    /// <returns></returns>
    public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.PasswordRequiresDigit }; }
    /// <summary>
    /// Password required lower case character
    /// </summary>
    /// <returns></returns>
    public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.PasswordRequiresLower }; }
    /// <summary>
    /// Password required uppser case character
    /// </summary>
    /// <returns></returns>
    public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.PasswordRequiresUpper }; }
}
}