using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelBookingApp.Models;
using TravelBookingApp.Repository;

namespace TravelBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelsController : ControllerBase
    {
        private readonly ITravelRepository _travelRepository;

        //constructor
        public TravelsController(ITravelRepository travelRepository)
        {
            _travelRepository = travelRepository;
        }

        #region Login

        [HttpGet]
        [Route("Login")]

        public async Task<IActionResult> Login(string userName, string password)
        {
            if (userName == null || password == null)
            {
                return BadRequest();
            }
            try
            {
                var user = await _travelRepository.Login(userName, password);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Add traveler

        //add route
        [HttpPost]
        [Authorize]
        [Route("AddTraveler")]

        public async Task<IActionResult> AddTraveler([FromBody] Traveler traveler)
        //check validation of body
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var travelerId = await _travelRepository.AddTraveler(traveler);
                    if (travelerId > 0)
                    {
                        return Ok(traveler);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        #endregion

        #region Update traveler

        //Update flight route
        [HttpPut]
        [Authorize]
        [Route("UpdateTraveler")]
        public async Task<IActionResult> UpdateTraveler([FromBody] Traveler traveler)
        {
            //check validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await _travelRepository.UpdateTraveler(traveler);
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        #endregion

        #region Delete traveler

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraveler(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                result = await _travelRepository.DeleteTraveler(id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion

        #region View all trveller list

        [HttpGet]
        [Route("ViewTravellers")]

        public async Task<IActionResult> ViewTravellers()
        {
            try
            {
                var user = await _travelRepository.GetTravellers();
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion

        #region Find travelers whose name starts with A

        [HttpGet]
        [Route("FindTraveler")]

        public async Task<IActionResult> FindTraveler()
        {
            try
            {
                var user = await _travelRepository.FindTraveler();
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion

        #region Find travelers from Delhi - travel date after 10 days

        [HttpGet]
        [Route("TravelerFromDelhi")]

        public async Task<IActionResult> FindTravelerFromDelhi()
        {
            try
            {
                var user = await _travelRepository.TravelerFromDelhi();
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion

        #region View all flight routes

        [HttpGet]
        [Route("ViewFlights")]
        public async Task<IActionResult> ViewFlightRoute()
        {
            try
            {
                var flight = await _travelRepository.GetFlightRoutes();
                if (flight == null)
                {
                    return NotFound();
                }
                return Ok(flight);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion
    }
}
