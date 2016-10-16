namespace ToDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inclusao_Campo_UserName_Model_Tarefas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tarefas", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tarefas", "UserName");
        }
    }
}
