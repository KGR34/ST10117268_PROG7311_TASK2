using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10117268_PROG7311_TASK2.Models;
using ST10117268_PROG7311_TASK2.Temp;
using System.Linq;
using System.Threading.Tasks;

namespace ST10117268_PROG7311_TASK2.Controllers
{
    public class UsersController : Controller
    {
        private readonly st10117268Context _context;

        public UsersController(st10117268Context context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Fullname,Email,Password,Role")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            ViewData["Roles"] = PopulateCmp.listRoles;
            logoutUser();
            return View();
        }

        // POST: Users/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password,Role")] TempLogin login)
        {
            ViewData["Roles"] = PopulateCmp.listRoles;
            User user = new User();
            if (ModelState.IsValid)
            {
                var userDetails = await _context.Users.Where(x => x.Email.Equals(login.Email) && x.Password.Equals(login.Password) && x.Role.Equals(login.Role)).FirstOrDefaultAsync();

                if (userDetails != null)
                {
                    currentUser.currentUserEmail = login.Email;
                    currentUser.currentUserId = await _context.Users.Where(x => x.Email.Equals(login.Email)).Select(x => x.UserId).FirstOrDefaultAsync();
                    currentUser.userRole = login.Role;

                    if (currentUser.userRole == "Employee")
                    {
                        // if role is employee go to the home page
                        return RedirectToAction("Index", "Home");
                    }
                    else if (currentUser.userRole == "Farmer")
                    {
                        // if role is farmer go to view the farmers products
                        return RedirectToAction("Index", "UsersProducts");
                    }
                }
                else
                {
                    return View(login);
                }
            }
            return View(login);
        }

        // GET: Users/Register
        public IActionResult Register()
        {
            ViewData["Roles"] = PopulateCmp.listRoles;
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserId,Fullname,Email,Password,Role")] TempRegister register)
        {
            ViewData["Roles"] = PopulateCmp.listRoles;
            User user = new User();
            if (ModelState.IsValid)
            {
                user.Fullname = register.Fullname;
                user.Email = register.Email;
                user.Password = register.Password;
                user.Role = register.Role;
                currentUser.currentUserId = await _context.Users.Where(x => x.Email.Equals(register.Email)).Select(x => x.UserId).FirstOrDefaultAsync();

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }



        public void logoutUser() 
        {
            currentUser.currentUserEmail = "";
            currentUser.currentUserId = 0;
            currentUser.userRole = "";
        }
    }
}
