using ConsumerApi.Data;
using ConsumerApi.Models;
using ConsumerApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class PropertyRepository : IPropertyRepository
{
    private readonly AppDbContext _context;

    public PropertyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Property> FindByConsumerIdAsync(long consumerId)
    {
        return await _context.Properties.FirstOrDefaultAsync(p => p.ConsumerId == consumerId);
    }

    public async Task<bool> ExistsByBusinessIdAsync(long id)
    {
        return await _context.Properties.AnyAsync(p => p.BusinessId == id);
    }

    public async Task<bool> ExistsByConsumerIdAsync(long id)
    {
        return await _context.Properties.AnyAsync(p => p.ConsumerId == id);
    }

    public async Task UpdateAsync(Property property)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        var existingProperty = await _context.Properties
            .FirstOrDefaultAsync(p => p.Id == property.Id);

        if (existingProperty == null)
            throw new InvalidOperationException("Property not found");

        existingProperty.BuildingSqFt = property.BuildingSqFt;
        existingProperty.BuildingType = property.BuildingType;
        existingProperty.BuildingAge = property.BuildingAge;
        existingProperty.BuildingStoreys = property.BuildingStoreys;
        existingProperty.PropertyValue = property.PropertyValue;
        existingProperty.CostOfTheAsset = property.CostOfTheAsset;
        existingProperty.SalvageValue = property.SalvageValue;
        existingProperty.UsefulLifeOfAsset = property.UsefulLifeOfAsset;

        _context.Entry(existingProperty).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

    public async Task<Property> GetByConsumerIdAsync(long consumerId)
    {
      return await _context.Properties.FindAsync(consumerId);
    }

    public async Task<Property> AddAsync(Property property)
    {
        try
        {
            await _context.Properties.AddAsync(property);

            await _context.SaveChangesAsync();

            return await FindByIdAsync(property.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while adding the property: {ex.Message}");

            return null;
        }
    }

  

    public async Task<bool> ExistsByIdAsync(long propertyId)
    {
       var ExistingId = await _context.Properties.FindAsync(propertyId);
        if(ExistingId != null)
        {
            return true;
        }
        return false;
    }

    public async Task<Property> FindByIdAsync(long propertyId)
    {
      return await  _context.Properties.FindAsync(propertyId);   
        
    }

    public async Task<Property> UpdateBusinessPropertyAsync(BusinessUpdateRequest updateRequest)
    {
        var  existingProperty = await _context.Properties
               .Where(p => p.ConsumerId == updateRequest.ConsumerId)
               .FirstOrDefaultAsync();

        if (existingProperty == null)
        {
            return null;
        }

        existingProperty.BuildingSqFt = updateRequest.BuildingSqFt;
        existingProperty.BuildingType = updateRequest.BuildingType;
        existingProperty.BuildingStoreys = updateRequest.BuildingStoreys;
        existingProperty.BuildingAge = updateRequest.BuildingAge;
        existingProperty.CostOfTheAsset = updateRequest.CostOftheAsset;
        existingProperty.SalvageValue = updateRequest.SalvageValue;
        existingProperty.UsefulLifeOfAsset = updateRequest.UsefulLifeofAsset;

       await _context.SaveChangesAsync();
        return existingProperty;
    }
}
