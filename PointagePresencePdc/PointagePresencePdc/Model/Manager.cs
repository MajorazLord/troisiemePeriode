using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointagePresencePdc.Model
{
    class Manager
    {
        private List<Groupe> _lesGroupes = new List<Groupe>();

        public List<Groupe> LesGroupes
        {
            get => _lesGroupes;
            private set => _lesGroupes = value;
        }

        private Groupe SelectedGroupe { get; set; }


        public Manager()
        {
            CreateLesGroupesFromSql();
        }

        public void CreateLesGroupesFromSql()
        {
            string queryString = "SELECT ID_GROUPE, POSTE_CHARGE FROM SAISIE_CONT.dbo.GROUPE_PDC";
            string connectionString = "SERVER=10.163.14.203;UID=sa;PWD=Passw0rd;DATABASE=SAISIE_CONT;Connection Timeout=2;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        //Console.WriteLine(String.Format("{0}, {1}", reader["ID_GROUPE"], reader["POSTE_CHARGE"]));
                        AddGroupeToLesGroupes((String)reader["ID_GROUPE"], (String)reader["POSTE_CHARGE"]);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        public void AddGroupeToLesGroupes(string id, string pdc)
        {
            if (_lesGroupes.Exists(t => ((Groupe)t).IdGroupe == id))
            {
                int index = _lesGroupes.FindIndex(t => ((Groupe) t).IdGroupe == id);
                _lesGroupes[index].LesPosteCharges.Add(new PosteCharge(pdc));
            }
            else
            {
                _lesGroupes.Add(new Groupe(new List<PosteCharge> { new PosteCharge(pdc) }, id));
            }
        }
    }
}
