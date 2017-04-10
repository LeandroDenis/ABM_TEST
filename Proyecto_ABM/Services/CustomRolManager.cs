using System.Collections.Generic;
using System.Xml;

namespace Proyecto_ABM.Services
{
    public class CustomRolManager
    {
        List<string> ListaRolApp = new List<string>();
        XmlDocument xmldoc = new XmlDocument();

        public CustomRolManager()
        {
            string path = @"C:\Users\ldenis\Desktop\Proyecto_ABM\Proyecto_ABM\Resources\Roles.xml";
            xmldoc.Load(path);
        }

        public List<string> getRoles(string rolDeUsuario)
        {
            return getRolesFromXML(xmldoc, rolDeUsuario);
        }

        private List<string> getRolesFromXML(XmlDocument xml, string rolDeUsuario)
        {
            XmlNodeList nodos = xml.GetElementsByTagName("RolItem");
            foreach (XmlElement nodo in nodos)
            {
                List<string> ListaRol = new List<string>();
                XmlNodeList subRolesPorNodo = nodo.GetElementsByTagName("SubRol");
                foreach (XmlElement item in subRolesPorNodo)
                {
                    XmlNodeList roles = item.GetElementsByTagName("Rol");
                    foreach (XmlElement rol in roles)
                    {
                        if (rolDeUsuario == rol.GetAttribute("name"))
                        {
                            ListaRol.Add(item.GetAttribute("name"));
                        }
                    }
                }
                if (ListaRol.Count == subRolesPorNodo.Count)
                {
                    ListaRolApp.Add(nodo.GetAttribute("name"));
                }
                else
                {
                    foreach (var i in ListaRol)
                    {
                        if (!ListaRolApp.Contains(i))
                            ListaRolApp.Add(i);
                    }
                }
            }
            return ListaRolApp;
        }
    }
}