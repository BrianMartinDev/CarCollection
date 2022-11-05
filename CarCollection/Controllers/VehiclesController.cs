using AutoMapper;
using CarCollection.Data;
using CarCollection.Repository.IRepository;
using CarCollection.ViewModels.Vehicle;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarCollection.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
        {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public VehiclesController(IVehicleRepository vehicleRepository, IMapper mapper)
            {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
            }

        /// <summary>
        /// Api controller that returns an IEnumerable list of vehicles.
        /// </summary>
        /// <remarks>[GET] Endpoint: api/Vehicles</remarks>
        /// <returns>A IEnumerable list of <see cref="VehicleViewModel"/> objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleViewModel>>> GetVehicles()
            {
            var Vehicles = await _vehicleRepository.GetAllAsync();
            var vehicleViewModel = _mapper.Map<IEnumerable<VehicleViewModel>>(Vehicles);
            return Ok(vehicleViewModel);
            }


        /// <summary>
        /// Api controller that a single vehicle with comments.
        /// </summary>
        /// <remarks>HttpGet("GetVehicleWithComments/{vehicleId}")] Endpoint: api/Vehicles/GetVehicleWithComments/{vehicleId}</remarks>
        /// <param name="id">Id property for Vehicle Id.</param>
        /// <returns>A single Vehicles object with comments.</returns>
        [HttpGet("GetVehicleWithComments/{vehicleId}")]
        public async Task<ActionResult<VehicleViewModel>> GetVehicleWithComments(int? vehicleId)
            {
            if (vehicleId == null) return BadRequest();

            var vehicle = await _vehicleRepository.GetVehicleWithComments(vehicleId);

            if (vehicle == null) return NotFound();

            var vehicleViewModel = _mapper.Map<VehicleViewModel>(vehicle);
            return Ok(vehicleViewModel);
            }


        /// <summary>
        /// Api controller that a single vehicle.
        /// </summary>
        /// <remarks>[HttpGet("{id}")] Endpoint: api/Vehicles/5</remarks>
        /// <param name="id">Id property for Vehicle Id.</param>
        /// <returns>A single Vehicles object.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleViewModel>> GetVehicle(int? id)
            {
            if (id == null) return BadRequest();

            var vehicle = await _vehicleRepository.GetAsync(id);

            if (vehicle == null) return NotFound();
            var vehicleViewModel = _mapper.Map<VehicleViewModel>(vehicle);
            return Ok(vehicleViewModel);
            }

        /// <summary>
        /// Api controller that updates a single vehicle.
        /// </summary>
        /// <remarks>[HttpPut("{id}")] Endpoint: api/Vehicles/5</remarks>
        /// <param name="id">Id property for Vehicle Id.</param>
        /// <param name="UpdateVehicleViewModel">User input of a single Vehicle object.</param>
        /// <returns>The created <see cref="UpdateVehicleViewModel"/> for the response.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, UpdateVehicleViewModel updateVehicleViewModel)
            {

            if (id != updateVehicleViewModel.Id) return BadRequest();

            var vehicle = await _vehicleRepository.GetAsync(id);

            if (vehicle == null) return NotFound();

            _mapper.Map(updateVehicleViewModel, vehicle);

            try
                {
                await _vehicleRepository.UpdateAsync(vehicle);
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!await _vehicleRepository.Exist(id))
                    {
                    return NotFound();
                    }
                else
                    {
                    throw;
                    }
                }
            var vehicleVm = _mapper.Map<GetVehicleViewModel>(vehicle);
            return Ok(vehicleVm);
            }

        /// <summary>
        /// Api controller that creates a single vehicle object.
        /// </summary>
        /// <remarks>[HttpPost] Endpoint: api/Vehicles</remarks>
        /// <param name="CreateVehicleViewModel">User input of a single Vehicle object.</param>
        /// <returns>The created <see cref="CreateVehicleViewModel"/> for the response.</returns>
        [HttpPost]
        public async Task<ActionResult<CreateVehicleViewModel>> PostVehicle(CreateVehicleViewModel createVehicleViewModel)
            {

            var vehicle = _mapper.Map<Vehicle>(createVehicleViewModel);

            await _vehicleRepository.CreateAsync(vehicle);

            return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, createVehicleViewModel);
            }

        /// <summary>
        /// Api controller that deletes a single vehicle.
        /// </summary>
        /// <remarks>[HttpDelete("{id}")] Endpoint: HttpPost: api/Vehicles/{id}</remarks>
        /// <param name="id">Id property for Vehicle Id.</param>
        /// <returns>After successful execution the response status is 200 OK.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
            {
            var vehicle = await _vehicleRepository.GetAsync(id);

            if (vehicle == null) return NotFound();

            await _vehicleRepository.DeleteAsync(vehicle);

            return NoContent();
            }
        }
    }
