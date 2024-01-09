using AutoMapper;
using SiteDevice.DataLayer.DTO;
using SiteDevice.DataLayer.Models;
using SiteDevice.DataLayer.Repository.UserRepository;

namespace SiteDevice.BusinessLogicLayer.UserBLL;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(
        IMapper mapper,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    public async Task PostUserAsync(UserDTO user)
    {
        var actualUser = _mapper.Map<User>(user);
        await _userRepository.PostUserAsync(actualUser);
    }
    

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteUserAsync(id);
    }

    public async Task LinkDeviceToUserAsync(int deviceId, int userId)
    {
        await _userRepository.LinkDeviceToUserAsync(deviceId, userId);
    }

    public async Task<ICollection<DeviceDTO>?> GetDevicesOfUserAsync(int id)
    {
        var devices =  await _userRepository.GetDevicesOfUserAsync(id);
        return _mapper.Map<ICollection<DeviceDTO>>(devices);
    }

    public async Task UnlinkDeviceFromUserAsync(int deviceId, int userId)
    {
        await _userRepository.UnlinkDeviceFromUserAsync(deviceId, userId);
    }
}