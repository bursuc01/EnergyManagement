using System.Text;
using AutoMapper;
using SiteUser.DataLayer.DTO;
using SiteUser.DataLayer.Models;
using SiteUser.DataLayer.Repository.UserRepository;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SiteUser.BusinessLogicLayer.UserBLL;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserService(
        IUserRepository userRepository,
        IMapper mapper
        )
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<UserDTO>> GetUsersAsync()
    {
         var users = await _userRepository.GetUsersAsync();
         return _mapper.Map<IEnumerable<UserDTO>>(users);
    }

    public async Task<UserDTO> GetUserAsync(int id)
    {
        var user = await _userRepository.GetUserAsync(id);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task PutUserAsync(UserDTO user)
    {
        var actualUser = _mapper.Map<User>(user);
        await _userRepository.PutUserAsync(actualUser);
    }

    public async Task PostUserAsync(UserDTO user)
    {
        var actualUser = _mapper.Map<User>(user);
        await _userRepository.PostUserAsync(actualUser);
        
        if (user.Name != null)
        {
            var microUser = await _userRepository.GetUserAsync(user.Name);
            await PostUserToDeviceMicroserviceAsync(microUser.Id);
        }
    }

    public async Task DeleteUserAsync(int id)
    {
        await DeleteUserFromDeviceMicroserviceAsync(id);
        await _userRepository.DeleteUserAsync(id);
    }

    private static async Task PostUserToDeviceMicroserviceAsync(int id)
    {
        var value = Environment.GetEnvironmentVariable("DEVICE_IP");
        var userToBeSent = new UserDeviceDTO(id);
        var content = JsonSerializer.Serialize(userToBeSent);
        var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
    
        await new HttpClient().PostAsync("http://" + value + ":5267/api/User", stringContent);
    }
    
    private static async Task DeleteUserFromDeviceMicroserviceAsync(int id)
    {
        var value = Environment.GetEnvironmentVariable("DEVICE_IP");
        var uri = "http://" + value + ":5267/api/User?id=" + id;
        await new HttpClient().DeleteAsync(uri);
    }
}