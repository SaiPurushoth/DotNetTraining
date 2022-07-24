using Microsoft.EntityFrameworkCore;
using MutualFundManagement.Models;

namespace MutualFundManagement.Services
{
    public class CustomerFundsService
    {
        private readonly MutualFundDbContext _context;
        private readonly ILogger<CustomerFunds> _serilog;

        public CustomerFundsService()
        {

        }
        public CustomerFundsService(MutualFundDbContext context, ILogger<CustomerFunds> serilog)
        {
            _context = context;
            _serilog = serilog;

        }
        public async Task<List<CustomerFunds>> AllValues()
        {
            return _context.CustomerFunds.ToList<CustomerFunds>().ToList<CustomerFunds>();
        }

        public async virtual Task<object> GetDetails(int id)
        {
            if (id == null || _context.CustomerFunds == null)

            {
                _serilog.LogError("customerFund id is null");
                return null;
            }

            var customerFunds = await _context.CustomerFunds
                .FirstOrDefaultAsync(m => m.Id == id);

            if (customerFunds == null)
            {
                _serilog.LogError("customer not found due to null");
                return null;
            }
            _serilog.LogInformation("Process done");
            return customerFunds;
        }

        public async virtual Task<object> CreateFunds(int Id, int CustomerId, float InvestedAmount, int FundId)
        {
            CustomerFunds customersFund = new();
            customersFund.Id = Id;
            customersFund.CustomerId = CustomerId;
            customersFund.InvestedAmount = InvestedAmount;
            customersFund.FundId = FundId;


            var fund = await _context.MutualFundBanks.FirstOrDefaultAsync(x => x.FundId == customersFund.FundId);
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == customersFund.CustomerId);

            if (fund == null || customer == null)
            {
                _serilog.LogError("customerFund not found");
                return null;

            }
            customersFund.InvestedUnits = customersFund.InvestedAmount / fund.NAV;

            fund.TotalInvestment += customersFund.InvestedAmount;

            fund.TotalUnits += customersFund.InvestedAmount / fund.NAV;

            try
            {
                _context.MutualFundBanks.Update(fund);
                _context.CustomerFunds.Add(customersFund);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(customersFund.CustomerId))
                {
                    _serilog.LogError("customerFund not found");
                    return null;
                }
                else
                {
                    _serilog.LogError("db error");
                    throw;
                }
            }
            _serilog.LogInformation("Process done");
            return new { message = "success" };
        }
        public async virtual Task<object> EditFunds(int id,int CustomerId, float InvestedAmount, int FundId)
        {

            var customersFund = await _context.CustomerFunds.FirstOrDefaultAsync(x => x.Id == id);

            if (customersFund == null)
            {
                _serilog.LogError("customerFund not found");
                return null;
            }

            var fund = await _context.MutualFundBanks.FirstOrDefaultAsync(x => x.FundId == customersFund.FundId);
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == CustomerId);


            customer.CustomerId = CustomerId;

            if (fund == null || customer == null)
            {
                _serilog.LogError("customerFund not found");
                return null;

            }

            fund.TotalInvestment -= customersFund.InvestedAmount;
            fund.TotalUnits -= customersFund.InvestedUnits;

            customersFund.CustomerId = CustomerId;
            customersFund.InvestedAmount = InvestedAmount;

            customersFund.FundId = FundId;

            var fundnew = await _context.MutualFundBanks.FirstOrDefaultAsync(x => x.FundId == FundId);
         
            if (fundnew == null)
            {
                _serilog.LogError("customerFund not found");
                return null;
            }
            customersFund.InvestedUnits = customersFund.InvestedAmount / fundnew.NAV;

            fundnew.TotalInvestment += InvestedAmount;

            fundnew.TotalUnits += InvestedAmount / fundnew.NAV;

            try
                {
                _context.Customers.Update(customer);
                _context.MutualFundBanks.Update(fund);
                _context.MutualFundBanks.Update(fundnew);
                _context.CustomerFunds.Update(customersFund);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomersExists(customersFund.CustomerId))
                    {
                        _serilog.LogError("customerFund not found");
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


        public async virtual  Task<object> DeleteFunds(int id)
        {
            var customersFund = await _context.CustomerFunds.FirstOrDefaultAsync(x=>x.Id==id);
            if (_context.CustomerFunds == null || customersFund == null)
            {
                _serilog.LogError("customer not found");
                return null;
            }


            var fund = await _context.MutualFundBanks.FirstOrDefaultAsync(x => x.FundId == customersFund.FundId);

            if (fund == null)
            {
                _serilog.LogError("customerFund not found due to null");
                return null;

            }
            fund.TotalInvestment -= customersFund.InvestedAmount;

            fund.TotalUnits -= customersFund.InvestedAmount / fund.NAV;


              _context.MutualFundBanks.Update(fund);
             _context.CustomerFunds.Remove(customersFund);
            await _context.SaveChangesAsync();
            _serilog.LogInformation("Process done");
            return new { message = "success" };
        }


        private bool CustomersExists(int id)
        {
            return (_context.CustomerFunds?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }


    }














}
