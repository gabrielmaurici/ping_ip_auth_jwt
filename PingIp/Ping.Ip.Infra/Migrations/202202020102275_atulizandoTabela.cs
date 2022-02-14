namespace Ping.Ip.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atulizandoTabela : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dispositivoes", "TipoDispositivo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dispositivoes", "TipoDispositivo");
        }
    }
}
