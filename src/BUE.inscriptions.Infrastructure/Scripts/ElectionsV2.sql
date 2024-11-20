drop table Organizations
CREATE TABLE Organizations (
    id INT identity(1,1) PRIMARY KEY,
    name VARCHAR(255) not null,
    image VARCHAR(300)  null,
    banner_image VARCHAR(300) null,
    acronym VARCHAR(50) null,
    slogan VARCHAR(600) null,
    content VARCHAR(MAX) null,
    proposal VARCHAR(MAX) null,
    additional_files VARCHAR(MAX) null,
    status bit,
	CreatedAt datetime null,
	UpdatedAt datetime null,
	DeletedAt datetime null
);

drop table [dbo].[CandidateOrganization]
CREATE TABLE [dbo].[CandidateOrganization] (
  [id] int  IDENTITY(1,1) primary key NOT NULL,
  [organization_id] int NOT NULL,
  [user_id] int  NOT NULL,
  [is_representative] bit NULL,
  [status] bit  NULL,
  [CreatedAt] datetime  NULL,
  [UpdatedAt] datetime  NULL,
  [DeletedAt] datetime  NULL,
)

drop table [dbo].[OrganizationElection]
CREATE TABLE [dbo].[OrganizationElection] (
  [id] int  IDENTITY(1,1) primary key NOT NULL,
  [election_id] int  NOT NULL,
  [organization_id] int NOT NULL,
  [is_representative] bit NULL,
  [status] bit  NULL,
  [CreatedAt] datetime  NULL,
  [UpdatedAt] datetime  NULL,
  [DeletedAt] datetime  NULL,
);


CREATE TABLE [dbo].[OrganizationVotes] (
  [id] int  IDENTITY(1,1) primary key NOT NULL,
  [organization_election_id] int  NULL,
  [election_id] int  NOT NULL,
  [vote_type] varchar(50)  NULL,
  [vote_date] datetime  NULL,
  [user_id] int  NOT NULL,
  [CreatedAt] datetime  NULL,
  [DeletedAt] datetime  NULL,
  [UpdatedAt] datetime  NULL
);


ALTER TABLE [dbo].[CandidateOrganization] ADD CONSTRAINT [FK_CANDIDATE_ORGANIZATION] FOREIGN KEY ([organization_id]) REFERENCES [dbo].[Organizations] ([id])

ALTER TABLE [dbo].[CandidateOrganization] ADD CONSTRAINT [FK_ORGANIZATION_CANDIDATE] FOREIGN KEY ([candidate_id]) REFERENCES [dbo].[Candidates] ([id])


drop table OrganizationElection
drop table Candidates
drop table Organizations 


ALTER TABLE [dbo].[Positions] ADD  seats int null
ALTER TABLE [dbo].[Elections] ADD  seats int null



EXEC sp_rename '[dbo].[Candidates].[urlImage]', 'image', 'COLUMN'



 --- eliminar
CREATE TABLE [dbo].[Candidates] (
  [id] int  IDENTITY(1,1) primary key NOT NULL,
  [first_name] nvarchar(255)   NULL,
  [second_name] nvarchar(255)   NULL,
  [lastname] nvarchar(255)  NULL,
  [second_lastname] nvarchar(255)  NULL,
  [entry_date] date  NULL,
  [exit_date] date  NULL,
  [identification] nvarchar(255) NULL,
  [email] nvarchar(255)  NULL,
  [phone1] nvarchar(255)  NULL,
  [phone2] nvarchar(255)  NULL,
  [address] nvarchar(255)  NULL,
  [abbreviation] nvarchar(255)  NULL,
  [status] bit  NULL,
  [image] varchar(300)  NULL,
  [CreatedAt] datetime  NULL,
  [UpdatedAt] datetime  NULL,
  [DeletedAt] datetime  NULL
  
)


---  verison 2 portal

CREATE TABLE dbo.Company (
    Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    CompanyName varchar(255) NOT NULL, -- Nombre de la Compañía
    Industry varchar(100) NULL, -- Industria
    Website varchar(255) NULL, -- Sitio Web
    PhoneNumber varchar(50) NULL, -- Número de Teléfono
    Email varchar(255) NULL, -- Correo Electrónico
    AddressLine1 varchar(500) NULL, -- Línea de Dirección 1
    AddressLine2 varchar(500) NULL, -- Línea de Dirección 2
    City varchar(100) NULL, -- Ciudad
    State varchar(100) NULL, -- Estado/Provincia
    Country varchar(100) NULL, -- País
    PostalCode varchar(20) NULL, -- Código Postal
    IsActive bit NULL, -- Estado Activo/Inactivo
    CreatedAt datetime NULL, -- Fecha de Creación
    UpdatedAt datetime NULL, -- Fecha de Actualización
    DeletedAt datetime NULL -- DeletedAt
);



CREATE TABLE dbo.Student (
    Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
   CompanyId INT NOT NULL,
   UserId INT NOT NULL,
    FirstName varchar(180) NOT NULL, -- PrimerNombre
    MiddleName varchar(180) NULL, -- SegundoNombre
    LastName varchar(120) NOT NULL, -- PrimerApellido
    SecondLastName varchar(120) NULL, -- SegundoApellido
    IdentificationTypeId int NULL, -- CodigoTipoIdentificacion
    BirthCountryId int NULL, -- CodigoPaisNacimiento
    NationalityId int NULL, -- CodigoNacionalidad
    SecondaryNationalityId int NULL, -- CodigoNacionalidadSecundaria
    GenderId int NULL, -- CodigoSexo
    BloodTypeId int NULL, -- CodigoTipoSangre
    Identification char(15) NULL, -- Identificacion
    BirthDate date NULL, -- FechaNacimiento
    Address varchar(4000) NULL, -- Direccion
    PreviousSchoolName varchar(150) NULL, -- NombreColegioProcede
    BirthCity varchar(100) NULL, -- CiudadNacimiento
    IsActive bit NULL, -- Estado
    Image varchar(500) NULL, -- Imagen
    IdentificationDocument varchar(500) NULL, -- DocumentoIdentificacion
    Sector varchar(255) NULL, -- Sector
    Telephone varchar(255) NULL, -- Telefono
    Email varchar(255) NULL, -- Email
    EmergencyContactPrefix varchar(50) NULL, -- PrefijoContactoEmergencia
    EmergencyContact varchar(255) NULL, -- ContactoEmergencia
    ExternalId INT NULL,  -- IdExterno
    CreatedAt datetime NULL, -- CreatedAt
    UpdatedAt datetime NULL, -- UpdatedAt
    DeletedAt datetime NULL -- DeletedAt
)

ALTER TABLE dbo.Student
ADD CONSTRAINT FK_Student_Company
    FOREIGN KEY (CompanyId) REFERENCES dbo.Company(Id)

ALTER TABLE dbo.Student
ADD CONSTRAINT FK_Student_User
    FOREIGN KEY (UserId) REFERENCES dbo.Usuarios(Codigo);



CREATE TABLE dbo.Requests (
    Id int IDENTITY(1, 1) PRIMARY KEY NOT NULL,
   CompanyId INT NOT NULL,
    StudentId INT NOT NULL, -- IdEstudiante
    UserId int NULL, -- IdUsuario
    AcademicYearId int NULL, -- CodigoAnioLectivo
    FirstRepresentativeId int NULL, -- CodigoPrimerRepresentante
    SecondRepresentativeId int NULL, -- CodigoSegundoRepresentante
    PhotographyStatus int NULL, -- EstadoFotografia
    RequestStatus int NULL, -- EstadoSolicitud
    IsActive bit NULL, -- Estado
    FileUrl varchar(500) NULL, -- urlFile
    Comment1 varchar(max) NULL, -- Coment1
    Comment2 varchar(max) NULL, -- Coment2
    Comment3 varchar(max) NULL, -- Coment3
    StudentRepresentativeInfo varchar(max) NULL, -- InformacionStudentRepresentative
    IntegrationInformation varchar(max) NULL, -- InformacionIntegracion
    AdditionalInformation varchar(max) NULL, -- InformacionAdicional
    Notes varchar(500) NULL, -- Notas
    SecondRepresentativeReason int NULL, -- MotivoRepresentante2
    SecondRepresentativeRegistration bit NULL, -- RegistroRepresentante2
    SingServiceId varchar(500) NULL, -- IdSingService
    SingServiceUrl varchar(500) NULL, -- UrlSingService
    SingServiceStatus bit NULL, -- statusSingService
    SingTransportId varchar(500) NULL, -- IdSingTransport
    SingTransportUrl varchar(500) NULL, -- UrlSingTransport
    SingTransportStatus bit NULL, -- statusSingTransport
    BankSingServiceId varchar(500) NULL, -- IdBankSingService
    BankSingServiceUrl varchar(500) NULL, -- UrlBankSingService
    BankSingServiceStatus bit NULL, -- statusBankSingService
    PreviousStatus int NULL, -- EstadoAnterior
    EnrollmentProcess varchar(800) NULL, -- procesoInscripcion
    SecondRepresentativeJustificationUrl varchar(300) NULL, -- urlJustificacionRepresentante2
    CompleteReportUrl varchar(500) NULL, -- urlReportComplete
    PreviousGradeCourseId int NULL, -- codigoCursoGradoAnterior
    LevelId int NULL, -- CodigoNivel
    GradeCourseId int NULL, -- CodigoCursoGrado
    ParallelId int NULL, -- CodigoParalelo
    SpecializationId int NULL, -- CodigoEspecialidad
    IsRepeater bit NULL, -- EsRepetidor
    EnrollmentProcessStatus bit NULL, -- ProcesoMatricula
    CompleteDocumentation bit NULL, -- DocumentacionCompleta
    EnrollmentDate datetime NULL, -- FechaMatricula
    Withdrawn bit NULL, -- Retirado
    WithdrawalDate date NULL, -- FechaRetiro
    WithdrawalDestination varchar(50) NULL, -- DestinoRetiro
    InternationalSection bit NULL, -- SeccionInternacional
    EnrollmentType int NULL, -- TipoInscripcion
    AcceptConditions bit NULL, -- AceptoCondiciones
    AuthorizeImageUsage bit NULL, -- AutorizaUsarImagen
    AuthorizeImagePublication bit NULL, -- AutorizaPublicarImagen
    AuthorizeImageSharing bit NULL, -- AutorizaCompartirImagen
    AuthorizeImageRetention bit NULL, -- AutorizaConservarImagen
    PreviousEstablishmentCountryId int NULL, -- CodigoPaisEstablecimientoAnterior
    PreviousEstablishmentCityId int NULL, -- CodigoCiudadEstablecimientoAnterior
    IsScholarshipRecipient bit NULL, -- EsBecado
    ScholarshipOrganism int NULL, -- OrganismoBeca
    UseTransport bit NULL, -- TomarTransporte
    AcceptTransportConditions bit NULL, -- AceptoCondicionesTransporte
    AuthorizeDeparture bit NULL, -- AutorizaSalida
    EmergencyReference int NULL, -- EmergenciaReferencia
    PedagogicalTrip bit NULL, -- salidaPedagogica
    InstitutionalRepresentation bit NULL, -- representacionInstitucion
    AcceptTerms bit NULL, -- AceptoTerminos
    AcceptUseData bit NULL, -- aceptUseData
    InstitutionQuestion bit NULL, -- PreguntaInstitucion
    WithdrawalQuestion bit NULL, -- PreguntaRetiro
    NotAuthorizedLeaveAlone bit NULL, -- NoAutorizadoSalirSolo
    AuthorizedFreeHoursDeparture bit NULL, -- AutorizacionSalidaHorasLibres
    ExternalId INT NULL, -- IdExterno
    CreatedAt datetime NULL, -- CreatedAt
    UpdatedAt datetime NULL, -- UpdatedAt
    DeletedAt datetime NULL -- DeletedAt
)


CREATE TABLE dbo.Person (
    Id int IDENTITY(1, 1) PRIMARY KEY NOT NULL,
    CompanyId INT NOT NULL,
    UserId INT NOT NULL,
    FirstName varchar(180) NOT NULL, -- PrimerNombre
    MiddleName varchar(180) NULL, -- SegundoNombre
    LastName varchar(120) NOT NULL, -- PrimerApellido
    SecondLastName varchar(120) NULL, -- SegundoApellido
    IdentificationTypeId int NULL, -- CodigoTipoIdentificacion
    Identification varchar(20)  UNIQUE NOT NULL, -- Identificacion
    Nomination varchar(20) NULL, -- Nominacion
    BirthDate date NULL, -- FechaNacimiento
    BloodTypeCode int NULL, -- CodigoTipoSangre
    GenderCode int NULL, -- CodigoSexo
    BirthCountryCode int NULL, -- CodigoPaisNacimiento
    BirthCity varchar(100) NULL, -- CiudadNacimiento
    IsActive bit NULL, -- Estado
    NationalityCode int NULL, -- CodigoNacionalidad
    SecondaryNationalityCode int NULL, -- CodigoNacionalidadSecundaria
    MaritalStatus int NULL, -- EstadoCivil
    Image varchar(500) NULL, -- Imagen
    IdentificationDocument varchar(500) NULL, -- DocumentoIdentificacion
    PostalCode varchar(30) NULL, -- CodigoPostal
    Address varchar(250) NULL, -- Direccion
    MainStreet varchar(500) NULL, -- CallePrincipal
    SecondaryStreet varchar(500) NULL, -- CalleSecundaria
    Sector varchar(255) NULL, -- Sector
    Email varchar(220) UNIQUE NOT NULL, -- Email
    HomePhone varchar(50) NULL, -- TelefonoCasa
    CellPhone varchar(50) NULL, -- TelefonoCelular
    CellPhonePrefix varchar(10) NULL, -- prefijoCelular
    PhonePrefix varchar(10) NULL, -- prefijoTelefono
    ProfessionalSituation int NULL, -- SituacionProfesional
    Position varchar(255) NULL, -- Cargo
    OfficePhone varchar(50) NULL, -- TelefonoOficina
    WorkAddress varchar(500) NULL, -- DireccionTrabajo
    WorkPlace varchar(500) NULL, -- LugarTrabajo
    EmployerName varchar(255) NULL, -- NombreEmpleador
    WorkCountryCode int NULL, -- CodigoPaisTrabajo
    WorkCityCode int NULL, -- CodigoCiudadTrabajo
    WorkPhone varchar(80) NULL, -- TelefonoTrabajo
    WorkCategory int NULL, -- CategoriaTrabajo
    WorkPhonePrefix varchar(10) NULL, -- prefijoTelefonoTrabajo
    SystemAccess bit NULL, -- AccesoAlSistema
    ShareContacts bit NULL, -- CompartirContactos
    AdditionalInformation varchar(max) NULL, -- InformacionAdicional
    ExternalId INT NULL, -- IdExterno
    CreatedAt datetime NULL, -- CreatedAt
    UpdatedAt datetime NULL, -- UpdatedAt
    DeletedAt datetime NULL -- DeletedAt
)
ALTER TABLE dbo.Person
ADD CONSTRAINT FK_Person_Company
    FOREIGN KEY (CompanyId) REFERENCES dbo.Company(Id)

ALTER TABLE dbo.Person
ADD CONSTRAINT FK_Person_User
    FOREIGN KEY (UserId) REFERENCES dbo.Usuarios(Codigo);

ALTER TABLE [dbo].[Person] ALTER COLUMN [Identification] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
GO

ALTER TABLE [dbo].[Person] ADD CONSTRAINT [UNIQUE_IDENTIFICATION] UNIQUE CLUSTERED ([Identification], [Email])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

CREATE TABLE StudentRelatives (
    ID INT PRIMARY KEY IDENTITY,
    StudentID INT UNIQUE NOT NULL,
    PersonID INT UNIQUE NOT NULL,
    RelationTypeId INT NOT NULL, -- TipoRelacion
    IsPrincipalRepresentative bit NOT NULL, -- PrincipalRepresentante
    IsEconomicRepresentative bit NOT NULL, -- RepresentanteEconomico
    IsLegalRepresentative bit NOT NULL, -- RepresentanteLegal
    IsResponsibleRepresentative bit NOT NULL, -- RepresentanteResponsable
    LivesWithStudent bit NOT NULL, -- ViveConEstudiante
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    FOREIGN KEY (PersonID) REFERENCES Persons(PersonID) -- ParentID cambiado a PersonID
);

CREATE TABLE dbo.Users (
    Id int IDENTITY(1, 1) PRIMARY KEY NOT NULL,
    InstitutionID INT NOT NULL,
    PersonID INT NOT NULL,
    FirstName varchar(180) NOT NULL, -- Nombres
    LastName varchar(180) NOT NULL, -- Apellidos
    Email varchar(180) UNIQUE NULL,
    SecondaryEmail varchar(180) NULL, -- EmailSecundario
    EmailVerification bit NULL, -- verificacionEmail
    IsActive bit NULL, -- Estado
    IsActive bit NULL, -- Activo
    Image varchar(250) NULL, -- Imagen
    Username varchar(180) NULL, -- Usuario
    PhonePrefix varchar(20) NULL, -- PrefijoTelefono
    MainPhone varchar(500) NULL, -- TelefonoPrincipal
    Password varchar(max) NULL,
    Token varchar(max) NULL,
    ResetPassword varchar(max) NULL,
    RememberToken varchar(max) NULL,
    ExternalId INT NULL, -- IdExterno
    CreatedAt datetime NULL,
    UpdatedAt datetime NULL,
    DeletedAt datetime NULL
)

namespace BUE.Inscriptions.Domain.Entity
{
    [Table("Usuarios")]
    public class User
    {
        [Key]
        [Column("Codigo")]
        public int? Code { get; set; }
        [Column("PrimerApellido")]
        public string? FirstLastName { get; set; }
        

    }
}