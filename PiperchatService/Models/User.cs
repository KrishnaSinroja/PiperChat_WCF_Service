using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PiperchatService.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public ObservableCollection<Message> UserMessages { get; set; }
    }
}
