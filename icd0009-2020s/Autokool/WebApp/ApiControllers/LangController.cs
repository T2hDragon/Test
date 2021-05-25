using System.Collections.Generic;
using System.Linq;
using DAL.App.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PublicAPI.DTO.v1;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Language Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class LangController : ControllerBase
    {
        private readonly ILogger<LangController> _logger;
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;
        
        /// <summary>
        /// Language controller constructor
        /// </summary>
        /// <param name="logger">Logger constructor</param>
        /// <param name="localizationOptions">Localization options used to get available languages</param>
        public LangController(ILogger<LangController> logger, IOptions<RequestLocalizationOptions> localizationOptions)
        {
            _logger = logger;
            _localizationOptions = localizationOptions;
        }

        /// <summary>
        /// Gives languages which the system supports
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  ActionResult<IEnumerable<PublicAPI.DTO.v1.SupportedLanguage>> GetSupportedLanguages()
        {
            var res = _localizationOptions.Value.SupportedUICultures.Select(c => new PublicAPI.DTO.v1.SupportedLanguage()
            {
                Name = c.Name,
                NativeName = c.NativeName,
            });
            return Ok(res);
        }
        
        /// <summary>
        ///     Gives language resources used by the front end 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  ActionResult<LangResources> GetLangResources()
        {
            return Ok(new LangResources());
        }
        

    }
}