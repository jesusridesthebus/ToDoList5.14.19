using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System;

namespace ToDoList.Controllers
{
  public class ItemsController : Controller
  {

    [HttpGet("/items")]
    public ActionResult Index()
    {
      List<Item> allItems = Item.GetAll();
      return View(allItems);
    }

    [HttpGet("/items/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/items")]
    public ActionResult Create(string description)
    {
      Item newItem = new Item(description);
      newItem.Save();
      List<Item> allItems = Item.GetAll();
      return View("Index", allItems);
    }

    [HttpGet("/categories/{categoryId}/items/{itemId}")]
    public ActionResult Show(int categoryId, int itemId)
    {
      Item item = Item.Find(itemId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Category category = Category.Find(categoryId);
      model.Add("item", item);
      model.Add("category", category);
      return View(model);
    }

    [HttpPost("/items/delete")]
    public ActionResult DeleteAll()
    {
      Item.ClearAll();
      return View();
    }

    [HttpPost("/categories/{categoryId}/items/{itemId}/delete")]
    public ActionResult Delete(int categoryId, int itemId)
    {
      Item item = Item.Find(itemId);
      item.DeleteItem();
      Dictionary<string, object> model = new Dictionary<string, object>();
      Category category = Category.Find(categoryId);
      model.Add("category", category);
      model.Add("item", item);
      return RedirectToAction("Show", "Categories");
    }

    [HttpGet("/categories/{categoryId}/items/{itemId}/edit")]
    public ActionResult Edit(int categoryId, int itemId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Category category = Category.Find(categoryId);
      model.Add("category", category);
      Item item = Item.Find(itemId);
      model.Add("item", item);
      return View(model);
    }

    [HttpPost("/categories/{categoryId}/items/{itemId}")]
    public ActionResult Update(int categoryId, int itemId, string newDescription)
    {
      Item item = Item.Find(itemId);
      item.Edit(newDescription);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Category category = Category.Find(categoryId);
      model.Add("category", category);
      model.Add("item", item);
      return View("Show", model);
    }

    // [HttpGet("/categories/{id}")]
    // public ActionResult Show(int id)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Category selectedCategory = Category.Find(id);
    //   List<Item> categoryItems = selectedCategory.GetItems();
    //   model.Add("category", selectedCategory);
    //   model.Add("items", categoryItems);
    //   return View(model);
    // }
  }
}
