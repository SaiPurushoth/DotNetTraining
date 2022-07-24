using Microsoft.EntityFrameworkCore;
using MutualFundManagement.Models;

namespace MutualFundManagement.Services
{
    public class CustomerService
    {
        private readonly MutualFundDbContext _context;
        private readonly ILogger<Customers> _serilog;

        public CustomerService()
        {

        }

        public CustomerService(MutualFundDbContext context, ILogger<Customers> serilog)
        {
             _context = context;    
            _serilog = serilog; 
        }
        public async Task<List<Customers>> AllValues()
        {
            return _context.Customers.ToList<Customers>().ToList<Customers>();
        }

        public async virtual Task<object> GetDetails(int id)
        {
            if (id == null || _context.Customers == null)
            {
                _serilog.LogError("customer id is null");
                return null;
            }

            var customers = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customers == null)
            {
                _serilog.LogError("customer not found due to null");
                return null;
            }
            _serilog.LogInformation("Process done");
            return customers;
        }

        public async virtual Task<object> Verify(string email,string password)
        {
            if (email == null || password == null || _context.Customers == null)
            {
                _serilog.LogError("customer email or password is null");
                return null;
            }

            var customers = await _context.Customers
                .FirstOrDefaultAsync(m => m.Email == email && m.Password == password);

            if (customers == null)
            {
                _serilog.LogError("customer not found due to null");
                return null;
            }
            _serilog.LogInformation("Process done");
            return customers;
        }


        public async virtual Task<object> CreateCustomer(Customers customers)
        {
 

                 _context.Customers.Add(customers);
                await _context.SaveChangesAsync();
                _serilog.LogInformation("Process done");
                return customers;

        }

        public async virtual Task<object> EditCustomer(int id, string customername,string email,string password,bool isAdmin)
        {

            var customers = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
            
            if (id == null || _context.Customers == null)
            {
                _serilog.LogError("customer id is null");
                return null;
            }

            if (customers == null)
            {
                _serilog.LogError("customer not found");
                return null;
            }

            customers.CustomerName = customername;
            customers.Email = email;
            customers.Password = password;
            customers.IsAdmin = isAdmin;
                try
                {
                    _context.Customers.Update(customers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomersExists(customers.CustomerId))
                    {
                        _serilog.LogError("customer not found");
                        return null;
                    }
                    else
                    {
                        _serilog.LogError("db error");
                        throw;
                    }
                }
                _serilog.LogInformation("Process done");
                return new {message = "success"};
        }


        public async virtual Task<object> DeleteCustomer(int id)
        {
            var customers = await _context.Customers.FirstOrDefaultAsync(x=>x.CustomerId == id);
            if (_context.Customers == null || customers == null)
            {
                _serilog.LogError("customer not found due to null");
                return null;
            }
            var customerFund = await _context.CustomerFunds.FirstOrDefaultAsync(x => x.CustomerId == id);


            if (customerFund == null)
            {
                _context.Customers.Remove(customers);

                await _context.SaveChangesAsync();

                _serilog.LogInformation("Process done");
                return new { message = "success" };

            }

            var fund = await _context.MutualFundBanks.FirstOrDefaultAsync(x => x.FundId == customerFund.FundId);
         




  
                fund.TotalInvestment -= customerFund.InvestedAmount;

                fund.TotalUnits -= customerFund.InvestedAmount / fund.NAV;
                _context.MutualFundBanks.Update(fund);
                _context.CustomerFunds.Remove(customerFund);
                _context.Customers.Remove(customers);

      

            await _context.SaveChangesAsync();

            _serilog.LogInformation("Process done");
            return new { message = "success" };
        }

        private bool CustomersExists(int id)
        {
            return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }

    }
}
