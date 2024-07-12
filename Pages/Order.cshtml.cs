using Bakery.Data;
using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;


namespace Bakery.Pages;

public class OrderModel : PageModel
{
    private readonly BakeryContext _bakeryContext;
    public OrderModel(BakeryContext bakeryContext) => _bakeryContext = bakeryContext;


    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    public Product Product { get; set; }

    [BindProperty, Range(1, int.MaxValue, ErrorMessage ="You must order at least one item")]
    public int Quantity {get; set;}=1;

    [BindProperty, Required, EmailAddress, Display(Name ="Your Email Addresse")]
    public string OrderEmail {get; set;}

    [BindProperty, Required, Display(Name ="Shipping Addresse")]
    public string ShippingAddress {get; set;}

    [BindProperty]
    public decimal UnitPrice {get; set;}

    [TempData]
    public string Confirmation {get; set;}




    public async Task OnGetAsync()
    {
        Product = await _bakeryContext.Products.FindAsync(Id);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Product = await _bakeryContext.Products.FindAsync(Id);
        if(ModelState.IsValid)
        {
            Confirmation = @$"You have ordered {Quantity} x {Product.Name} 
                            at a total cost of {Quantity * Product.Price:c}";
                            // at a total cost of {Quantity * UnitPrice:c}";
            return RedirectToPage("/OrderSuccess");
        }
        return Page();
    }
 
}
