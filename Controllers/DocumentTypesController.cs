using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaMVC.DataBases;
using PracticaMVC.Models;


namespace PracticaMVC.Controllers;

[Route("[controller]")]

public class DocumentTypesController : Controller
{
    private readonly ILogger<DocumentTypesController> _logger;
    private readonly ApplicationDbContext _context;

    public DocumentTypesController(ILogger<DocumentTypesController> logger,ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }


    [Route("Index")]

//metodo asincrono
    public async Task<IActionResult> Index()
    {
        var allDocumentTypes = await _context.DocumentTypes.ToListAsync();
        return View(allDocumentTypes);
    }

    [Route("Create")]

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    
    public async Task<IActionResult> Create(DocumentType documentType)
    {
        if (ModelState.IsValid)
        {
            _context.Add(documentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        else
        {
            _logger.LogError("Error saving");
            return View(documentType);
        }

    }

    [Route("Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var documnetTypeFound = await _context.DocumentTypes.FindAsync(id);
        if(documnetTypeFound == null)
        {
            return NotFound();
        }
    }
    


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    
    [Route("Error")]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
