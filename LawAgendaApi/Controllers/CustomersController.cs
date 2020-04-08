using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LawAgendaApi.Data.Dtos;
using LawAgendaApi.Data.Dtos.Responses;
using LawAgendaApi.Data.Entities;
using LawAgendaApi.Data.Queries.Search;
using LawAgendaApi.Helpers;
using LawAgendaApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawAgendaApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepo customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        // GET : api/v1/customers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerRepo.GetAllCustomers();
            return Ok(new {Customers = customers});
        }

        // GET : api/v1/customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById([FromRoute(Name = "id")] long customerId)
        {
            var customer = await _customerRepo.GetCustomerById(customerId);
            return Ok(new {Customer = customer});
        }

        // GET : api/v1/customers/search
        [HttpGet("search")]
        public async Task<IActionResult> SearchCustomer([FromQuery(Name = "query")] string query,
            [FromQuery(Name = "filter")] string filter)
        {
            filter = filter.ConvertToPascalCase();

            if (!Enum.TryParse(filter, out CustomerSearchQueryType queryType))
            {
                return BadRequest();
            }

            var searchQuery = new SearchQuery<CustomerSearchQueryType>
            {
                Query = query,
                Filter = queryType
            };

            var customersFromDb = await _customerRepo.Search(searchQuery, true);

            var customers = _mapper.Map<IEnumerable<CustomerToReturnDto>>(customersFromDb);

            return Ok(new {Customers = customers});
        }

        // GET : api/v1/customers/find
        [HttpGet("find")]
        public async Task<IActionResult> FindCustomer([FromQuery(Name = "query")] string query)
        {
            var searchQuery = new SearchQuery<CustomerSearchQueryType>
            {
                Query = query,
            };

            var customersFromDb = await _customerRepo.Search(searchQuery, false);

            var customers = _mapper.Map<IEnumerable<CustomerToReturnDto>>(customersFromDb);

            return Ok(new {Customers = customers});
        }


        // POST : api/v1/customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromForm] CustomerToCreateRequestDto customerToCreateRequest)
        {
            var customerToCreate = _mapper.Map<Customer>(customerToCreateRequest);

            var result = await _customerRepo.CreateCustomer(customerToCreate);

            switch (result.Message)
            {
                case "Success":
                    var customerToReturn = _mapper.Map<CustomerToReturnDto>(result.Entity);
                    return CreatedAtAction("GetCustomerById", new {customerToReturn.Id},
                        new {Customer = customerToReturn});
                case "Already Exists":
                    return Conflict();
                default:
                    return StatusCode(500, "Unknown Error, Please Try Again Later");
            }
        }


        // PUT : api/v1/customers
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromForm] CustomerToUpdateRequestDto updateRequest)
        {
            var customerToUpdate = _mapper.Map<Customer>(updateRequest);

            customerToUpdate.UpdatedAt = DateTime.UtcNow.AddHours(3);

            var updateResult = await _customerRepo.UpdateCustomer(customerToUpdate);

            switch (updateResult.Message)
            {
                case "Success":
                    return Ok(new {Customer = updateResult.Entity});
                case "Not Found":
                    return NotFound();
                default:
                    return StatusCode(500, "Unknown Error, Please Try Again Later");
            }
        }

        // DEL : api/v1/customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute(Name = "id")] long customerId)
        {
            var deleteResult = await _customerRepo.DeleteCustomer(customerId);

            switch (deleteResult.Message)
            {
                case "Success":
                    return NoContent();
                case "Not Found":
                    return NotFound();
                default:
                    return StatusCode(500, "Unknown Error, Please Try Again Later");
            }
        }
    }
}