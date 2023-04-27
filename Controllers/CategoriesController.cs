using ProductCategories.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductCategories.Controllers;

public class CategoryController : Controller
{
private ProductCategories.Models.MyContext db;         
    // Here we can "inject" our context service into the constructor 
    public CategoryController(ProductCategories.Models.MyContext context)    
    {         
        // When our CategoryController is instantiated, it will fill in db with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
        db = context;    
    }       

//CATEGORY PAGE
[HttpGet("/categories")]
    public IActionResult CategoryNew()
    {
        ViewBag.allCategories= db.Categories.OrderBy(categories => categories.Name).ToList();
        return View("CategoryNew");
        
    }

//ADD CATEGORY
[HttpPost("/categories/post")]
public IActionResult CreateCategory(Category newCategory)
    {
        string messages = string.Join("; ", ModelState.Values
            .SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage));         
            Console.WriteLine(messages);
            // catch extra errs

        if (!ModelState.IsValid)
        {
            return CategoryNew();
        }
        db.Categories.Add(newCategory);
        //the .add is maping over your data table in mysql bench

        db.SaveChanges();
        
        Console.WriteLine(newCategory.CategoryId);
         
        return RedirectToAction("NewCategory");
        // return RedirectToAction returns to function of desire route
        // return View refers to cshtml file name
    }

//SHOW ONE CATEGORY
[HttpGet("/category/{categoryId}")]
    public IActionResult DetailsCategory(int categoryId)
    {
        Category? one_category = db.Categories.Include(category => category.Relationships).ThenInclude(relation => relation.Product).
        FirstOrDefault(category => category.CategoryId == categoryId);

        if(one_category == null)
        {
            return RedirectToAction("CategoryNew");
        }
        
        ViewBag.productlist = db.Products.OrderBy(name => name.Name).ToList();
        // query to remove all ready associated products **UPDATE query
        

        return View("ViewAdd", one_category);
    }

   
//ADD PRODUCT TO CATEGORY
[HttpPost("/category/AddProdToCat")]
public IActionResult AddProdToCat(Relation newProdToCat)
    {
        // validation for many to many
        db.Add(newProdToCat);
        db.SaveChanges();
        
        return RedirectToAction("DetailsCategory", new {categoryId = newProdToCat.CategoryId});
        // cant redirecto action with same function names for post methods
        // return RedirectToAction returns to function of desire route
        // return View refers to cshtml file name
    }
}