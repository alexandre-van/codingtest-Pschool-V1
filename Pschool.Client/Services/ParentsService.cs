using System.Net.Http.Json;
using Pschool.Client.Models;

namespace Pschool.Client.Services
{
    public class ParentsService : IParentsService
    {
        private readonly HttpClient _httpClient;
        private const string ApiEndpoint = "api/Parents";
        public ParentsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CreateParentAndStudentAsync(ParentAndStudentDTO parentAndStudent)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{ApiEndpoint}/with-student", parentAndStudent);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<ParentDTO>> GetListParentsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ParentDTO>>(ApiEndpoint);
                return response;
            }
            catch
            {
                return null;
            }
        }
        public async Task<ParentDTO> GetParentAsync(int parentId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ParentDTO>($"{ApiEndpoint}/{parentId}");
                return response;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> EditParentAsync(int parentId, ParentDTO parentDTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{ApiEndpoint}/{parentId}", parentDTO);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }

        }
        public async Task<bool> DeleteParentAsync(int parentId)
        {
            try
            {
                var response = await _httpClient.DeleteFromJsonAsync<bool>($"{ApiEndpoint}/{parentId}");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}