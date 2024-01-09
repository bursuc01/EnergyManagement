namespace Chat.DataLayer.ChatLogs;

public class ChatLogs
{
    private static readonly Dictionary<string, string> Users = new Dictionary<string, string>();

    public bool AddUserToList(string userToAdd)
    {
        lock (Users)
        {
            foreach (var user in Users)
            {
                if (user.Key.ToLower().Equals(userToAdd.ToLower()))
                {
                    return false;
                }
            }
            Users.Add(userToAdd, null);
            return true;
        }
    }

    public void AddUserConnectionId(string user, string connectionId)
    {
        lock (Users)
        {
            if (Users.ContainsKey(user))
            {
                Users[user] = connectionId;
            }
        }
    }

    public string? GetUserByConnectionId(string connectionId)
    {
        lock (Users)
        {
            return Users
                .Where(x => x.Value.Equals(connectionId))
                .Select(x => x.Key)
                .FirstOrDefault();
        }
    }
    
    public string? GetConnectionIdByUser(string user)
    {
        lock (Users)
        {
            return Users
                .Where(x => x.Value.Equals(user))
                .Select(x => x.Value)
                .FirstOrDefault();
        }
    }

    public void RemoveUserFromList(string user)
    {
        lock (Users)
        {
            if (Users.ContainsKey(user))
            {
                Users.Remove(user);
            }
        }
    }

    public string[] GetOnlineUsers()
    {
        lock (Users)
        {
            return Users
                .OrderBy(x => x.Key)
                .Select(x => x.Key)
                .ToArray();
        }
    }
}