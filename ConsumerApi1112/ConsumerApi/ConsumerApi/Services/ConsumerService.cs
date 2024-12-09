using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConsumerApi.Models;
using ConsumerApi.Repository;


namespace ConsumerApi.Services
{
    public class ConsumerService : IConsumerService
    {
      
        private readonly IConsumerRepository _consumerRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IPropertyRepository _propertyRepository;

        public ConsumerService(
           
            IConsumerRepository consumerRepository,
            IBusinessRepository businessRepository,
            IPropertyRepository propertyRepository)
        {
            
            _consumerRepository = consumerRepository;
            _businessRepository = businessRepository;
            _propertyRepository = propertyRepository;
        }
        public async Task<IActionResult> CreateConsumerBusinessAsync(ConsumerBusinessRequest inputRequest)
        {
            

            var bussinessvalue = CalculateBusinessValue(inputRequest.CapitalInvested, inputRequest.BusinessTurnover, inputRequest.BusinessAge );
            
            var consumer = new Consumer
            {
                FirstName = inputRequest.FirstName,
                LastName = inputRequest.LastName,
                Dob  =  inputRequest.Dob,
                Email = inputRequest.Email,
                Pan = inputRequest.Pan,
                BusinessName = inputRequest.BusinessName,
                Validity = inputRequest.Validity,
                AgentName = inputRequest.AgentName,
                AgentId = inputRequest.AgentId,
               
            };

            var consumerSaved = await _consumerRepository.AddAsync(consumer);

            

            var business = new Bussiness
            {
                ConsumerId = consumerSaved.Id,
                BusinessName = inputRequest.BusinessName,
                BusinessType = inputRequest.BusinessType,
                BusinessAge = inputRequest.BusinessAge,
                TotalEmployees = inputRequest.TotalEmployees,
                CapitalInvested = (long)inputRequest.CapitalInvested,
                BusinessTurnover = (long)inputRequest.BusinessTurnover,
                BusinessValue = Convert.ToInt32( bussinessvalue)
            };

            var businessSaved = await _businessRepository.AddAsync(business);

            return new OkObjectResult("Created");
        }
        public async Task<ConsumerBusinessResponse> ViewConsumerBusinessAsync(long consumerId)
        {
            var consumer = await _consumerRepository.GetByIdAsync(consumerId);
            if (consumer == null)
            {
                throw new NullReferenceException(nameof (consumer));    
            }
            var business = await _businessRepository.GetByConsumerIdAsync(consumerId);

            var response = new ConsumerBusinessResponse
            {
                FirstName = consumer.FirstName,
                LastName = consumer.LastName,
                Dob = consumer.Dob,
                Email = consumer.Email,
                Pan = consumer.Pan,
                BusinessName = consumer.BusinessName,
                Validity = consumer.Validity,
                AgentName = consumer.AgentName,
                AgentId = consumer.AgentId,
                BusinessId = business.Id,
                BusinessType = business.BusinessType,
                BusinessAge = business.BusinessAge,
                TotalEmployees = business.TotalEmployees,
                CapitalInvested = business.CapitalInvested,
                BusinessTurnover = business.BusinessTurnover
            };
            return response;
        }
        public async Task<Bussiness> UpdateConsumerBusinessAsync(UpdateRequest updateRequest)
        {
            var business = await _businessRepository.UpdateConsumerBusinessAsync(updateRequest);
            if (business == null)
            {
                return null;
            }
            return business ;
        }
        public async Task<IActionResult> CreateBusinessPropertyAsync(BusinessInputRequest inputRequest)
        {
           

            var propertyValue = CalculatePropertyValue(inputRequest.CostOftheAsset, inputRequest.SalvageValue,
                inputRequest.UsefulLifeofAsset);

            var property = new Property
            {
                BusinessId = inputRequest.BusinessId,
                ConsumerId = inputRequest.ConsumerId,
                BuildingSqFt = Convert.ToInt32(inputRequest.BuildingSqFt),
                BuildingType = inputRequest.BuildingType,
                BuildingStoreys = inputRequest.BuildingStoreys,
                BuildingAge = inputRequest.BuildingAge,
                PropertyValue = propertyValue,
                CostOfTheAsset = inputRequest.CostOftheAsset,
                SalvageValue = inputRequest.SalvageValue,
                UsefulLifeOfAsset = inputRequest.UsefulLifeofAsset
            };
            var propertySaved = await _propertyRepository.AddAsync(property);
            return new OkObjectResult("success");
        }
        public async Task<Property> UpdateBusinessPropertyAsync(BusinessUpdateRequest updateRequest)
        {

            var property = await _propertyRepository.UpdateBusinessPropertyAsync(updateRequest);
            if (property == null)
            {
                return null;
            }
            return property;
        }
        private decimal CalculateBusinessValue(decimal capitalInvested, decimal businessTurnover, int businessAge)
        {
            decimal businessAgeFactor = businessAge / 10m; 
            decimal businessValue = capitalInvested + (businessTurnover * businessAgeFactor);

            decimal rangeMin = 1m; 
            decimal rangeMax = 10m;
            decimal normalizedValue = (decimal)Math.Log10((double)(businessValue + 1)) + rangeMin;

            normalizedValue = Math.Min(rangeMax, Math.Max(rangeMin, normalizedValue));

            return Math.Abs(Math.Round(normalizedValue));
        }


        private long CalculatePropertyValue(long costOfTheAsset, long salvageValue, long usefulLifeOfAsset)
        {
            
            double xRatio = (double)((costOfTheAsset - salvageValue) / usefulLifeOfAsset);
            
            double rangeMin = 0D;
            double rangeMax = 10D;
            double xMax = (double)(costOfTheAsset / usefulLifeOfAsset);
 
            double xMin = (double)(salvageValue / usefulLifeOfAsset);
       
            double rangeDiff = (rangeMax - rangeMin);
           
            double sat = ((xRatio - xMin) / (xMax - xMin));
          
            double propertyValue = rangeDiff * sat;
          
            return (long)Math.Abs(Math.Round(propertyValue));
        }

        public bool CheckBusinessEligibility(ConsumerBusinessRequest inputRequest)
        {
            bool flag = inputRequest.BusinessAge >= 1 || inputRequest.TotalEmployees >= 1;

            return flag;
        }
        public bool CheckPropertyEligibility(BusinessInputRequest inputRequest)
        {
            bool flag = (inputRequest.BuildingType.Equals("own", StringComparison.OrdinalIgnoreCase) && inputRequest.BuildingAge >= 1) ||
                        (inputRequest.BuildingType.Equals("rent", StringComparison.OrdinalIgnoreCase) && inputRequest.BuildingAge >= 1);

            return flag;
        } 
    }
}


