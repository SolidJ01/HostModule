using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using HostServiceApplication.LoginReference;

namespace HostServiceApplication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class HostService : IHostService
    {
        private LoginServiceClient login = new LoginServiceClient();

        public bool CreateHost(HostData hostData, string password)
        {
            Host host = new Host();
            host.Name = hostData.Name;
            host.Phone = hostData.Phone;
            host.Description = hostData.Description;
            host.Website = hostData.Website;

            //Connect to login service, create account. Get foreign key, set hosts foreign key accordingly. 
            int result = login.CreateHost(hostData.Email, password);

            if (result != -1) //if account successfully created do the rest of the stuff
            {
                host.LoginId = result;
                using (DataModel db = new DataModel())
                {
                    db.Hosts.Add(host);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool DeleteHost(int id)
        {
            using (DataModel db = new DataModel())
            {
                Host target = db.Hosts.Find(id);
                if (target != null)
                {
                    db.Hosts.Remove(target);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public HostData GetHost(int id)
        {
            HostData hostData = new HostData();
            using (DataModel db = new DataModel())
            {
                Host host = db.Hosts.Find(id);
                if (host != null)
                {
                    hostData.Id = host.Id;
                    hostData.Name = host.Name;
                    //TODO: Connect to login service, get email
                    //Login account creation should return id of created entry. Account creation for hosts goes through host service, 
                    //allowing immediate creation of host entry with appropriate foreign key
                    hostData.Email = login.GetHostEmail(host.LoginId);

                    hostData.Phone = host.Phone;
                    hostData.Description = host.Description;
                    hostData.Website = host.Website;
                }
            }
            return hostData;
        }

        public List<HostData> GetHosts()
        {
            List<HostData> hostsData = new List<HostData>();
            using (DataModel db = new DataModel())
            {
                List<Host> hosts = db.Hosts.ToList();
                foreach (Host host in hosts)
                {
                    HostData hostData = new HostData();
                    hostData.Id = host.Id;
                    hostData.Name = host.Name;
                    //  Email from Service
                    hostData.Email = login.GetHostEmail(host.LoginId);
                    hostData.Phone = host.Phone;
                    hostData.Description = host.Description;
                    hostData.Website = host.Website;
                    hostsData.Add(hostData);
                }
            }
            return hostsData;
        }

        public bool UpdateHost(HostData hostData)
        {
            using (DataModel db = new DataModel())
            {
                Host target = db.Hosts.Find(hostData.Id);
                if (target != null)
                {
                    target.Name = hostData.Name;
                    target.Phone = hostData.Phone;
                    target.Description = hostData.Description;
                    target.Website = hostData.Website;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
