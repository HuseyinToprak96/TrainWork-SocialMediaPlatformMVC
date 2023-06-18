using CoreLayer.Entities.User;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.App.Controllers;

namespace Web.App.Framework.Pagination
{
    public class OrnekAction:BaseController
    {
        public async Task<ActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var data =await _userService.GetAllAsync();
            var totalCount = data.Data.Count();
            var items = data.Data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var model = new PagingModel<User>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalCount
            };
            return View(model);
        }
    }
}