using Microsoft.EntityFrameworkCore;
using MutualFundManagement.Models;

namespace MutualFundManagement.Services
{

    public class MutualFundBanksService
    {

        private readonly MutualFundDbContext _context;

        private readonly ILogger<MutualFundBank> _serilog;

        public MutualFundBanksService()
        {

        }
        public MutualFundBanksService(MutualFundDbContext context, ILogger<MutualFundBank> serilog)
        {
            _context = context;
            _serilog = serilog;
        }

        public async Task<List<MutualFundBank>> AllValues()
        {
            return _context.MutualFundBanks.ToList<MutualFundBank>().ToList<MutualFundBank>();

        }

        public async virtual Task<object> GetDetails(int id)
        {
            if (id == null || _context.MutualFundBanks == null)
            {
                _serilog.LogError("Fund id is null");
                return null;
            }

            var mutualFundBank = await _context.MutualFundBanks.FirstOrDefaultAsync(m => m.FundId == id);
            if (mutualFundBank == null)
            {
                _serilog.LogError("Fund not found due to null");
                return null;
            }
            _serilog.LogInformation("Process done");
            return mutualFundBank;
        }

        public async virtual Task<object> CreateMutualFund(int FundId, string FundName, float NAV)
        {
            MutualFundBank mutualFundBank = new();
            mutualFundBank.FundId = FundId;
            mutualFundBank.FundName = FundName;
            mutualFundBank.NAV = NAV;
            mutualFundBank.TotalInvestment = 0;
            mutualFundBank.TotalUnits = 0;


            try
            {
                _context.Add(mutualFundBank);
                await _context.SaveChangesAsync();

            }

            catch (DbUpdateConcurrencyException)
            {
                if (!MutualFundBankExists(mutualFundBank.FundId))
                {
                    _serilog.LogError("Fund not found");
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


        public async virtual Task<object> EditMutualFund(int id, float NAV )
        { 
                  if (id == null || _context.MutualFundBanks == null)
            {
                _serilog.LogError("Fund id is null");
                return null;
            }
            var mutualFundBank = await _context.MutualFundBanks.FirstOrDefaultAsync(x => x.FundId == id);

            if (mutualFundBank == null) 
            {
                _serilog.LogError("Fund not found");
                return null;
               }

            mutualFundBank.NAV= NAV;

            mutualFundBank.TotalInvestment = NAV * mutualFundBank.TotalUnits;

            var customerFunds = _context.CustomerFunds.Where(x=>x.FundId==mutualFundBank.FundId);



 
                try
                {
                    _context.Update(mutualFundBank);
                    await _context.SaveChangesAsync();



                foreach (var customerFund in customerFunds)
                {
                    customerFund.InvestedAmount = NAV * customerFund.InvestedUnits;
                    _context.Update(customerFund);
                    
                }
                await _context.SaveChangesAsync();
            }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MutualFundBankExists(mutualFundBank.FundId))
                    {
                        _serilog.LogError("Fund not found");
                        return null;
                    }
                    else
                    {

                        _serilog.LogError("db error");
                        throw;
                    }
                }
                _serilog.LogInformation("Process done");
                return new {message ="success"};
     
        }
        public async virtual Task<object> DeleteMutualFund(int id)
        {

            var mutualFundBank = await _context.MutualFundBanks.FirstOrDefaultAsync(x=>x.FundId==id);

            var customerFund = await _context.CustomerFunds.FirstOrDefaultAsync(x => x.FundId == id);
            if (_context.MutualFundBanks == null || mutualFundBank == null)
            {
                _serilog.LogError("customer not found");
                return null;
            }

            if (customerFund == null)
            {
                _context.MutualFundBanks.Remove(mutualFundBank);
                await _context.SaveChangesAsync();
                _serilog.LogInformation("Process done");
                return new { message = "success" };
            }
            else
            {
                _context.CustomerFunds.Remove(customerFund);
                _context.MutualFundBanks.Remove(mutualFundBank);
                await _context.SaveChangesAsync();
                _serilog.LogInformation("Process done");
                return new { message = "success" };
            }
        }

        private bool MutualFundBankExists(int id)
        {
            return (_context.MutualFundBanks?.Any(e => e.FundId == id)).GetValueOrDefault();
        }

    }




    


}
