using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ConsumerApi.Models;
//using ConsumerApi.Requests;
//using ConsumerApi.Responses;

namespace ConsumerApi.Services
{
    public interface IConsumerService
    {
        Task<IActionResult> CreateConsumerBusinessAsync(ConsumerBusinessRequest inputRequest);
        Task<ConsumerBusinessResponse> ViewConsumerBusinessAsync(long consumerId);
        Task<Bussiness> UpdateConsumerBusinessAsync(UpdateRequest updateRequest);
        Task<IActionResult> CreateBusinessPropertyAsync(BusinessInputRequest inputRequest);

        Task<Property> UpdateBusinessPropertyAsync(BusinessUpdateRequest updateRequest);
        bool CheckBusinessEligibility(ConsumerBusinessRequest inputRequest);
        bool CheckPropertyEligibility(BusinessInputRequest inputRequest);


    }
}
