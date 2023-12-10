using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShiftType.Controllers
{
    public class LanguageController : Controller
    {
        [HttpPost("language/change")]
        public IActionResult changeLanguage([FromBody] string search)
        {
            return PartialView("Partials/_LanguageListPartial", search);
        }
    }
}
