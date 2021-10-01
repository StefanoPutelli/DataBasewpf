using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataBasewpf
{
    class User
    {
        public ObjectId Id { get; set; }
        public string username{ get; set;}
        public string password{ get; set;}
        public string eta{ get; set;}
        public sex sex_{ get; set;}

        public override string ToString()
        {
            return username + " " + password + " " + eta + " " + sex_.ToString() ;
        }
    }

    

}