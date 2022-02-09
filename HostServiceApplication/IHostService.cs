using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HostServiceApplication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IHostService
    {

        [OperationContract]
        List<HostData> GetHosts();
        [OperationContract]
        HostData GetHost(int id);
        [OperationContract]
        bool UpdateHost(HostData hostData);
        [OperationContract]
        bool CreateHost(HostData hostData);
        [OperationContract]
        bool DeleteHost(int id);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class HostData
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Website { get; set; }
    }
}
