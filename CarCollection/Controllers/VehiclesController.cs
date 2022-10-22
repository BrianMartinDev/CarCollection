﻿using AutoMapper;
using CarCollection.Data;
using CarCollection.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarCollection.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
        {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VehiclesController(ApplicationDbContext context, IMapper mapper)
            {
            _context = context;
            _mapper = mapper;
            }

        /// <summary>
        /// Api controller that returns an IEnumerable list of vehicles.
        /// </summary>
        /// <remarks>[GET] Endpoint: api/Vehicles</remarks>
        /// <returns>A IEnumerable list of <see cref="Vehicle"/> objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
            {
            var Vehicles = await _context.Vehicles.ToListAsync();
            var vehicleViewModel = _mapper.Map<IEnumerable<VehicleViewModel>>(Vehicles);
            return Ok(vehicleViewModel);
            }

        /// <summary>
        /// Api controller that a single vehicle.
        /// </summary>
        /// <remarks>[HttpGet("{id}")] Endpoint: api/Vehicles/5</remarks>
        /// <param name="id">Id property for Vehicle Id.</param>
        /// <returns>A single Vehicles object.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleViewModel>> GetVehicle(int id)
            {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
                {
                return NotFound();
                }
            var vehicleViewModel = _mapper.Map<Vehicle>(vehicle);
            return Ok(vehicleViewModel);
            }

        /// <summary>
        /// Api controller that updates a single vehicle.
        /// </summary>
        /// <remarks>[HttpPut("{id}")] Endpoint: api/Vehicles/5</remarks>
        /// <param name="id">Id property for Vehicle Id.</param>
        /// <param name="VehicleViewModel">User input of a single Vehicle object.</param>
        /// <returns>The created <see cref="VehicleViewModel"/> for the response.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, VehicleViewModel vehicleViewModel)
            {

            var vehicle = new Vehicle
                {
                Id = id,
                Name = vehicleViewModel.Name,
                Year = vehicleViewModel.Year,
                Address = vehicleViewModel.Address,
                Description = vehicleViewModel.Description,
                Engine = vehicleViewModel.Engine,
                };

            if (id != vehicle.Id)
                {
                return BadRequest();
                }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
                {
                await _context.SaveChangesAsync();
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!VehicleExists(id))
                    {
                    return NotFound();
                    }
                else
                    {
                    throw;
                    }
                }

            return Ok(vehicle);
            }

        /// <summary>
        /// Api controller that creates a single vehicle object.
        /// </summary>
        /// <remarks>[HttpPost] Endpoint: api/Vehicles</remarks>
        /// <param name="VehicleViewModel">User input of a single Vehicle object.</param>
        /// <returns>The created <see cref="VehicleViewModel"/> for the response.</returns>
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(VehicleViewModel vehicleViewModel)
            {

            //var vehicle = new Vehicle
            //    {
            //    Name = vehicleViewModel.Name,
            //    Year = vehicleViewModel.Year,
            //    Address = vehicleViewModel.Address,
            //    Description = vehicleViewModel.Description,
            //    Engine = vehicleViewModel.Engine,
            //    };

            var vehicle = _mapper.Map<Vehicle>(vehicleViewModel);

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, vehicle);
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
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
                {
                return NotFound();
                }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
            }

        /// <summary>
        /// Check if Vehicle exist else return false
        /// </summary>
        /// <param name="id">Id property for Vehicle Id.</param>
        /// <returns>returns boolean value</returns>
        private bool VehicleExists(int id)
            {
            return _context.Vehicles.Any(e => e.Id == id);
            }
        }
    }
