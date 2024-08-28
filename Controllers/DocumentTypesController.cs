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

    public DocumentTypesController(ILogger<DocumentTypesController> logger, ApplicationDbContext context)
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

    [Route("edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var documentTypeFound = await _context.DocumentTypes.FindAsync(id);
        return View(documentTypeFound);
    }

    [HttpPost("edit/{id}")]
    public async Task<IActionResult> Edit(int id, DocumentType documentType)
    {
        var result = CheckExist(id);
        if (result == false)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(documentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        else
        {
            _logger.LogError("El modelo no es valido");
            return View(documentType);
        }
    }



    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var documentTypeFound = await _context.DocumentTypes.FindAsync(id);
        if(documentTypeFound == null)
        {
            return View("Error");
        }
        return View(documentTypeFound);
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = CheckExist(id);
        if (result == false)
        {
            return NotFound();
        }

        var documentTypeFound = await _context.DocumentTypes.FindAsync(id);
        if (documentTypeFound == null)
        {
            return NotFound();
        }
        _context.DocumentTypes.Remove(documentTypeFound);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    private bool CheckExist(int id)
    {
        var checkDocumentType = _context.DocumentTypes.Any(e => e.Id == id);
        if (checkDocumentType == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    [Route("Error")]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
