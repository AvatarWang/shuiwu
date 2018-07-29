using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.Design;
using System.DirectoryServices;
using System.Configuration;
using System.IO;
using Services.Model;
using System.Web;

namespace Services
{
    public class LoginService
    {
        public LoginModel Login(string userName,string passWord)
        {
            LoginModel model = new LoginModel();
            try
            {
                var member = DataHelp.GetUserByAccount(userName);
                if (member != null)
                {

                    model.UserId = member.ID.ToString();
                    if (DataHelp.InsertLoginInfo(model.UserId))
                    {
                        model.Status = "1";
                    }
                }
            }
            catch
            {
                model.Status = "0";
            }
            return model;
        }
    }
}
