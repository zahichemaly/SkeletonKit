using CME.Common.Enums;
using CME.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CME.Common.Controllers
{
    public class IngoControllerBase : Controller
    {
        protected string AccountId
        {
            get
            {
                return User.Claims.GetValue<string>(Constants.Claims.SubjectId);
            }
        }

        protected string UserName
        {
            get
            {
                return User.Claims.GetValue<string>(Constants.Claims.UserName);
            }
        }

        protected string UserEmail
        {
            get
            {
                return User.Claims.GetValue<string>(Constants.Claims.Email);
            }
        }

        protected string FirstName
        {
            get
            {
                return User.Claims.GetValue<string>(Constants.Claims.GivenName);
            }
        }

        protected string LastName
        {
            get
            {
                return User.Claims.GetValue<string>(Constants.Claims.FamilyName);
            }
        }

        protected string Gender
        {
            get
            {
                return User.Claims.GetValue<string>(Constants.Claims.Gender);
            }
        }

        protected string TimeZone
        {
            get
            {
                return Request.Headers.TryGetValue(Constants.Headers.TimeZone, out var value) ? value.ToString() : string.Empty;
            }
        }

        protected string TenantId
        {
            get
            {
                return Request.Headers.TryGetValue(Constants.Headers.TenantId, out var value) ? value.ToString() : string.Empty;
            }
        }

        protected string OsName
        {
            get
            {
                Request.Headers.TryGetValue(Constants.Headers.OsName, out var headerValues);
                var value = headerValues.FirstOrDefault()?.ToLowerInvariant() ?? string.Empty;
                var enumNames = Enum.GetValues(typeof(DeviceType)).Cast<DeviceType>().Select(x => x.ToString()).ToList();
                return enumNames.FirstOrDefault(x => value.Contains(x));
            }
        }

        protected string Locale
        {
            get
            {
                Request.Headers.TryGetValue(Constants.Headers.AcceptLanguage, out var value);
                CultureInfo ci;
                if (string.IsNullOrWhiteSpace(value))
                {
                    ci = CultureInfo.GetCultureInfo("en");
                }
                else
                {
                    try
                    {
                        ci = CultureInfo.GetCultureInfo(value);
                    }
                    catch (Exception)
                    {
                        ci = CultureInfo.GetCultureInfo("en");
                    }
                }
                return ci.Name;
            }
        }
    }
}
