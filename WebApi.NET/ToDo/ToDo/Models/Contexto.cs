using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ToDo.Models
{
    public class Contexto : DbContext 
    {
        public Contexto() : base ("Contexto")
        {
            
        }

        public DbSet<Tarefas> Tarefas { get; set; }
        
    }
}