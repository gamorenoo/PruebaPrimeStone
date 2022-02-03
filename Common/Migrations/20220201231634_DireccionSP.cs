using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class DireccionSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var insertData = @"SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                -- =============================================
                -- Author:		<Gustavo Moreno>
                -- Create date: <2022-02-01>
                -- Description:	<Inserta un registro de dirección para un estudiante>
                -- =============================================
                CREATE OR ALTER  PROCEDURE [dbo].[DireccionInsert]
	                @Direccion Varchar(Max),
	                @TipoDireccion int,
	                @EstudianteId int
                AS
                BEGIN
	                INSERT INTO Direccion(Direccion,TipoDireccion,EstudianteId,EstaBorrado,FechaCreacion)
	                VALUES(@Direccion,@TipoDireccion, @EstudianteId,0,GETDATE());
                END";

            var updateData = @"-- =============================================
                -- Author:		<Gustavo Moreno>
                -- Create date: <2022-02-01>
                -- Description:	<Actualiza un registro de dirección para un estudiante>
                -- =============================================
                CREATE OR ALTER PROCEDURE DireccionUpdate
	                @Id int,
	                @Direccion Varchar(Max),
	                @TipoDireccion int,
	                @EstudianteId int
                AS
                BEGIN
	                UPDATE Direccion 
	                SET Direccion = @Direccion
	                    , TipoDireccion = @TipoDireccion
		                , EstudianteId  = @EstudianteId
		                , FechaActualizacion = GETDATE()
	                WHERE Id = @Id;
                END
                GO";

            var getData = @"SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                -- =============================================
                -- Author:		<Gustavo Moreno>
                -- Create date: <2022-02-01>
                -- Description:	<Obtiene las direcciones de un estudiante>
                -- =============================================
                CREATE OR ALTER PROCEDURE DireccionGet
	                @EstudianteId int
                AS
                BEGIN
	                SELECT * FROM Direccion WHERE EstudianteId = @EstudianteId;
                END
                GO";

            var deleteData = @"SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                -- =============================================
                -- Author:		<Gustavo Moreno>
                -- Create date: <2022-02-01>
                -- Description:	<Elimina las direcciones de un estudiante>
                -- =============================================
                CREATE OR ALTER PROCEDURE DireccionDelete
	                @EstudianteId int
                AS
                BEGIN
	                DELETE FROM Direccion WHERE EstudianteId = @EstudianteId;
                END
                GO
                ";

            migrationBuilder.Sql(insertData);
            migrationBuilder.Sql(updateData);
            migrationBuilder.Sql(getData);
            migrationBuilder.Sql(deleteData);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropProcSql = @"DROP PROC DireccionInsert
                                DROP PROC DireccionUpdate
                                DROP PROC DireccionGet
                                DROP PROC DireccionDelete";
            migrationBuilder.Sql(dropProcSql);
        }
    }
}
