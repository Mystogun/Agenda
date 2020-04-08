using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LawAgendaApi.Data;
using LawAgendaApi.Data.Entities;
using LawAgendaApi.Data.Queries.Search;
using Microsoft.EntityFrameworkCore;

namespace LawAgendaApi.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly DataContext _context;

        public CustomerRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();

            return customers;
        }

        public async Task<Customer> GetCustomerById(long customerId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);

            return customer;
        }

        public async Task<IEnumerable<Customer>> Search(SearchQuery<CustomerSearchQueryType> query, bool singleFilter)
        {
            if (string.IsNullOrEmpty(query.Query) || string.IsNullOrWhiteSpace(query.Query))
            {
                return null;
            }

            var customers = new List<Customer>();
            if (singleFilter)
            {
                switch (query.Filter)
                {
                    case CustomerSearchQueryType.Name:
                        customers = await _context.Customers.Where(c => EF.Functions.Like(c.Name, $"%{query.Query}%"))
                            .ToListAsync();
                        break;
                    case CustomerSearchQueryType.Username:
                        customers = await _context.Customers
                            .Where(c => EF.Functions.Like(c.Username, $"%{query.Query}%"))
                            .ToListAsync();
                        break;
                    case CustomerSearchQueryType.PhoneNumber:
                        customers = await _context.Customers
                                        .Where(c => EF.Functions.Like(c.PhoneNumber, $"%{query.Query}%"))
                                        .ToListAsync() ??
                                    await _context.Customers
                                        .Where(c => EF.Functions.Like(c.PhoneNumber2, $"%{query.Query}%"))
                                        .ToListAsync();

                        break;
                    default:
                        customers = null;
                        break;
                }

                return customers;
            }

            customers = await _context.Customers.Where(c =>
                    EF.Functions.Like(c.Name, $"%{query.Query}%") ||
                    EF.Functions.Like(c.Username, $"%{query.Query}%") ||
                    EF.Functions.Like(c.PhoneNumber, $"%{query.Query}%") ||
                    EF.Functions.Like(c.PhoneNumber2, $"%{query.Query}%"))
                .ToListAsync();

            return customers;
        }

        public async Task<SaveChangesToDbResult<Customer>> CreateCustomer(Customer customer)
        {
            var customerFromDb = await _context.Customers.FirstOrDefaultAsync(c => c.Username == customer.Username);

            var result = new SaveChangesToDbResult<Customer>();
            if (customerFromDb != null)
            {
                result.Message = "Already Exists";
                result.Entity = null;
                return result;
            }

            var addResult = await _context.Customers.AddAsync(customer);

            var isSaved = (await _context.SaveChangesAsync()) > 0;

            if (isSaved)
            {
                result.Message = "Success";
                result.Entity = addResult.Entity;
                return result;
            }

            result.Message = "Could Not Save, Try Again Later";
            result.Entity = null;
            return result;
        }

        public async Task<SaveChangesToDbResult<Customer>> UpdateCustomer(Customer customer)
        {
            var customerFromDb = await GetCustomerById(customer.Id);

            var result = new SaveChangesToDbResult<Customer>();
            if (customerFromDb == null)
            {
                result.Message = "Not Found";
                result.Entity = null;
                return result;
            }

            var updateResult = _context.Customers.Update(customer);

            var isSaved = (await _context.SaveChangesAsync()) > 0;

            if (isSaved)
            {
                result.Message = "Success";
                result.Entity = updateResult.Entity;
                return result;
            }

            result.Message = "Could Not Save, Try Again Later";
            result.Entity = null;
            return result;
        }

        public async Task<SaveChangesToDbResult<Customer>> DeleteCustomer(long customerId)
        {
            var customerFromDb = await GetCustomerById(customerId);

            var result = new SaveChangesToDbResult<Customer>();
            if (customerFromDb == null)
            {
                result.Message = "Not Found";
                result.Entity = null;
                return result;
            }

            customerFromDb.IsDeleted = 1;
            
            var deleteResult = _context.Customers.Update(customerFromDb);

            var isSaved = (await _context.SaveChangesAsync()) > 0;

            if (isSaved)
            {
                result.Message = "Success";
                result.Entity = null;
                return result;
            }

            result.Message = "Could Not Save, Try Again Later";
            result.Entity = null;
            return result;
        }
    }
}