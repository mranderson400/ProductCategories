using ProductCategories.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductCategories.Controllers;

public class ProductController : Controller
{
private ProductCategories.Models.MyContext db;         
    // Here we can "inject" our context service into the constructor 
    public ProductController(ProductCategories.Models.MyContext context)    
    {         
        // When our ProductController is instantiated, it will fill in db with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
        db = context;    
    }       

//PRODUCT PAGE
[HttpGet("")]
[HttpGet("/products")]
    public IActionResult NewProduct()
    {
        ViewBag.allProducts= db.Products.OrderBy(products => products.Name).ToList();
        return View("ProductNew");
    }

//ADD PRODUCT
[HttpPost("/products/create")]
public IActionResult CreateProduct(Product newProduct)
    {
        // string messages = string.Join("; ", ModelState.Values
        //     .SelectMany(x => x.Errors)
        //     .Select(x => x.ErrorMessage));         
        //     Console.WriteLine(messages);
        //     //line 58 61 to catch extra errs

        //ADD VALIFATION FEATURE IN VIEW
        if (!ModelState.IsValid)
        {
            return NewProduct();
        }
        db.Products.Add(newProduct);
        //the .add is maping over your data table in mysql bench

        db.SaveChanges();
        
       return RedirectToAction("NewProduct");
    }

//SHOW ONE PRODUCCT
    [HttpGet("/product/{productId}")]
    public IActionResult DetailsProduct(int productId)
    {
        Product? one_product = db.Products.Include(product => product.Relationships).
        ThenInclude(relation => relation.Category).
        FirstOrDefault(product => product.ProductId == productId);

        if(one_product == null)
        {
            return RedirectToAction("NewProduct");
        }
        
        ViewBag.categorylist = db.Categories.OrderBy(name => name.Name).ToList();
        return View("ViewAdd", one_product);
    }

//ADD CATERGORY TO PRODUCT
    [HttpPost("/product/AddCatToProd")]
public IActionResult AddCatToProd(Relation newCatToProd)
    {
        // Relation newRelationship = new Relation()
        // {
        //     CategoryId = categoryId,
        //     ProductId = productId
        // };
        db.Add(newCatToProd);
        db.SaveChanges();
        
        return RedirectToAction("DetailsProduct", new{productId = newCatToProd.ProductId});
        // cant redirecto action with same function names for post methods
        // return RedirectToAction returns to function of desire route
        // return View refers to cshtml file name
    }
}