using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
namespace ProxyObject
{
    [Serializable]
    public enum ProcessType
    {
        CLOSE_A_CLIENT_APPLICATION=100,
        CLOSE_ALL_CLIENT_APPLICATION=101,
        SEND_MESSAGE_TO_A_CLIENT = 102,
        SEND_MESSAGE_TO_ALL_CLIENT = 103,
        SHUTDOWN_A_CLIENT_COMPUTER = 104,
        SHUTDOWN_ALL_CLIENT_COMPUTER = 105,
        NONE=113
    };
    [Serializable]
    public class ClientInfor
    {
        public string ClientName { get; set; }
        public ProcessType Type { get; set; }
    }
    [Serializable]
    public class MyProcess:MarshalByRefObject
    {
        private ArrayList listClient = new ArrayList();
        public void addClient(ClientInfor client)
        {
            listClient.Add(client);
        }
        public void updateClientToClose(string clientName)
        {
            for (int i = 0; i < listClient.Count; i++)
            {
                ClientInfor c = listClient[i] as ClientInfor;
                if (c.ClientName.Equals(clientName, StringComparison.CurrentCultureIgnoreCase))
                {
                    c.Type = ProcessType.CLOSE_A_CLIENT_APPLICATION;
                    break;
                }
            }
        }
        public void removeAllClient()
        {
            listClient.Clear();
        }
        public void removeClient(ClientInfor client)
        {
            listClient.Remove(client);
        }
        public void removeClientByName(string name)
        {
            for (int i = 0; i < listClient.Count; i++)
            {
                ClientInfor c = listClient[i] as ClientInfor;
                if (c.ClientName.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    listClient.RemoveAt(i);
                    break;
                }
            }
        }
        public ArrayList ListClient
        {
            get
            {
                return listClient;
            }
        }
        public ProcessType Type { get; set; }
    }
}
