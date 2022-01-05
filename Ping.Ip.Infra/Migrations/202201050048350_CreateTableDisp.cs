namespace Ping.Ip.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableDisp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dispositivoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guid = c.Guid(nullable: false),
                        Nome = c.String(),
                        Ip = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Dispositivoes");
        }
    }
}
