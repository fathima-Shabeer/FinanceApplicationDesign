// Controllers/RealEstateController.cs
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// [Authorize] // Protect controller or actions
public class RealEstateController : Controller
{
    private readonly IRealEstateService _realEstateService;
    private readonly IMapper _mapper; // Often useful in controllers too

    public RealEstateController(IRealEstateService realEstateService, IMapper mapper)
    {
        _realEstateService = realEstateService ?? throw new ArgumentNullException(nameof(realEstateService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    // GET: /RealEstate
    public async Task<IActionResult> Index()
    {
        var propertiesDto = await _realEstateService.GetAllPropertiesAsync();
        // Map DTOs to ViewModels if needed for UI-specific logic/formatting
        var viewModels = _mapper.Map<IEnumerable<PropertyViewModel>>(propertiesDto);
        return View(viewModels);
    }

    // GET: /RealEstate/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var propertyDto = await _realEstateService.GetPropertyDetailsAsync(id);
        if (propertyDto == null)
        {
            return NotFound();
        }
        var viewModel = _mapper.Map<PropertyViewModel>(propertyDto);
        return View(viewModel);
    }

    // GET: /RealEstate/Create
    public IActionResult Create()
    {
        return View(new PropertyCreateViewModel()); // Use a ViewModel for the form
    }

    // POST: /RealEstate/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PropertyCreateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var createDto = _mapper.Map<PropertyCreateDto>(viewModel);
                int newId = await _realEstateService.CreatePropertyAsync(createDto);
                // Add TempData message for success
                TempData["SuccessMessage"] = "Property created successfully!";
                return RedirectToAction(nameof(Details), new { id = newId });
            }
            catch (Exception ex) // Basic error handling
            {
                // Log the exception (use a proper logging framework)
                ModelState.AddModelError("", "An error occurred while creating the property.");
            }
        }
        // If model state is invalid, return the view with validation errors
        return View(viewModel);
    }

    // Add Edit, Delete actions similarly...
}