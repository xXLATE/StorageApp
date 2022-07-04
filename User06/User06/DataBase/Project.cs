using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace User06.DataBase
{
    [Table("Projects")]
    public class Project
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public string URL { get; set; }
        public int User_Id { get; set; }
        public int Type_Id { get; set; }
    }
}
