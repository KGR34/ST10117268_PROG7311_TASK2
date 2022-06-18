using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ST10117268_PROG7311_TASK2.Models;
using ST10117268_PROG7311_TASK2.Temp;
using System.Linq;
using System.Threading.Tasks;

namespace ST10117268_PROG7311_TASK2.Controllers
{
    public class UsersProductsController : Controller
    {
        private readonly st10117268Context _context;

        public UsersProductsController(st10117268Context context)
        {
            _context = context;
        }

        // GET: UsersProducts
        public async Task<IActionResult> Index()
        {
            var st10117268Context = _context.UsersProducts.Include(u => u.Product).Include(u => u.User);
            return View(await st10117268Context.Where(x => x.UserId.Equals(currentUser.farmerUserId)).ToListAsync());
        }

        // GET: UsersProducts
        public async Task<IActionResult> ViewFarmersProducts()
        {
            var st10117268Context = _context.UsersProducts.Include(u => u.Product).Include(u => u.User);
            return View(await st10117268Context.Where(x => x.UserId.Equals(currentUser.farmerUserId)).ToListAsync());
        }

        // GET: UsersProducts
        public async Task<IActionResult> ViewFilterDate()
        {
            var st10117268Context = _context.UsersProducts.Include(u => u.Product).Include(u => u.User);
            return View(await st10117268Context.Where(x => x.UserId.Equals(currentUser.farmerUserId) && (x.ProductDate >= currentUser.startDate && x.ProductDate <= currentUser.endDate)).ToListAsync());
        }

        // GET: UsersProducts
        public async Task<IActionResult> ViewFilterProductType()
        {
            var st10117268Context = _context.UsersProducts.Include(u => u.Product).Include(u => u.User);
            return View(await st10117268Context.Where(x => x.UserId.Equals(currentUser.farmerUserId) && x.ProductType.Equals(currentUser.filterByProductType)).ToListAsync());
        }

        // GET: UsersProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersProduct = await _context.UsersProducts
                .Include(u => u.Product)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UsersProductId == id);
            if (usersProduct == null)
            {
                return NotFound();
            }

            return View(usersProduct);
        }

        // GET: UsersProducts/Create
        public IActionResult Create()
        {
            GetProductType();
            ViewData["PickProductType"] = PopulateCmp.listProductType;

            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: UsersProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName,Quantity,ProductType,ProductDate")] TempUserProducts tempUserProduct)
        {
            GetProductType();
            ViewData["PickProductType"] = PopulateCmp.listProductType;

            if (ModelState.IsValid)
            {
                Product product = new Product();
                UsersProduct usersProduct = new UsersProduct();

                // product
                product.ProductName = tempUserProduct.ProductName;
                _context.Add(product);
                await _context.SaveChangesAsync();
                var tempProductId = await _context.Products.Where(x => x.ProductName.Equals(tempUserProduct.ProductName)).Select(x => x.ProductId).FirstOrDefaultAsync();


                // userProduct
                usersProduct.UserId = currentUser.currentUserId;
                usersProduct.ProductId = tempProductId;
                usersProduct.ProductDate = tempUserProduct.ProductDate;
                usersProduct.Quantity = tempUserProduct.Quantity;
                usersProduct.ProductType = tempUserProduct.ProductType;
                usersProduct.ProductDate = tempUserProduct.ProductDate;
                _context.Add(usersProduct);
                await _context.SaveChangesAsync();
            }
            // ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", usersProduct.ProductId);
            // ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", usersProduct.UserId);
            return View(tempUserProduct);
        }

        // GET: UsersProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersProduct = await _context.UsersProducts.FindAsync(id);
            if (usersProduct == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", usersProduct.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", usersProduct.UserId);
            return View(usersProduct);
        }

        // POST: UsersProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsersProductId,UserId,ProductId,Quantity,ProductType,ProductDate")] UsersProduct usersProduct)
        {
            if (id != usersProduct.UsersProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersProductExists(usersProduct.UsersProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", usersProduct.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", usersProduct.UserId);
            return View(usersProduct);
        }

        // GET: UsersProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersProduct = await _context.UsersProducts
                .Include(u => u.Product)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UsersProductId == id);
            if (usersProduct == null)
            {
                return NotFound();
            }

            return View(usersProduct);
        }

        // POST: UsersProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usersProduct = await _context.UsersProducts.FindAsync(id);
            _context.UsersProducts.Remove(usersProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersProductExists(int id)
        {
            return _context.UsersProducts.Any(e => e.UsersProductId == id);
        }

        // GET: UsersProducts/PickFarmer
        public IActionResult PickFarmer()
        {
            GetFarmers();
            ViewData["PickFarmer"] = PopulateCmp.listFarmerNames;
            return View();
        }

        // POST: UsersProducts/PickFarmer
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PickFarmerAsync([Bind("FarmerName")] TempPickFarmer tempPickFarmer)
        {
            GetFarmers();
            ViewData["PickFarmer"] = PopulateCmp.listFarmerNames;
            if (ModelState.IsValid)
            {
                if (tempPickFarmer != null)
                {
                    currentUser.filterByFarmer = tempPickFarmer.FarmerName;
                    currentUser.farmerUserId = await _context.Users.Where(x => x.Fullname.Equals(tempPickFarmer.FarmerName)).Select(x => x.UserId).FirstOrDefaultAsync();
                    return RedirectToAction("ViewFarmersProducts", "UsersProducts");
                }
                else
                {
                    return View(tempPickFarmer);
                }
            }
            return View(tempPickFarmer);
        }

        // GET: Products/Create
        public IActionResult SelectFilterDate()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectFilterDate([Bind("StartDate,EndDate")] TempFilterDate tempFilterDate)
        {
            if (ModelState.IsValid)
            {
                currentUser.startDate = tempFilterDate.StartDate;
                currentUser.endDate = tempFilterDate.EndDate;
                return RedirectToAction("ViewFilterDate", "UsersProducts");
            }
            return View(tempFilterDate);
        }

        // GET: Products/Create
        public IActionResult SelectFilterProductType()
        {
            GetProductType();
            ViewData["PickProductType"] = PopulateCmp.listProductType;
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectFilterProductType([Bind("ProductType")] TempFilterProductType tempFilterProductType)
        {
            if (ModelState.IsValid)
            {
                currentUser.filterByProductType = tempFilterProductType.ProductType;
                return RedirectToAction("ViewFilterProductType", "UsersProducts");
            }
            return View(tempFilterProductType);
        }


        public void GetFarmers()
        {

            var farmerList = _context.Users.Where(x => x.Role.Equals("Farmer")).Select(x => x.Fullname).ToList();

            PopulateCmp.listFarmerNames.Clear();
            farmerList.ForEach(x => PopulateCmp.listFarmerNames.Add(new SelectListItem { Text = x, Value = x }));

        }

        public void GetProductType()
        {
            var productList = _context.UsersProducts.ToList();

            PopulateCmp.listProductType.Clear();

            var productTypeList = productList.Select(x => x.ProductType).Distinct().ToList();

            productTypeList.ForEach(x => PopulateCmp.listProductType.Add(new SelectListItem { Text = x, Value = x }));

        }


    }
}
