select Codigo,CodigoContacto, Nombres, PrimerApellido,SegundoApellido,Email,CodigoContactoSecundario from BUE_WMCR..Usuarios

--delete  BUE_WMCR..Usuarios_Roles WHERE codigoUsuario != 47
--delete  BUE_WMCR..Usuarios WHERE codigo != 47

select Codigo,CodigoContacto, Nombres, PrimerApellido,SegundoApellido,Email,CodigoContactoSecundario from BUE_WMCR..Usuarios 




select * from BUE_WMCR..Usuarios
 --- last user  2397
INSERT INTO [BUE_WMCR]..[Usuarios]
           ([CodigoContacto]
           ,[CodigoIdioma]
           ,[Usuario]
           ,[Nombres]
           ,[Identificacion]
           ,[Email]
           ,[Estado]
           ,[Activo]
           ,[CreatedAt]
           ,[UpdatedAt]
           ,[verificacionEmail]
           ,[PrimerApellido]
           ,[SegundoApellido]
		   ,FechaNacimiento
           ,[Prefijo]
           ,[Direccion]
           ,[TelefonoCelular])

---select null,'ES','Colecturia', 'Colecturia','000000','colecturia@martimcerere.edu.ec',1,1,getdate(),getdate(),1,'','',null,'Sra.','Martim Cerere','000000'



select distinct ct.Codigo,'ES',''
,ct.Nombres
,LTRIM(RTRIM(ct.Identificacion))
,ct.Email as EmailContacto
,1,1
,GETDATE()
,GETDATE()
,1
,ct.PrimerApellido
,ct.SegundoApellido
,CT.FechaNacimiento
,iif(cr.CodigoParentesco = 1,'Sr.','Sra.')
,CT.Direccion
,CT.TelefonoCelular
from EstudianteAnioLectivo eal
join ContactoEstudianteRepresentante cr on cr.CodigoEstudianteAnioLectivo = eal.CodigoEstudianteAnioLectivo
join Contacto ct on ct.Codigo= cr.CodigoContacto
where eal.CodigoAnioLectivo = 9 
and eal.CodigoEstudiante in (858)
--and ct.Codigo <> 890


update Contacto set 
--select
PrimerApellido = (select FirstName from dbo.SplitFullName(Apellidos, ' ',0) ) 
,SegundoApellido = (select LastName from dbo.SplitFullName(Apellidos, ' ',0) ) 
from Contacto where codigo in (2625)

select * from BUE_WMCR..Usuarios_Roles


select top 4 * from BUE_WMCR..Usuarios order by Codigo desc

---insert into BUE_WMCR..Usuarios_Roles(codigoRol,codigoUsuario) values(1,2435)


--update BUE_WMCR..Usuarios set CodigoContactoSecundario = 2609 where Codigo = 1831
--update BUE_WMCR..Usuarios set CodigoContactoSecundario = 890, CodigoContacto = 2609 where Codigo = 1526




--and ct.Codigo = 586

select * from BUE_WMCR..Usuarios where email like '%cinthiaherr@gmail.com%'
select * from BUE_WMCR..Usuarios where email like '%mhconstruc@gmail.com%'

select * from BUE_WMCR..Usuarios where CodigoContacto in (1585,1586)

select CodigoEstudianteAnioLectivo, NombreCompletoCIAI,IdExterno,IdUsuario from EstudianteAnioLectivo where CodigoEstudiante in (836)

select top 3 * from EstudianteAnioLectivo where CodigoAnioLectivo = 9 order by CodigoEstudianteAnioLectivo desc

select NombreCompleto,Codigo from Contacto where Codigo in (1585,1586)

--update Contacto set IdExterno = '64dfab8db93ac27fdcf4e53e', IdUser = '64dfab8db93ac20085f4e537' where Codigo = 1591
-- update EstudianteAnioLectivo set IdExterno = '64e8dcbb152bee0a58e78913', IdUser = '64e8dcbb152bee1772e7890c' where CodigoEstudianteAnioLectivo = 1543


--update Contacto set PrimerApellido = 'Landazuri', SegundoApellido = 'Wiets' where Codigo = 1585
--update Contacto set PrimerApellido = 'Rojas', SegundoApellido = 'Sanabria' where Codigo = 1586


--and ct.Codigo =1495

--select * from BUE_WMCR..Usuarios where Email like '%gelsy%'
--select * from BUE_WMCR..Usuarios where CodigoContacto = 1495

--update BUE_WMCR..Usuarios set Identificacion = '1705631347' where Codigo = 2397

--INSERT INTO BUE_WMCR..Usuarios_Roles(codigoRol,codigoUsuario)
--SELECT 2, Codigo FROM BUE_WMCR..Usuarios WHERE Codigo !=47


--select Codigo, NombreCompletoCIAI, PrimerApellido,SegundoApellido, Nombres from Contacto where SegundoApellido is null 

--update  Contacto  set PrimerApellido = '', SegundoApellido = '' where Codigo = 1559


--update  Contacto  set PrimerApellido = 'Vásconez ', SegundoApellido = 'Lozada ' where Codigo = 1544
--update  Contacto  set PrimerApellido = 'Viteri', SegundoApellido = 'Pontón' where Codigo = 1545
--update  Contacto  set PrimerApellido = 'Cabezas', SegundoApellido = 'Montenegro' where Codigo = 1546
--update  Contacto  set PrimerApellido = 'Puga ', SegundoApellido = 'Jaramillo' where Codigo = 1547
--update  Contacto  set PrimerApellido = 'Gómez ', SegundoApellido = 'Gómez' where Codigo = 1548
--update  Contacto  set PrimerApellido = 'Orellana', SegundoApellido = 'Ayala' where Codigo = 1549
--update  Contacto  set PrimerApellido = 'Jácome', SegundoApellido = 'Alvarado' where Codigo = 1550
--update  Contacto  set PrimerApellido = 'Castillo ', SegundoApellido = 'Narro' where Codigo = 1551
--update  Contacto  set PrimerApellido = 'Panamá ', SegundoApellido = 'Herrera' where Codigo = 1552
--update  Contacto  set PrimerApellido = 'Montalvo ', SegundoApellido = 'Yépez' where Codigo = 1553
--update  Contacto  set PrimerApellido = 'Armstrong ', SegundoApellido = 'Torres' where Codigo = 1554
--update  Contacto  set PrimerApellido = 'Garzón ', SegundoApellido = 'Chamorro' where Codigo = 1555
--update  Contacto  set PrimerApellido = 'González ', SegundoApellido = 'Anguilar' where Codigo = 1556
--update  Contacto  set PrimerApellido = 'Valdivieso ', SegundoApellido = 'Flores' where Codigo = 1557
--update  Contacto  set PrimerApellido = 'Martínez ', SegundoApellido = 'Guerrero' where Codigo = 1558
--update  Contacto  set PrimerApellido = 'Bravo ', SegundoApellido = 'Muñoz' where Codigo = 1560
--update  Contacto  set PrimerApellido = 'Ordóñez ', SegundoApellido = 'Sequera' where Codigo = 1561
--update  Contacto  set PrimerApellido = 'Gallo ', SegundoApellido = 'Aguilera' where Codigo = 1562
--update  Contacto  set PrimerApellido = 'De La Torre ', SegundoApellido = 'Dávila' where Codigo = 1563
--update  Contacto  set PrimerApellido = 'Parra ', SegundoApellido = 'Caymayo' where Codigo = 1564
--update  Contacto  set PrimerApellido = 'Carrión ', SegundoApellido = 'Arboleda' where Codigo = 1565
--update  Contacto  set PrimerApellido = 'Honores ', SegundoApellido = 'Rivera' where Codigo = 1566
--update  Contacto  set PrimerApellido = 'Flores ', SegundoApellido = 'Cevallos' where Codigo = 1567
--update  Contacto  set PrimerApellido = 'Polo ', SegundoApellido = 'Cabezas' where Codigo = 1568
--update  Contacto  set PrimerApellido = 'Madrid ', SegundoApellido = 'Serrano' where Codigo = 1569
--update  Contacto  set PrimerApellido = 'López ', SegundoApellido = 'Barragan' where Codigo = 1570
--update  Contacto  set PrimerApellido = 'Ortiz  ', SegundoApellido = 'Benavides' where Codigo = 1571


select Codigo, ltrim(rtrim(identificacion)) from BUE_WMCR..Usuarios where Codigo > 2397







------------     VERISON 2


select Codigo,CodigoContacto, Nombres, PrimerApellido,SegundoApellido,Email,CodigoContactoSecundario from BUE_WMCR..Usuarios

--delete  BUE_WMCR..Usuarios_Roles WHERE codigoUsuario != 47
--delete  BUE_WMCR..Usuarios WHERE codigo != 47

select Codigo,CodigoContacto, Nombres, PrimerApellido,SegundoApellido,Email,CodigoContactoSecundario from BUE_WMCR..Usuarios 

select top 20 CodigoEstudiante from EstudianteAnioLectivo where CodigoAnioLectivo = 9 and Estado = 1 and EstadoAnioLectivo = 1




select * from BUE_WMCR..Usuarios
 --- last user  2397
INSERT INTO [BUE_WMCR]..[Usuarios]
           ([CodigoContacto]
           ,[CodigoIdioma]
           ,[Usuario]
           ,[Nombres]
           ,[Identificacion]
           ,[Email]
           ,[Estado]
           ,[Activo]
           ,[CreatedAt]
           ,[UpdatedAt]
           ,[verificacionEmail]
           ,[PrimerApellido]
           ,[SegundoApellido]
		   ,FechaNacimiento
           ,[Prefijo]
           ,[Direccion]
           ,[TelefonoCelular]
		   ,CodigoContactoSecundario)

---select null,'ES','Colecturia', 'Colecturia','000000','colecturia@martimcerere.edu.ec',1,1,getdate(),getdate(),1,'','',null,'Sra.','Martim Cerere','000000'



select distinct  ct.Codigo,'ES',''

,ct.Nombres
,LTRIM(RTRIM(ct.Identificacion))
,ct.Email as EmailContacto
,1,1
,GETDATE()
,GETDATE()
,1
,ct.PrimerApellido
,ct.SegundoApellido
,CT.FechaNacimiento
,iif(cr.CodigoParentesco = 1,'Sr.','Sra.')
,CT.Direccion
,CT.TelefonoCelular
,(select top 1 cr2.CodigoContacto
	from EstudianteAnioLectivo eal2
	join ContactoEstudianteRepresentante cr2 on cr2.CodigoEstudianteAnioLectivo = eal2.CodigoEstudianteAnioLectivo
	join Contacto ct on ct.Codigo= cr2.CodigoContacto
	where eal2.CodigoAnioLectivo = 9 
	and eal2.CodigoEstudiante in (select top 10 CodigoEstudiante from EstudianteAnioLectivo where CodigoAnioLectivo = 9 and Estado = 1 and EstadoAnioLectivo = 1)
	and cr2.CodigoParentesco in (1,2) and eal2.CodigoEstudiante = eal.CodigoEstudiante and cr2.CodigoParentesco <> cr.CodigoParentesco	
	) as codigoSecundario
from EstudianteAnioLectivo eal
join ContactoEstudianteRepresentante cr on cr.CodigoEstudianteAnioLectivo = eal.CodigoEstudianteAnioLectivo
join Contacto ct on ct.Codigo= cr.CodigoContacto
where eal.CodigoAnioLectivo = 9 
and eal.CodigoEstudiante in (select top 10 CodigoEstudiante from EstudianteAnioLectivo where CodigoAnioLectivo = 9 and Estado = 1 and EstadoAnioLectivo = 1)
and cr.CodigoParentesco in (1,2)
order by 1
--and ct.Codigo <> 890




update Contacto set 
--select
PrimerApellido = (select FirstName from dbo.SplitFullName(Apellidos, ' ',0) ) 
,SegundoApellido = (select LastName from dbo.SplitFullName(Apellidos, ' ',0) ) 
from Contacto where codigo in (2625)

select * from BUE_WMCR..Usuarios_Roles


select top 4 * from BUE_WMCR..Usuarios order by Codigo desc



select * from BUE_WMCR..Usuarios where email like '%cinthiaherr@gmail.com%'
select * from BUE_WMCR..Usuarios where email like '%mhconstruc@gmail.com%'

select * from BUE_WMCR..Usuarios where CodigoContacto in (1585,1586)

select CodigoEstudianteAnioLectivo, NombreCompletoCIAI,IdExterno,IdUsuario from EstudianteAnioLectivo where CodigoEstudiante in (836)

select top 3 * from EstudianteAnioLectivo where CodigoAnioLectivo = 9 order by CodigoEstudianteAnioLectivo desc

select NombreCompleto,Codigo from Contacto where Codigo in (1585,1586)



select Codigo, ltrim(rtrim(identificacion)) from BUE_WMCR..Usuarios where Codigo > 2397