using PiperchatService.Contract;
using PiperchatService.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PiperchatService.Service
{
    [ServiceBehavior(ConcurrencyMode =ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class MessageService : IMessageService
    {
        private IMessageServiceCallback _callback = null;
        private ObservableCollection<User> _activeUsers;
        private Dictionary<int, IMessageServiceCallback> _clients;

        public MessageService()
        {
            _activeUsers = new ObservableCollection<User>();
            _clients = new Dictionary<int, IMessageServiceCallback>();
        }

        public void Connect(User user)
        {
            _callback = OperationContext.Current.GetCallbackChannel<IMessageServiceCallback>();

            if(_callback != null)
            {
                _clients.Add(user.UserId, _callback);
                _activeUsers.Add(user);
                _clients?.ToList().ForEach(client => client.Value.UsersConnected(_activeUsers));
            }
           
        }

        public void SendMessage(Message message)
        {
            IMessageServiceCallback receiverCallBack = _clients?.First(client => client.Key == message.ReceiverId).Value;
            receiverCallBack?.ForwardToClient(message);
        }

        public ObservableCollection<User> GetConnectedUsers()
        {
            return _activeUsers;
        }
    }
}
