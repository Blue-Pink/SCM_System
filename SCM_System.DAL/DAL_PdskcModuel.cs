using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using SCM_System.Model;
using SCM_System.IDAL;
using Ninject;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SCM_System.DAL
{
    public class DAL_PdskcModuel<T> : DAL_UniversalModuel<T>, IDAL.IDAL_UniversalModuel<T> where T : BaseModel
    {
        private static readonly string strCon = ConfigurationManager.ConnectionStrings["SCMEntities"].ConnectionString;

        //entities ==db

        public async Task<List<Vw_CDU>> GetVwCDUs() {
            
            List<Vw_CDU> list = new List<Vw_CDU>();
            await Task.Run(() => {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from Vw_CDU", con);
                    SqlDataReader sd = cmd.ExecuteReader();
                    while (sd.Read())
                    {
                        list.Add(new Vw_CDU()
                        {
                            CDDate = Convert.ToDateTime(sd["CDDate"]),
                            CDID = sd["CDID"].ToString(),
                            CDState = Convert.ToInt32(sd["CDState"]),
                            DepotName = sd["DepotName"].ToString(),
                            UsersName = sd["UsersName"].ToString(),
                            CDDesc = sd["CDDesc"].ToString(),

                        });
                    }

                }
            });
            return list;
        }
    }
}
