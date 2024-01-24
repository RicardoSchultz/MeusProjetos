using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Search.Query;
//using Microsoft.SharePoint.News.DataModel;

namespace IntranetSharePoint.ConsoleAprovarDocumentos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio da aprovação dos itens - " + DateTime.Now.ToString());
            
            string username = "";
            string password = "";

            username = "S1006778@tkelevator.com";
            password = "QScft6yjm-ko09";

            //string siteUrl = "https://tke.sharepoint.com/sites/LA-BD";
            //string lista = "Normativos";

            string strFileLogPath = "C:\\Temp\\TKE-Logs";
            System.IO.StreamWriter log = new System.IO.StreamWriter(strFileLogPath + "\\LOG_Eng_Approved.txt");
            log.WriteLine("Inicio da aprovação dos itens - " + DateTime.Now.ToString());
            log.WriteLine("");
            //ClientContext context = new ClientContext(siteUrl);

            SecureString pass = new SecureString();
            foreach (char c in password.ToCharArray())
            {
                pass.AppendChar(c);
            }
            context.Credentials = new SharePointOnlineCredentials(username, pass);

            Site site = context.Site;
            context.Load(site);

            Web web = site.OpenWeb("");
            context.Load(web);
            context.ExecuteQuery();

            List docLib = web.Lists.GetByTitle(lista);
            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = @"<View Scope = 'RecursiveAll'>
                                    <Query>
                                        <Where>
                                                <And>
                                                    <Eq><FieldRef Name='Area' /><Value Type='Lookup'>Engenharia</Value></Eq>
                                                    <Eq><FieldRef Name='CategoriaTKE' /><Value Type='Lookup'>Engenharia de Produtos Mecânicos</Value></Eq>
                                                </And>
                                        </Where>
                                    </Query>
                                </View>";

            ListItemCollection collListItem = docLib.GetItems(camlQuery);
            context.Load(collListItem);
            context.ExecuteQuery();

            foreach (ListItem item in collListItem)
            {
                if (((FieldUserValue)item["Author"]).LookupValue == "BU.LA.SVC.POWER.AUTOMATE")
                {
                    Console.WriteLine(item.Id + " - " + item["Title"] + " - " + ((FieldLookupValue)item["Area"]).LookupValue + " - " + ((FieldLookupValue)item["CategoriaTKE"]).LookupValue + " - " + ((FieldUserValue)item["Author"]).LookupValue);
                    log.WriteLine(item.Id + " - " + item["Title"] + " - " + ((FieldLookupValue)item["Area"]).LookupValue + " - " + ((FieldLookupValue)item["CategoriaTKE"]).LookupValue + " - " + ((FieldUserValue)item["Author"]).LookupValue);
                    context.Load(item, i => i.Folder);
                    context.ExecuteQuery();
                    //Console.WriteLine(item.Folder.ServerRelativeUrl);
                    var rootfolder = web.GetFolderByServerRelativeUrl(item.Folder.ServerRelativeUrl);
                    context.Load(rootfolder, i => i.Folders);
                    context.ExecuteQuery();
                    foreach (var folder in rootfolder.Folders)
                    {
                        //Console.WriteLine(folder.ServerRelativeUrl);
                        var targetFolder = web.GetFolderByServerRelativeUrl(folder.ServerRelativeUrl);
                        context.Load(targetFolder, i => i.UniqueId, i => i.Files);
                        context.ExecuteQuery();
                        ListItem idItemPt = docLib.GetItemByUniqueId(targetFolder.UniqueId);
                        context.Load(idItemPt);
                        context.ExecuteQuery();
                        idItemPt["_ModerationStatus"] = Convert.ToInt32(0);
                        idItemPt.Update();
                        context.ExecuteQuery();
                        idItemPt = docLib.GetItemByUniqueId(targetFolder.UniqueId);
                        context.Load(idItemPt);
                        context.ExecuteQuery();
                        Console.WriteLine(idItemPt.Id + " -" + convertModerationStatus(idItemPt["_ModerationStatus"].ToString()) + "- " + folder.ServerRelativeUrl);
                        log.WriteLine(idItemPt.Id + " -" + convertModerationStatus(idItemPt["_ModerationStatus"].ToString()) + "- " + folder.ServerRelativeUrl);

                        foreach (var file in targetFolder.Files)
                        {
                            //Console.WriteLine(file.ServerRelativeUrl);
                            var filepath = web.GetFileByServerRelativeUrl(file.ServerRelativeUrl);
                            context.Load(filepath);
                            context.ExecuteQuery();
                            ListItem idItem = docLib.GetItemByUniqueId(filepath.UniqueId);
                            context.Load(idItem);
                            context.ExecuteQuery();
                            idItem["_ModerationStatus"] = Convert.ToInt32(0);
                            idItem.Update();
                            context.ExecuteQuery();
                            idItem = docLib.GetItemByUniqueId(filepath.UniqueId);
                            context.Load(idItem);
                            context.ExecuteQuery();
                            Console.WriteLine(idItem.Id + " -" + convertModerationStatus(idItem["_ModerationStatus"].ToString()) + "- " + file.ServerRelativeUrl);
                            log.WriteLine(idItem.Id + " -" + convertModerationStatus(idItem["_ModerationStatus"].ToString()) + "- " + file.ServerRelativeUrl);
                        }
                    }
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Fim do processo de Aprovação." + DateTime.Now.ToString());
            log.WriteLine("Fim do processo de Aprovação." + DateTime.Now.ToString());
            log.Close();
            Console.ReadLine();
        }

        public static string convertModerationStatus(string Status)
        {
             switch (Status)
            {
                case "0":
                    return ("Approved");
                case "1":
                    return ("Denied");
                case "2":
                    return ("Pending");
                case "3":
                    return ("Draft");
                case "4":
                    return ("Scheduled");
                default:
                    return "Falha";
            }

        }

    }
}
