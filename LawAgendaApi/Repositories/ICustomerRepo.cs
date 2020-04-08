using System.Collections.Generic;
using System.Threading.Tasks;
using LawAgendaApi.Data;
using LawAgendaApi.Data.Entities;
using LawAgendaApi.Data.Queries.Search;

namespace LawAgendaApi.Repositories
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(long customerId);
        Task<IEnumerable<Customer>> Search(SearchQuery<CustomerSearchQueryType> query, bool singleFilter);
        Task<SaveChangesToDbResult<Customer>> CreateCustomer(Customer customer);
        Task<SaveChangesToDbResult<Customer>> UpdateCustomer(Customer customer);
        Task<SaveChangesToDbResult<Customer>> DeleteCustomer(long customerId);

    }
}