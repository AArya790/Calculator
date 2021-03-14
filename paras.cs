using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Cal_Data_Core.Model;

namespace Cal_Data
{
    class paras
    {
        public void Data()
        {
            List<UserModel> allUsers = new List<UserModel>();

            string txtData = "";
            var rows = txtData.Split("");
            foreach (var row in rows)
            {
                var columns = row.Split("");
                var user = new UserModel();
                user.Email = columns[0];
                user.Password = columns[1];
                allUsers.Add(user);
            }

            var currentUser = allUsers.FirstOrDefault(x => x.Email == "xyz");

            if (currentUser.IsVerified)
            {
                //s
            }
            else
            {

            }
        }
    }
}
