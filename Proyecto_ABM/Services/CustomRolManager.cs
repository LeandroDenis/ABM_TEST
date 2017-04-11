using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Xml;

namespace Proyecto_ABM.Services
{
    public class CustomRolManager
    {
        XmlDocument xmldoc = new XmlDocument();

        public CustomRolManager()
        {
            string path = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Roles"]);
            xmldoc.Load(path);
        }

        public List<string> getRoles(string rolDeUsuario)
        {
            return getRolesFromXML(xmldoc, rolDeUsuario);
        }

        private List<string> getRolesFromXML(XmlDocument xml, string rolDeUsuario)
        {
            List<string> listaRoles = new List<string>();
            foreach (XmlElement rol in xml.GetElementsByTagName("RolAD"))
            {
                if (rol.GetAttribute("name") == rolDeUsuario)
                {
                    foreach (XmlElement appRol in rol.GetElementsByTagName("RolApp"))
                    {
                        listaRoles.Add(appRol.GetAttribute("name"));
                    }
                }
            }
            return listaRoles;
        }
    }
}