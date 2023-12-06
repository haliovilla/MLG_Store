using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MLGStore.Data;
using MLGStore.Entities;
using MLGStore.Services.Common;
using MLGStore.Services.DTOs;
using MLGStore.Services.Interfaces;

namespace MLGStore.Services.Services
{
    public class CustomerService : ServiceBase, ICustomerService
    {
        private readonly IPasswordHasher<Customer> passwordHasher;

        public CustomerService(StoreDbContext dbContext, 
            ILogger<CustomerService> logger,
            IPasswordHasher<Customer> passwordHasher) 
            : base(dbContext, logger)
        {
            this.passwordHasher = passwordHasher;
        }

        public async Task<Result<CustomerDTO>> InsertAsync(CreateCustomerDTO createDto)
        {
            try
            {
                var usernameExists = await dbContext.Customers
                    .FirstOrDefaultAsync(x => x.Username == createDto.Username);
                if (usernameExists != null)
                    return Result<CustomerDTO>
                        .CreateResult(null, "Username already exists");

                var entity = mapper.Map<Customer>(createDto);
                entity.Password = passwordHasher.HashPassword(entity, createDto.Password);

                dbContext.Customers.Add(entity);
                await dbContext.SaveChangesAsync();

                return Result<CustomerDTO>
                    .CreateResult(mapper.Map<CustomerDTO>(entity));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<CustomerDTO>
                    .CreateExceptionResult(ex);
            }
        }

        public async Task<Result<CustomerDTO>> UpdateAsync(long customerId, UpdateCustomerDTO updateDto)
        {
            try
            {
                var dbEntity = await dbContext.Customers
                    .FirstOrDefaultAsync(x => x.Id == customerId);
                if (dbEntity == null)
                    return Result<CustomerDTO>
                        .CreateResult(null, "Customer not found");

                dbEntity = mapper.Map(updateDto, dbEntity);
                await dbContext.SaveChangesAsync();

                return Result<CustomerDTO>
                    .CreateResult(mapper.Map<CustomerDTO>(dbEntity));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<CustomerDTO>
                    .CreateExceptionResult(ex);
            }
        }

        public async Task<Result<bool>> DeleteAsync(long customerId)
        {
            try
            {
                var dbEntity = await dbContext.Customers
                    .FirstOrDefaultAsync(x => x.Id == customerId);
                if (dbEntity == null)
                    return Result<bool>
                        .CreateResult(false, "Customer not found");

                dbContext.Customers.Remove(dbEntity);
                await dbContext.SaveChangesAsync();

                return Result<bool>.CreateResult(true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<bool>
                    .CreateExceptionResult(ex);
            }
        }

        public async Task<Result<CustomerDTO>> FindByIdAsync(long customerId)
        {
            try
            {
                var dbEntity = await dbContext.Customers
                    .FirstOrDefaultAsync(x => x.Id == customerId);
                if (dbEntity == null)
                    return Result<CustomerDTO>
                        .CreateResult(null, "Customer not found");

                return Result<CustomerDTO>
                    .CreateResult(mapper.Map<CustomerDTO>(dbEntity));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<CustomerDTO>
                    .CreateExceptionResult(ex);
            }
        }

        public async Task<Result<List<CustomerDTO>>> GetAllAsync()
        {
            try
            {
                var dbEntities = await dbContext.Customers
                    .ToListAsync();

                return Result<List<CustomerDTO>>
                    .CreateResult(mapper.Map<List<CustomerDTO>>(dbEntities));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<List<CustomerDTO>>
                    .CreateExceptionResult(ex);
            }
        }
    }
}
