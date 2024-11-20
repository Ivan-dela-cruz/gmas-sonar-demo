using AutoMapper;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Elecctions.Entities;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Entity.DTO.enrollment;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Inscriptions.Entities;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Domain.AppSettings
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(x =>
            {
                #region VERSION 2
                x.CreateMap<Student, StudentDTO>().ReverseMap();
                x.CreateMap<Person, PersonDTO>().ReverseMap();
                x.CreateMap<Company, CompanyDTO>().ReverseMap();
                x.CreateMap<StudentFamilies, StudentFamiliesDTO>().ReverseMap();
                x.CreateMap<Inscription, InscriptionDTO>().ReverseMap();
                x.CreateMap<PaymentInformation, PaymentInformationDTO>().ReverseMap();
                #endregion
                #region VERSION 1
                x.CreateMap<Role, RoleDTO>().ReverseMap();
                x.CreateMap<StudentBUE, StudentBUEDTO>().ReverseMap();
                x.CreateMap<StudentBUE, StudentPortal>().ReverseMap();
                x.CreateMap<StudentBUEDTO, StudentPortalDTO>().ReverseMap();
                x.CreateMap<StudentPortal, StudentPortalDTO>()
                .ForMember(dest => dest.portalRequest, opt => opt.Ignore()).ReverseMap();
                x.CreateMap<StudentPortalDTO, StudentGeneralDataDTO>().ReverseMap();
                x.CreateMap<User, UserDTO>().ReverseMap();
                x.CreateMap<UserRole, UserRoleDTO>().ReverseMap();
                x.CreateMap<User, UserResponse>().ReverseMap();
                x.CreateMap<CurrentUserResponse, UserDTO>().ReverseMap();
                x.CreateMap<Level, LevelDTO>().ReverseMap();
                x.CreateMap<LevelPortal, LevelDTO>().ReverseMap();
                x.CreateMap<CourseGrade, CourseGradeDTO>().ReverseMap();
                x.CreateMap<CourseGradePortal, CourseGradeDTO>().ReverseMap();
                x.CreateMap<Specialty, SpecialtyDTO>().ReverseMap();
                x.CreateMap<ParallelClass, ParallelClassDTO>().ReverseMap();
                x.CreateMap<Notification, NotificationDTO>().ReverseMap();
                x.CreateMap<Contact, ContactDTO>().ReverseMap();
                x.CreateMap<ContactPortal, ContactPortalDTO>()
                .ForMember(dest => dest.portalRequest, opt => opt.Ignore())
                .ReverseMap();
                x.CreateMap<StudentRepresentative, StudentRepresentativeDTO>().ReverseMap();
                x.CreateMap<Application, ApplicationDTO>().ReverseMap();
                x.CreateMap<Country, CountryDTO>().ReverseMap();
                x.CreateMap<Nationality, NationalityDTO>().ReverseMap();
                x.CreateMap<FileDownload, FileDownloadDTO>().ReverseMap();
                x.CreateMap<Province, ProvinceDTO>().ReverseMap();
                x.CreateMap<Canton, CantonDTO>().ReverseMap();
                x.CreateMap<Parish, ParishDTO>().ReverseMap();
                x.CreateMap<RelationShip, RelationShipDTO>().ReverseMap();
                x.CreateMap<Profession, ProfessionDTO>().ReverseMap();
                x.CreateMap<SchoolYear, SchoolYearDTO>().ReverseMap();
                x.CreateMap<PortalSchoolYear, SchoolYearDTO>().ReverseMap();
                x.CreateMap<AuthorizePeople, AuthorizePeopleDTO>().ReverseMap();
                x.CreateMap<Permission, PermissionDTO>().ReverseMap();
                x.CreateMap<Status, StatusDTO>().ReverseMap();
                x.CreateMap<PortalRequest, PortalRequestDTO>()
                // .ForMember(dest => dest.StudentPortal, opt => opt.Ignore())
                .ReverseMap();
                x.CreateMap<DashBoardChart, DashBoardChartDTO>().ReverseMap();
                x.CreateMap<FinanceInformation, FinanceInformationDTO>()
                    .ForMember(dest => dest.PortalRequest, opt => opt.Ignore()).ReverseMap();
                x.CreateMap<DebitType, DebitTypeDTO>().ReverseMap();
                x.CreateMap<PaymentMethod, PaymentMethodDTO>().ReverseMap();
                x.CreateMap<Bank, BankDTO>().ReverseMap();
                x.CreateMap<CreditCard, CreditCardDTO>().ReverseMap();
                x.CreateMap<CivilStatus, CivilStatusDTO>().ReverseMap();
                x.CreateMap<MedicalRecord, MedicalRecordDTO>()
                 .ForMember(dest => dest.PortalRequest, opt => opt.Ignore()).ReverseMap();
                #endregion
                // ELECTIONS
                x.CreateMap<AcademicYear, AcademicYearDTO>().ReverseMap();
                x.CreateMap<Election, ElectionDTO>().ReverseMap();
                x.CreateMap<Position, PositionDTO>().ReverseMap();
                x.CreateMap<Organization, OrganizationDTO>().ReverseMap();
                x.CreateMap<Candidate, CandidateDTO>().ReverseMap();
                x.CreateMap<CandidateElection, CandidateElectionDTO>().ReverseMap();
                x.CreateMap<Vote, VoteDTO>().ReverseMap();
                x.CreateMap<CandidateOrganization, CandidateOrganizationDTO>().ReverseMap();
                x.CreateMap<OrganizationElection, OrganizationElectionDTO>().ReverseMap();
                x.CreateMap<OrganizationVote, OrganizationVoteDTO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
