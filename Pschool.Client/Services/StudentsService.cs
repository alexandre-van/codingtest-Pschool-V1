using System.Net.Http.Json;
using Pschool.Client.Models;

namespace Pschool.Client.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly HttpClient _httpClient;
        private const string ApiEndpoint = "api/Students";
        public StudentsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CreateParentAndStudentAsync(ParentAndStudentDTO parentAndStudent)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{ApiEndpoint}/with-parent", parentAndStudent);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<StudentDTO>> GetListStudentsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<StudentDTO>>(ApiEndpoint);
                return response;
            }
            catch
            {
                return null;
            }
        }
        public async Task<StudentDTO> GetStudentAsync(int studentId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<StudentDTO>($"{ApiEndpoint}/{studentId}");
                return response;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> EditStudentAsync(int studentId, StudentDTO studentDTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{ApiEndpoint}/{studentId}", studentDTO);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }

        }
        public async Task<bool> DeleteStudentAsync(int studentId)
        {
            try
            {
                var response = await _httpClient.DeleteFromJsonAsync<bool>($"{ApiEndpoint}/{studentId}");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}