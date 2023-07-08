using API.DTOs;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id);
        Task<PagedList<MessageDTO>> GetMessagesForUser();
        Task<IEnumerable<MessageDTO>> GetMEssageThread(int CurrentUserId, int recipientId);
        Task<bool> SaveAllAsync();

    }
}