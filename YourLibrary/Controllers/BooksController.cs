using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using YourLibrary.Abstractions;
using YourLibrary.Models;
using YourLibrary.ViewModels;

namespace YourLibrary.Controllers;

public class BooksController : Controller
{
    private readonly IRepository<Author> _authorRepository;
    private readonly IRepository<Image>  _imageRepository;
    private readonly IRepository<Book>   _bookRepository;
    private readonly IPhotoService       _photoService;

    public BooksController(IRepository<Book> bookRepository, 
        IPhotoService photoService, 
        IRepository<Image> imageRepository, 
        IRepository<Author> authorRepository)
    {
        _imageRepository  = imageRepository;
        _authorRepository = authorRepository;
        _bookRepository   = bookRepository;   
        _photoService     = photoService;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _bookRepository.GetAllAsync();

        return View(books);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookViewModel createBookViewModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "PhotoUploadFailed");

            return View(createBookViewModel);
        }
        
        ImageUploadResult? result = null;   
        
        if (createBookViewModel.Image != null)
        {
            result = await _photoService.AddPhotoAsync(createBookViewModel.Image);
        }

        var book = new Book
        {
            Name = createBookViewModel.Name,
            Author = createBookViewModel.Author,
            Image = result == null ? null : new Image { Link = result.Url.ToString() }
        };

        await _bookRepository.AddAsync(book);
        
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        
        var bookVm = new EditBookViewModel
        {
            Name = book.Name,
            Author = book.Author.Name,
            Image = null
        };
        
        return View(bookVm);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditBookViewModel bookViewModel)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        ImageUploadResult? photoUploadResult = null;

        if (bookViewModel.Image != null)
        {
            photoUploadResult = await _photoService.AddPhotoAsync(bookViewModel.Image);
        }

        book.Name = bookViewModel.Name;
        book.Author.Name = bookViewModel.Author;
        
        if (book.Image == null && photoUploadResult != null)
        {
            book.Image = new Image
            {
                Link = photoUploadResult.Url.ToString()
            };

            await _imageRepository.AddAsync(book.Image);
        }
        else if (book.Image != null)
        {
            if (photoUploadResult != null)
            {
                book.Image.Link = photoUploadResult.Url.ToString();
                await _imageRepository.UpdateAsync(book.Image);
            }
            else
            {
                await _imageRepository.DeleteAsync(book.Image);
                book.Image = null;
            }
        }

        await _bookRepository.UpdateAsync(book);

        return RedirectToAction("Edit");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book.Image != null)
        {
            await _photoService.DeletePhotoAsync(book.Image.Link);
            await _imageRepository.DeleteAsync(book.Image);
        }

        await _bookRepository.DeleteAsync(book);
        await _authorRepository.DeleteAsync(book.Author);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> ClearAll()
    {
        var books = await _bookRepository.GetAllAsync();
        
        foreach (var book in books.Where(b => b.Image != null))
        {
            await _photoService.DeletePhotoAsync(book.Image.Link);
        }
        
        await _bookRepository.DeleteAllAsync();  
        await _authorRepository.DeleteAllAsync();
        await _imageRepository.DeleteAllAsync(); 

        return RedirectToAction("Index");
    }
}
