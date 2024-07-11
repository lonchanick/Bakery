using Bakery.Data;
using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bakery.Pages;

public class OrderModel : PageModel
{
    private readonly BakeryContext _bakeryContext;


    public OrderModel(BakeryContext bakeryContext) => _bakeryContext = bakeryContext;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    public Product Product { get; set; }
    public async Task OnGetAsync()
    {
        Product = await _bakeryContext.Products.FindAsync(Id);
    }
 
}
