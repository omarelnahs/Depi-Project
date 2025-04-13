using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Context;
using MVC.Models;

namespace MVC.Controllers

{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(AppDbContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var store = await _context.Stores.FirstOrDefaultAsync(s => s.UserId == userId);

            if (store == null)
                return Unauthorized();

            var products = _context.Products
                .Include(p => p.Store)
                .Where(p => p.StoreId == store.Id);

            return View(await products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Store)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var store = _context.Stores.FirstOrDefault(s => s.UserId == userId);
            if (store == null) return Unauthorized();

            // Set ViewBag for categories and store name
            ViewBag.Categories = new MultiSelectList(_context.Categories, "Id", "Name");
            ViewBag.StoreName = store.Name;
            ViewBag.StoreId = store.Id; // Pass the store ID to the view

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, List<int>? selectedCategories)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Unauthorized access attempt to Create action.");
                return Unauthorized();
            }

            // Validate the store ID from the form
            var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == product.StoreId && s.UserId == userId);
            if (store == null)
            {
                _logger.LogWarning("Store not found or does not belong to user ID: {UserId}", userId);
                return Unauthorized();
            }

            selectedCategories ??= new List<int>();

            if (ModelState.IsValid)
            {
                try
                {
                    product.StoreId = store.Id;

                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();

                    if (selectedCategories.Any())
                    {
                        foreach (var categoryId in selectedCategories)
                        {
                            _context.ProductCategories.Add(new ProductCategory
                            {
                                ProductId = product.Id,
                                CategoryId = categoryId
                            });
                        }

                        await _context.SaveChangesAsync();
                    }

                    _logger.LogInformation("Product created successfully with ID: {ProductId}", product.Id);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating product.");
                    ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
                }
            }

            _logger.LogWarning("Model state is invalid for product creation.");
            ViewBag.Categories = new MultiSelectList(_context.Categories, "Id", "Name", selectedCategories);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.StoreId = new SelectList(_context.Stores, "Id", "Name", product.StoreId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl,Price,Stock,StoreId")] Product product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Product with ID {ProductId} updated successfully.", product.Id);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!ProductExists(product.Id))
                    {
                        _logger.LogWarning("Concurrency conflict: Product with ID {ProductId} no longer exists.", product.Id);
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError(ex, "Concurrency conflict occurred while updating product with ID {ProductId}.", product.Id);
                        ModelState.AddModelError("", "The product was updated by another user. Please refresh the page and try again.");
                        // Reload the product data
                        var existingProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                        if (existingProduct != null)
                        {
                            product = existingProduct;
                        }
                    }
                }
            }

            _logger.LogWarning("Model state is invalid for product update with ID {ProductId}.", product.Id);
            ViewBag.StoreId = new SelectList(_context.Stores, "Id", "Name", product.StoreId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Store)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                _logger.LogWarning("Attempted to delete a non-existent product with ID {ProductId}.", id);
                return NotFound();
            }

            // Check if the product is part of any orders or carts
            var isInOrders = await _context.OrderItems.AnyAsync(oi => oi.ProductId == id);
            var isInCarts = await _context.CartItems.AnyAsync(ci => ci.ProductId == id);

            if (isInOrders || isInCarts)
            {
                _logger.LogWarning("Product with ID {ProductId} cannot be deleted because it is part of an order or cart.", id);
                ModelState.AddModelError("", "This product cannot be deleted because it is part of an order or cart.");
                return View(product);
            }

            try
            {
                // Remove associated categories
                _context.ProductCategories.RemoveRange(product.ProductCategories);

                // Remove the product
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Product with ID {ProductId} deleted successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product with ID {ProductId}.", id);
                ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
                return View(product);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }

}
