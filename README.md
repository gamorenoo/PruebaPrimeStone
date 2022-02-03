# PruebaPrimeStone
 PruebaPrimeStone
 
 La solución está construida con Net Core 5
 
 Se utiliza Entity para Persistencia de datos en Sql Server
 
 Se persiste en base de datos local de sql server, no se requiere instalar el server de Base de datos, ya que se usan paquetes nuget  Microsoft.Data.Sqlite.Core
 para el uso de la instancia que visual studio trae integrada.
 
 Se deben ejecutar las migraciones para crear el modelo de datos donde se quiera.
 
 REquest Para crear un estudiante:
 
 {
  "id": 0,
  "nombres": "Gudtavo",
  "apellidos": "Moreno",
  "fechaNacimento": "1987-01-13T00:01:30.498Z",
  "genero": 1,
  "direcciones": [
    {
      "stringDireccion": "Carrera 90A # 8A - 68, Torre 5 Apto 519",
      "tipoDireccion": 0
    },
	{
      "stringDireccion": "Cl. 72 ##No. 12-65, Bogotá, Cundinamarca",
      "tipoDireccion": 1
    }
  ],
  "cursos": []
}


Request para crear Cursos:

{
  "id": 0,
  "codigoCurso": "P1",
  "nombreCurso": "Programación Básica",
  "fechaInicio": "2022-02-20T02:40:52.045Z",
  "fechaFin": "2022-12-20T02:40:52.045Z"
}
 
