using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POEProg.Data;
using POEProg.Models;

namespace POEProg.Controllers
{
    public class ApprovalsController : Controller
    {
        private readonly POEProgContext _context;

        public ApprovalsController(POEProgContext context)
        {
            _context = context;
        }

        // GET: Approvals
        public async Task<IActionResult> Index()
        {
            var pOEProgContext = _context.Approval.Include(a => a.Claim);
            return View(await pOEProgContext.ToListAsync());
        }

        // GET: Approvals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approval = await _context.Approval
                .Include(a => a.Claim)
                .FirstOrDefaultAsync(m => m.ApprovalId == id);
            if (approval == null)
            {
                return NotFound();
            }

            return View(approval);
        }

        // GET: Approvals/Create
        public IActionResult Create()
        {
            ViewData["ClaimId"] = new SelectList(_context.Claim, "ClaimId", "ClaimId");
            return View();
        }

        
    }
}
