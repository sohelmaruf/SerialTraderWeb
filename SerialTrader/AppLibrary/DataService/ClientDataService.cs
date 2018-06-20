using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppLibrary.Model;
using AppLibrary.Interfaces;

namespace AppLibrary.DataServices
{
    public class ClientDataService : EntityFrameworkDataService, IClientDataService
    {
        /// <summary>
        /// Create Client
        /// </summary>
        public void AddClient(client objClient)
        {
            dbConnection.clients.Add(objClient);
        }

        public client GetClient(int ID)
        {
            client objclient = dbConnection.clients.SingleOrDefault(u => u.ID == ID);
            return objclient;
        }

        public client GetClientByTitle(string title)
        {
            client objclient = dbConnection.clients.SingleOrDefault(u => u.Title == title);
            return objclient;
        }

        public client GetClientByCode(string code)
        {
            client objclient = dbConnection.clients.SingleOrDefault(u => u.Code == code);
            return objclient;
        }

        public void UpdateClient(client objClient)
        {
            dbConnection.clients.Add(objClient);
        }
    }
}
